////////////////////////////////////////////////////////////////////////////////////////////////////
/// YBOT Comamnd Node Program
/// Used to control the command node and send data to the Tower Nodes
////////////////////////////////////////////////////////////////////////////////////////////////////

#include <SoftwareSerial.h>
#include <mcp_can.h>
#include <SPI.h>
#include <string.h>
#include <Adafruit_NeoPixel.h>
#include <EEPROM.h>



////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> NeoPixel Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region NeoPixel Variables
//Neo Pixel data pin
uint8_t neoPixPin = 8;
//Total number for Neo Pixels in the light
uint8_t stripLength = 32;
//Pixels per ring
uint8_t pixPerRing = 8;
//Defines how bright each pixel should be.
uint8_t brt = 25;

//Start NeoPixel library
Adafruit_NeoPixel light = Adafruit_NeoPixel(stripLength, neoPixPin, NEO_GRB + NEO_KHZ800);

//Define Colors
uint32_t red = light.Color(brt, 0, 0);
uint32_t green = light.Color(0, brt, 0);
uint32_t blue = light.Color(0, 0, brt);
uint32_t white = light.Color(brt, brt, brt);
uint32_t yellow = light.Color(brt, brt, 0);
uint32_t off = light.Color(0, 0, 0);
#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> CanBus Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region CANBUS Variables
//Can-bus pin (Uno Shield pin 9 ; Leonardo Boards pin 17)
uint8_t canPin = 4;
//Can-bus Interrupt Pin (Uno Shield 0 ; Leonardo 4)
uint8_t canIntrPin = 0;
//Tower Node Number
uint32_t nodeID = 21;//(uint32_t)EEPROM.read(0);

//uint32_t nodeID = 31;
//Command Node Number
uint32_t commandNode = 31;

// Set CanBus pin
MCP_CAN CAN(canPin);
//Can-bus message
byte canOut[8] = { 0, 0, 0, 0, 0, 0, 0, 0 };
//Interrupt flag
uint8_t canRecv = 0;
//Length of can-bus message
uint8_t canLength = 0;
//Read Buffer
byte canIn[8] = { 0, 0, 0, 0, 0, 0, 0, 0 };
//Destination Node
uint32_t destNode = 0;
//Tower Node status 
byte nodeStatus[8] = { 5, 0, 0, 0, 0, 0, 0, 0 };

#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> IO Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region IO Variables
//Input Pin number - Pin# of Filtered Digital Inputs - 99 if not used
uint8_t inputPins[6] = { 8, 99, 99, 99, 99, 99 };
//LED Pin number - 13 Uno - 23 Leonardo
uint8_t ledPin = 13;
uint8_t solarLED = 9; //Solar Panels LED
//Button State of the Filtered Digital Inputs - 0 = open; 1 = closed
byte inputStates[6] = { 0, 0, 0, 0, 0, 0 };
byte outputState[6] = { 0, 0, 0, 0, 0, 0 };
uint8_t autoPin = 5;
uint8_t manPin = 6;
uint8_t manTonPin = 4;
int manTonState = 1;

#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> EEPROM Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region EEPROM Variables

const int CAN_NODE_ID_EEPROM_ADDRESS = 0;

const int analog_MIN_RAW_EEPROM_ADDRESS = CAN_NODE_ID_EEPROM_ADDRESS + sizeof(byte);
const int analog_MAX_RAW_EEPROM_ADDRESS = analog_MIN_RAW_EEPROM_ADDRESS + sizeof(int);
#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> analog Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region analog Variables
#define analog_PIN A1                       // Pin connected to analog signal
int maxRaw = 325;							// Uncalibrated maximum reading
int minRaw = 325;							// Uncalibrated minimum reading
float scaleFactor = 0;						// Adjusts output to desired range
int maxRange = 10;

int analogMaxPulled = 0;					//Max Pull value
boolean fullPull = false;					//If Max pull was acheived
#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> Serial Input Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
byte index = 0;		//Counter for serial data
char inData[32];	//Incoming serial data
boolean newserial = false;
int xbRX = 3;		//xb RX pin
int xbTX = 2;		//xb TX pin
SoftwareSerial xbSerial(xbRX, xbTX);	//Start serial for XBee

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> Program Flags Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
boolean towerSelected = false;		//If the tower is selected
uint8_t selectedState = 0;			//Selected state of tower
boolean gameModeChanged = true;		//If mode is changed
boolean complete = false;			//If task is complete
uint8_t function = 0;				//Function Type
uint8_t functionMode = 0;			//Fuction Mode
uint8_t gameMode = 8;				//Game Mode Value - starts in Debug mode
uint8_t delayMultiplier = 0;		//Report Delay Multiplier
int messagesSent = 0;
int messagesRecieved = 0;

boolean sunState = false;			//Sun's state True = on, False = off
boolean alarmState = false;			//Alarm state True = on, False = offF
boolean testedState = false;		//Tower's teseted state True = tested, False = not tested
boolean reportSent = false;

int currentLocation = 0;			//Current panel location in steps
int sunTower = 0;					//Sun's tower number
int sunLocation = 0;				//Sun's location in steps
int alignment = 0;					//0 = not aligned, 1 = 10 degrees, 2 = 5 degrees, 3 = aligned
int maxSteps = 1600;					//Steps in 360 degrees
int angle_1 = 44;					//First angle to start scoring 
int angle_2 = 22;					//Second angle to score
int angle_3 = 4;					//Aligned angle
int maxSpeed = 2;					//Max speed delay (smaller = faster)
int minSpeed = 50;					//Min speed delay (larger = slower)
int dirPin = 11;						//Stepper Direction Pin
int stepPin = 10;					//Stepper step Pin
int potInputL = A2;					//Left Pot pin
int potInputR = A0;					//Right Pot pin
int potValL = 0;
int potValR = 0;

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method> Ardiuno Setup Method</method>
////////////////////////////////////////////////////////////////////////////////////////////////////
void setup()
{
	Serial.begin(115200);	//Start serial communication
	xbSerial.begin(9600);	//Start xb serial communication

	Serial.print("Node ID: ");
	Serial.println(nodeID);

	//Setup up and turn on the onboard LED to indicated power
	pinMode(ledPin, OUTPUT);
	digitalWrite(ledPin, HIGH);


	////Setup NeoPixels and test them
	//Serial.println("Neopixel Test");
	//light.begin();						//Start NeoPixel light
	//light.show();						// Initialize all pixels to 'off'
	//testPattern();						//Test Neopixels
	//solidColor(red, 0, 0, stripLength); //Set all red to begin self check

	//Setup Inputs and test to see if they are in desired state
	Serial.println("-----Inputs----");
	for (int i = 0; i < sizeof(inputPins); i++)
	{
		//If pin is a valid pin
		if (inputPins[i] != 99)
		{
			// Setup the button with an internal pull-up :
			pinMode(inputPins[i], INPUT_PULLUP);

			//Print the input state for each button
			Serial.print("Pin#");
			Serial.print(inputPins[i]);
			Serial.print(" = ");
			Serial.print(checkInput(i));

			if (!checkInput(i))
			{
				wipeColor(green, 0, 0, firstPixel(1), lastPixel(1));	//If okay light ring green
				Serial.println(" - OK");
			}
			else
			{
				wipeColor(yellow, 0, 0, firstPixel(1), lastPixel(1));	//If not light ring yellow
				Serial.println(" - PRESSED");
			}
			delay(250);	//Delay so we can see the result
		}

	}


//	//Start CAN BUS
//	Serial.println("-----Can Bus----");
//START_INIT:
//
//	Serial.print("CAN-BUS Startup: ");
//	//If CANBUS begins 
//	if (CAN_OK == CAN.begin(CAN_50KBPS))						// init can bus : baudrate = 50k
//	{
//		wipeColor(green, 0, 0, firstPixel(2), lastPixel(2));	//If okay light ring green
//		Serial.println("OK");									//Report Okay
//
//	}
//	else
//	{
//		Serial.println("Not Good, check your pin# and connections");	//Report a problem
//		wipeColor(yellow, 0, 0, firstPixel(2), lastPixel(2));			//If not okay light ring yellow
//		goto START_INIT;												//If startup failed try again
//	}
//
//	// There are 2 mask in mcp2515
//	// Set both Masks
//	CAN.init_Mask(0, 0, 0x1f);
//	CAN.init_Mask(1, 0, 0x1f);
//
//	// set filter, we can receive
//	CAN.init_Filt(0, 0, nodeID);							// there are 6 filter in mcp2515
//	//CAN.init_Filt(1, 0, 0x00);                            // there are 6 filter in mcp2515
//
//	CAN.init_Filt(2, 0, nodeID);							// there are 6 filter in mcp2515
//	//CAN.init_Filt(3, 0, 0x00);							// there are 6 filter in mcp2515
//	//CAN.init_Filt(4, 0, 0x08);							// there are 6 filter in mcp2515
//	//CAN.init_Filt(5, 0, 0x09);							// there are 6 filter in mcp2515
//
//	//Check to see if we can talk to the command node	
//	Serial.println("CAN-BUS Communication: Command Node");

	Serial.println("-----XBee-----");
	xbSerial.println("31,0,0");

	//Solar Panel Pins
	Serial.println("-----Solar Panel-----");
	pinMode(dirPin, OUTPUT);
	pinMode(stepPin, OUTPUT);
	pinMode(solarLED, OUTPUT);
	//pinMode(potInputL, INPUT);
	//pinMode(potInputR, INPUT);

	//Blink LEDs 
	digitalWrite(solarLED, HIGH);
	delay(1000);
	digitalWrite(solarLED, LOW);
	delay(1000);
	digitalWrite(solarLED, HIGH);
	delay(1000);
	digitalWrite(solarLED, LOW);


	//delay(2000); //Visual Delay

	//solidColor(off, 0, 0, stripLength);	//Turn off light

	Serial.println("-----Home Panel-----");
	homePanel();
	Serial.println("-----Setup Complete-----");
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method> Main Loop </method>
////////////////////////////////////////////////////////////////////////////////////////////////////
void loop()
{
	////While there is CANBUS data available
	//while (CAN_MSGAVAIL == CAN.checkReceive())
	//{
	//	// read data,  len: data length, incoming: data incoming
	//	CAN.readMsgBuf(&canLength, canIn);
	//	messagesRecieved++;
	//	execute();				//Execute new message
	//}

	if (newserial)
	{
		newserial = false;

		//If message is for the command node process the data
		if (destNode == nodeID)
		{
			for (uint8_t i = 0; i < sizeof(canIn); i++)
			{
				canIn[i] = canOut[i];	//Move message to canIn
			}

			Serial.print("Destination Node#");
			Serial.print(destNode);
			Serial.print(" : Message = ");
			for (int i = 0; i < 8; i++)
			{
				Serial.print(canIn[i]);
				Serial.print("|");
			}
			Serial.println();

			execute();					//Execute Command
		}
		else if (destNode == commandNode)			//It's an xbee node
		{
			for (uint8_t i = 0; i < sizeof(canIn); i++)
			{
				xbSerial.print(canOut[i]);	//send data
			}
			xbSerial.println();
		}
		//else							//Else send it out
		//{
		//	messagesSent++;
		//	uint32_t _destinationAddress = address(destNode, nodeID);			//Build Address		
		//	CAN.sendMsgBuf(_destinationAddress, 0, sizeof(canOut), canOut);		//Send Message
		//}
	}

	while (Serial.available())
	{
		char aChar = Serial.read();		//Read data

		if (aChar == '$')
		{
			inData[0] = aChar;
			index = 1;
		}
		else								//If no new line keep collecting the serial data
		{
			if (inData[0] = '$')
			{
				if (aChar == '\n')				//If there is a new line
				{
					parseData();				//Parse the data that was received
					newserial = true;
				}
				else
				{
					inData[index] = aChar;			//Add next character received to the buffer
					index++;						//Increment index
					inData[index] = '\0';			//Keep NULL Terminated as last the last character
				}
			}
		}
	}

	while (xbSerial.available())
	{
		char aChar = xbSerial.read();		//Read data

		//Serial.print(aChar);

		if (aChar == '$')
		{
			inData[0] = aChar;
			index = 1;
		}
		else								//If no new line keep collecting the serial data
		{
			if (inData[0] = '$')
			{
				if (aChar == '\n')				//If there is a new line
				{
					//Serial.println();
					parseData();				//Parse the data that was received
					newserial = true;
				}
				else
				{
					inData[index] = aChar;			//Add next character received to the buffer
					index++;						//Increment index
					inData[index] = '\0';			//Keep NULL Terminated as last the last character
				}
			}
		}
	}

	if (function == 9)
	{
		if (functionMode == 5) NetworkResponseTest();
		else if (functionMode == 6) NetworkSpeedTest();
	}
	else gamePlayCanbus();
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>Execute()</method>
///
/// <summary>Used to execute incoming commands</summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void execute()
{
	uint_least8_t msgType = canIn[0];		//First byte = Message type

	if (msgType == 0)						//Report nodeStatus
	{
		report(true, sendingNode());		//Send Report to the sending Node
	}
	else if (msgType == 1)					//Set neopixels to desired state
	{
		neoPix();
	}
	else if (msgType == 2)					//Set current game mode
	{
		gameMode = canIn[1];				//Set Game Mode to the new Game Mode
		gameModeChanged = true;
		nodeStatus[3] = gameMode;
	}
	else if (msgType == 3)					//Set transmitters to desired state
	{
		if (canIn[1] == 0)
		{
			digitalWrite(autoPin, LOW);
			digitalWrite(manPin, LOW);
		}
		else if (canIn[1] == 1)
		{
			digitalWrite(autoPin, HIGH);
			digitalWrite(manPin, LOW);
		}
		else if (canIn[1] == 2)
		{
			digitalWrite(autoPin, HIGH);
			digitalWrite(manPin, HIGH);
		}
	}
	else if (msgType == 4)					//Set 1-wire relays to desired state
	{
		//setRelays(canIn[1], canIn[2]);
	}
	else if (msgType == 5)					//Print Current Message
	{
		Serial.print(sendingNode());
		Serial.print(",");
		for (int i = 0; i < sizeof(canIn); i++)
		{
			Serial.print(canIn[i]);
			Serial.print(",");
		}
		Serial.println();
	}
	else if (msgType == 6)					//Set Function Type
	{
		function = canIn[1];
		functionMode = canIn[2];

		if (canIn[3] > 0) delayMultiplier = canIn[3];
		else delayMultiplier = 0;

		if (function == 6) ChangeBaudRate(canIn[2]);

		if (function == 9) nodeStatus[0] = 9;
		else nodeStatus[0] = 5;
	}
	else if (msgType == 7)
	{
		if (canIn[1] == 1)
		{
			towerSelected = true;
			selectedState = canIn[2];
			if (selectedState == 1)
			{
				wipeColor(yellow, 0, 1, firstPixel(3), lastPixel(4));
				sunState = true;
			}
			else if (selectedState == 2)
			{
				wipeColor(red, 0, 1, firstPixel(2), lastPixel(2));
				alarmState = true;
			}
			else if (selectedState == 3)
			{
				homePanel();
			}
			else if (selectedState == 4)
			{
				towerLocation(canIn[3]);
				reportSent = false;
				complete = false;
			}
			else if (selectedState == 9)
			{
				//Don't calibrate FoS in game
				if ((gameMode != 2) && (gameMode != 3) && (gameMode != 4) && (gameMode != 5))
				{
					//calibrate(maxRange);			// Calibrate and set range
				}
			}
			else
			{
				if (sunState)
				{
					sunState = false;
					solidColor(blue, 0, firstPixel(3), lastPixel(4));
				}
				if (alarmState)
				{
					alarmState = false;
					solidColor(blue, 0, firstPixel(2), lastPixel(2));
				}
			}
		}
		else towerSelected = false;
	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>parseData() </method>
///
/// <summary>Used to Parse Incoming Serial Data </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void parseData()
{
	char *token = NULL;		//Set pointer

	int counter = 0;

	//Serial.print("Pre-parse");
	//for (int i = 0; i < sizeof(inData); i++)
	//{
	//	Serial.print(inData[i]);
	//	Serial.print("|");
	//}
	//Serial.println();

	//If not NULL
	if (inData[0] == '$')
	{ 
		//Break data at commas
		token = strtok(inData, ",");
		token = strtok(NULL, ",");	//Break at the next comma

		destNode = atoi(token);		//Convert message to number: First message is Destination Node
		token = strtok(NULL, ",");	//Break at the next comma
									//Iterate through the message storing each part of the message in the canOut buffer
		for (uint8_t i = 0; i < sizeof(canOut); i++)
		{
			//If token is not NULL
			if (token != NULL)
			{
				canOut[i] = atoi(token);		//Store this part in the current canOut byte
				token = strtok(NULL, ",");		//Break at the next comma
			}
			else
			{
				canOut[i] = 0;					//If the message is Null set to zero
			}
		}

		//Use for debugging
		//Serial.print("Destination Node#");
		//Serial.print(destNode);
		//Serial.print(" : Message = ");
		//for (int i = 0; i < 8; i++)
		//{
		//	Serial.print(canOut[i]);
		//	Serial.print("|");
		//}
		//Serial.println();
	}

	index = 0;

	for (uint8_t i = 0; i < sizeof(inData); i++)
	{
		inData[i] = '\0';
	}



}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>Change Game Modes </method>
////////////////////////////////////////////////////////////////////////////////////////////////////
void gamePlayCanbus()
{
	if (gameMode == 1)	//Standby
	{
		if (gameModeChanged)
		{
			homePanel();
			gameModeChanged = false;
			fullPull = false;
			nodeStatus[6] = 0;
			nodeStatus[7] = 0;
			complete = false;
		}
	}
	else if (gameMode == 2)	//Start
	{
		
	}
	else if (gameMode == 3)	//Autonomous
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			reportSent = false;
			complete = false;
			fullPull = false;
			nodeStatus[6] = 0;
			nodeStatus[7] = 0;
			nodeStatus[4] = 0;
		}

		if (!complete)
		{

			potValL = analogRead(potInputL);
			potValR = analogRead(potInputR);

			int potVal = 0;
			if (potValR < 450 || potValR > 550) potVal = potValR;
			else potVal = potValL;

			if (potVal > 550)
			{
				digitalWrite(dirPin, HIGH);
				if (potVal > 800) potVal = 1000;

				int stepDelay = map(potVal, 550, 1000, minSpeed, maxSpeed);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation++;
				if (currentLocation > maxSteps) currentLocation = 0;

			}
			else if (potVal < 450)
			{
				digitalWrite(dirPin, LOW);
				if (potVal < 200)potVal = 0;
				int stepDelay = map(potVal, 450, 0, minSpeed, maxSpeed);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation--;
				if (currentLocation < 0) currentLocation = (maxSteps - 1);
			}
			//Else the handle is in neutral
			else
			{
				if (!reportSent)	//If report not sent
				{
					xbReport();
					reportSent = true;													//Reset flag
				}
			}

			//Angle Range Variables
			int maxAngle1 = 0;
			int minAngle1 = 0;

			//If sun location is 0 do special math
			if (sunLocation = 0)
			{
				maxAngle1 = 0 + angle_1;		//Set max angle starting from 0
				minAngle1 = 1600 - angle_1;		//Set min angle starting from 1600
			}
			else
			{
				maxAngle1 = sunLocation + angle_1;	//Calculate Max angle
				minAngle1 = sunLocation - angle_1;	//Calculate Min angle
			}

			//If current panel location is within greatest angle
			if ((currentLocation < maxAngle1) && (currentLocation > minAngle1))
			{
				//Angle Range Variables
				int maxAngle2 = maxAngle1 - angle_2;
				int minAngle2 = minAngle1 + angle_2;
				int maxAngle3 = maxAngle1 - angle_3;
				int minAngle3 = minAngle1 + angle_3;

				//If current panel location is within 2nd angle
				if ((currentLocation < maxAngle2) && (currentLocation > minAngle2))
				{
					analogWrite(solarLED, 50);	//Turn on the LED at 50/255
					nodeStatus[6] = 2;
				}
				//If current panel location is within 3rd angle
				else if ((currentLocation < maxAngle3) && (currentLocation > minAngle3))
				{
					analogWrite(solarLED, 150);	//Turn on the LED at 150/255
					nodeStatus[6] = 3;
				}
				else
				{
					analogWrite(solarLED, 30);	//Turn on the LED 30/255
					nodeStatus[6] = 1;
				}
			}
			//Else turn off the LED
			else
			{
				analogWrite(solarLED, LOW);
				nodeStatus[6] = 0;
			}
		}
	}
	else if (gameMode == 4)	//Man-Tonomous
	{
		if (!complete)
		{

			potValL = analogRead(potInputL);
			potValR = analogRead(potInputR);

			int potVal = 0;
			if (potValR < 450 || potValR > 550) potVal = potValR;
			else potVal = potValL;

			if (potVal > 550)
			{
				digitalWrite(dirPin, HIGH);
				if (potVal > 800) potVal = 1000;

				int stepDelay = map(potVal, 550, 1000, minSpeed, maxSpeed);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation++;
				if (currentLocation > maxSteps) currentLocation = 0;

			}
			else if (potVal < 450)
			{
				digitalWrite(dirPin, LOW);
				if (potVal < 200)potVal = 0;
				int stepDelay = map(potVal, 450, 0, minSpeed, maxSpeed);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation--;
				if (currentLocation < 0) currentLocation = (maxSteps - 1);
			}
			//Else the handle is in neutral
			else
			{
				if (!reportSent)	//If report not sent
				{
					xbReport();
					reportSent = true;													//Reset flag
				}
			}

			//Angle Range Variables
			int maxAngle1 = 0;
			int minAngle1 = 0;

			//If sun location is 0 do special math
			if (sunLocation = 0)
			{
				maxAngle1 = 0 + angle_1;		//Set max angle starting from 0
				minAngle1 = 1600 - angle_1;		//Set min angle starting from 1600
			}
			else
			{
				maxAngle1 = sunLocation + angle_1;	//Calculate Max angle
				minAngle1 = sunLocation - angle_1;	//Calculate Min angle
			}

			//If current panel location is within greatest angle
			if ((currentLocation < maxAngle1) && (currentLocation > minAngle1))
			{
				//Angle Range Variables
				int maxAngle2 = maxAngle1 - angle_2;
				int minAngle2 = minAngle1 + angle_2;
				int maxAngle3 = maxAngle1 - angle_3;
				int minAngle3 = minAngle1 + angle_3;

				//If current panel location is within 2nd angle
				if ((currentLocation < maxAngle2) && (currentLocation > minAngle2))
				{
					analogWrite(solarLED, 50);	//Turn on the LED at 50/255
					nodeStatus[6] = 2;
				}
				//If current panel location is within 3rd angle
				else if ((currentLocation < maxAngle3) && (currentLocation > minAngle3))
				{
					analogWrite(solarLED, 150);	//Turn on the LED at 150/255
					nodeStatus[6] = 3;
				}
				else
				{
					analogWrite(solarLED, 30);	//Turn on the LED 30/255
					nodeStatus[6] = 1;
				}
			}
			//Else turn off the LED
			else
			{
				analogWrite(solarLED, LOW);
				nodeStatus[6] = 0;
			}
		}
	}
	else if (gameMode == 5)	//Manual Mode
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			reportSent = false;
			complete = false;
			fullPull = false;
			nodeStatus[6] = 0;
			nodeStatus[7] = 0;
			nodeStatus[4] = 0;
		}

		if (!complete)
		{

			potValL = analogRead(potInputL);
			potValR = analogRead(potInputR);

			int potVal = 0;
			if (potValR < 450 || potValR > 550) potVal = potValR;
			else potVal = potValL;

			if (potVal > 550)
			{
				digitalWrite(dirPin, HIGH);
				if (potVal > 800) potVal = 1000;

				int stepDelay = map(potVal, 550, 1000, minSpeed, maxSpeed);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation++;
				if (currentLocation > maxSteps) currentLocation = 0;

			}
			else if (potVal < 450)
			{
				digitalWrite(dirPin, LOW);
				if (potVal < 200)potVal = 0;
				int stepDelay = map(potVal, 450, 0, minSpeed, maxSpeed);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation--;
				if (currentLocation < 0) currentLocation = (maxSteps - 1);
			}
			//Else the handle is in neutral
			else
			{
				if (!reportSent)	//If report not sent
				{
					xbReport();
					reportSent = true;													//Reset flag
				}
			}

			//Angle Range Variables
			int maxAngle1 = 0;
			int minAngle1 = 0;

			//If sun location is 0 do special math
			if (sunLocation = 0)
			{
				maxAngle1 = 0 + angle_1;		//Set max angle starting from 0
				minAngle1 = 1600 - angle_1;		//Set min angle starting from 1600
			}
			else
			{
				maxAngle1 = sunLocation + angle_1;	//Calculate Max angle
				minAngle1 = sunLocation - angle_1;	//Calculate Min angle
			}

			//If current panel location is within greatest angle
			if ((currentLocation < maxAngle1) && (currentLocation > minAngle1))
			{
				//Angle Range Variables
				int maxAngle2 = maxAngle1 - angle_2;
				int minAngle2 = minAngle1 + angle_2;
				int maxAngle3 = maxAngle1 - angle_3;
				int minAngle3 = minAngle1 + angle_3;

				//If current panel location is within 2nd angle
				if ((currentLocation < maxAngle2) && (currentLocation > minAngle2))
				{
					analogWrite(solarLED, 50);	//Turn on the LED at 50/255
					nodeStatus[6] = 2;
				}
				//If current panel location is within 3rd angle
				else if ((currentLocation < maxAngle3) && (currentLocation > minAngle3))
				{
					analogWrite(solarLED, 150);	//Turn on the LED at 150/255
					nodeStatus[6] = 3;
				}
				else
				{
					analogWrite(solarLED, 30);	//Turn on the LED 30/255
					nodeStatus[6] = 1;
				}
			}
			//Else turn off the LED
			else
			{
				analogWrite(solarLED, LOW);
				nodeStatus[6] = 0;
			}
		}
	}
	else if (gameMode == 6)	//End
	{

	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}
	}
	else if (gameMode == 8)	//Debug Mode
	{
		if (gameModeChanged)
		{
			Serial.println("DEBUG MODE");
			digitalWrite(solarLED, HIGH);
			delay(500);
			digitalWrite(solarLED, LOW);
			delay(500);
			digitalWrite(solarLED, HIGH);
			delay(500);
			digitalWrite(solarLED, LOW);
			gameModeChanged = false;
			complete = false;
		}

		if (!complete)
		{
			
			potValL = analogRead(potInputL);
			//delay(10);
			potValR = analogRead(potInputR);
			//delay(10);

			
			//Serial.print("Left = ");
			//Serial.println(potValL);
			//Serial.print("Right = ");
			//Serial.println(potValR);


			int potVal = 0;
			if (potValR < 450 || potValR > 550) potVal = potValR;
			else potVal = potValL;

			//Serial.print("Pot Value = ");
			//Serial.println(potVal);
			//delay(100);

			if (potVal > 550)
			{
				digitalWrite(dirPin, HIGH);
				if (potVal > 800) potVal = 1000;

				int stepDelay = map(potVal, 550, 1000, minSpeed, maxSpeed);

				//Serial.println("Direction = CCW");
				//Serial.print("Delay = ");
				//Serial.println(stepDelay);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation++;
				if (currentLocation > maxSteps) currentLocation = 0;

			}
			else if (potVal < 450)
			{
				digitalWrite(dirPin, LOW);
				if (potVal < 200)potVal = 0;
				int stepDelay = map(potVal, 450, 0, minSpeed, maxSpeed);

				//Serial.println("Direction = CW");
				//Serial.print("Delay = ");
				//Serial.println(stepDelay);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation--;
				if (currentLocation < 0) currentLocation = (maxSteps-1);
			}

			int max = sunLocation + angle_1;
			int min = sunLocation - angle_1;

			if (max > maxSteps)
			{
				int limitShift = max - maxSteps;

				if (currentLocation < limitShift)
				{
					int delta = abs((currentLocation + maxSteps) - sunLocation);
					int ledPower = map(delta, 0, angle_1, 255, 0);
					analogWrite(solarLED, ledPower);
				}
				else if (currentLocation > min)
				{
					int delta = abs(sunLocation - currentLocation);
					int ledPower = map(delta, 0, angle_1, 255, 0);
					analogWrite(solarLED, ledPower);
				}
				else
				{
					analogWrite(solarLED, LOW);
				}
			}
			else if ((currentLocation > min) && (currentLocation < max))
			{
				int delta = abs(sunLocation - currentLocation);
				int ledPower = map(delta, 0, angle_1, 255, 0);
				analogWrite(solarLED, ledPower);
			}
			else
			{
				analogWrite(solarLED, LOW);
			}
		}

	}
	else  //Reset	
	{
		fieldReset();
		if (gameModeChanged)
		{
			messagesRecieved = 0;
			messagesSent = 0;

			solidColor(off, 0, 0, stripLength);
			towerSelected = false;
			updateInputs();
			gameModeChanged = false;
		}
	}

}

//Game Play Test Mode - All towers randomly respond 
void gamePlayRandomTest()
{
	if (gameMode == 1)	//Ready
	{
		if (gameModeChanged)
		{
			wipeColor(yellow, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			gameModeChanged = false;
			complete = false;
		}
	}
	else if (gameMode == 2)	//Start
	{
		//not used at this time
	}
	else if (gameMode == 3)	//Autonomous
	{
		if (gameModeChanged)
		{
			solidColor(blue, 0, 0, stripLength);
			gameModeChanged = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, LOW);
			}
		}


		if (!complete)
		{
			//Auto Test Code
		}
	}
	else if (gameMode == 4)	//Man-Tonomous
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			complete = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
		}

		if (!complete)
		{
			//ManTon Test Code
		}
	}
	else if (gameMode == 5)	//Manual Mode
	{
		if (gameModeChanged)
		{
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			gameModeChanged = false;
			complete = false;
		}

		if (!complete)
		{
			//Manual Test Coe
		}
	}
	else if (gameMode == 6)	//End
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}
	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}

	}
	else if (gameMode == 8)	//Debug Mode
	{
		if (gameModeChanged)
		{
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(yellow, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
		}

		//Debug Test code
	}
	else  //Reset	
	{
		fieldReset();
	}
}

//Game Play Test Mode - All Towers Respond at the same time with default color
void gamePlaySpeedTest()
{
	if (gameMode == 1)	//Ready
	{
		if (gameModeChanged)
		{
			homePanel();
			gameModeChanged = false;
			fullPull = false;
			nodeStatus[6] = 0;
			nodeStatus[7] = 0;
			complete = false;
		}
	}
	else if (gameMode == 2)	//Start
	{
		//not used at this time
	}
	else if (gameMode == 3)	//Autonomous
	{
		if (gameModeChanged)
		{
			solidColor(blue, 0, 0, stripLength);
			gameModeChanged = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, LOW);
			}
		}
		if (!complete)
		{
			int potVal = 1000;

			if (potVal > 550)
			{
				digitalWrite(dirPin, HIGH);
				if (potVal > 800) potVal = 1000;

				int stepDelay = map(potVal, 550, 1000, minSpeed, maxSpeed);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation++;
				if (currentLocation > maxSteps) currentLocation = 0;

			}
			else if (potVal < 450)
			{
				digitalWrite(dirPin, LOW);
				if (potVal < 200)potVal = 0;
				int stepDelay = map(potVal, 450, 0, minSpeed, maxSpeed);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation--;
				if (currentLocation < 0) currentLocation = (maxSteps - 1);
			}
			//Else the handle is in neutral
			else
			{
				if (!reportSent)	//If report not sent
				{
					xbReport();
					reportSent = true;													//Reset flag
					complete = true;
				}
			}

			//Angle Range Variables
			int maxAngle1 = 0;
			int minAngle1 = 0;

			//If sun location is 0 do special math
			if (sunLocation = 0)
			{
				maxAngle1 = 0 + angle_1;		//Set max angle starting from 0
				minAngle1 = 1600 - angle_1;		//Set min angle starting from 1600
			}
			else
			{
				maxAngle1 = sunLocation + angle_1;	//Calculate Max angle
				minAngle1 = sunLocation - angle_1;	//Calculate Min angle
			}

			//If current panel location is within greatest angle
			if ((currentLocation < maxAngle1) && (currentLocation > minAngle1))
			{
				//Angle Range Variables
				int maxAngle2 = maxAngle1 - angle_2;
				int minAngle2 = minAngle1 + angle_2;
				int maxAngle3 = maxAngle1 - angle_3;
				int minAngle3 = minAngle1 + angle_3;

				//If current panel location is within 2nd angle
				if ((currentLocation < maxAngle2) && (currentLocation > minAngle2))
				{
					analogWrite(solarLED, 50);	//Turn on the LED at 50/255
					nodeStatus[6] = 2;
				}
				//If current panel location is within 3rd angle
				else if ((currentLocation < maxAngle3) && (currentLocation > minAngle3))
				{
					analogWrite(solarLED, 150);	//Turn on the LED at 150/255
					nodeStatus[6] = 3;
					potVal = 500;
				}
				else
				{
					analogWrite(solarLED, 30);	//Turn on the LED 30/255
					nodeStatus[6] = 1;
				}
			}
			//Else turn off the LED
			else
			{
				analogWrite(solarLED, LOW);
				nodeStatus[6] = 0;
			}
		}
	}
	else if (gameMode == 4)	//Man-Tonomous
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			complete = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
		}
		if (!complete)
		{

		}
	}
	else if (gameMode == 5)	//Manual Mode
	{
		if (gameModeChanged)
		{
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			gameModeChanged = false;
			complete = false;
		}

		if (!complete)
		{
			int potVal = 1000;

			if (potVal > 550)
			{
				digitalWrite(dirPin, HIGH);
				if (potVal > 800) potVal = 1000;

				int stepDelay = map(potVal, 550, 1000, minSpeed, maxSpeed);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation++;
				if (currentLocation > maxSteps) currentLocation = 0;

			}
			else if (potVal < 450)
			{
				digitalWrite(dirPin, LOW);
				if (potVal < 200)potVal = 0;
				int stepDelay = map(potVal, 450, 0, minSpeed, maxSpeed);

				digitalWrite(stepPin, HIGH);
				delay(stepDelay);

				digitalWrite(stepPin, LOW);
				delay(stepDelay);

				currentLocation--;
				if (currentLocation < 0) currentLocation = (maxSteps - 1);
			}
			//Else the handle is in neutral
			else
			{
				if (!reportSent)	//If report not sent
				{
					xbReport();
					reportSent = true;													//Reset flag
					complete = true;
				}
			}

			//Angle Range Variables
			int maxAngle1 = 0;
			int minAngle1 = 0;

			//If sun location is 0 do special math
			if (sunLocation = 0)
			{
				maxAngle1 = 0 + angle_1;		//Set max angle starting from 0
				minAngle1 = 1600 - angle_1;		//Set min angle starting from 1600
			}
			else
			{
				maxAngle1 = sunLocation + angle_1;	//Calculate Max angle
				minAngle1 = sunLocation - angle_1;	//Calculate Min angle
			}

			//If current panel location is within greatest angle
			if ((currentLocation < maxAngle1) && (currentLocation > minAngle1))
			{
				//Angle Range Variables
				int maxAngle2 = maxAngle1 - angle_2;
				int minAngle2 = minAngle1 + angle_2;
				int maxAngle3 = maxAngle1 - angle_3;
				int minAngle3 = minAngle1 + angle_3;

				//If current panel location is within 2nd angle
				if ((currentLocation < maxAngle2) && (currentLocation > minAngle2))
				{
					analogWrite(solarLED, 50);	//Turn on the LED at 50/255
					nodeStatus[6] = 2;
				}
				//If current panel location is within 3rd angle
				else if ((currentLocation < maxAngle3) && (currentLocation > minAngle3))
				{
					analogWrite(solarLED, 150);	//Turn on the LED at 150/255
					nodeStatus[6] = 3;
					potVal = 500;
				}
				else
				{
					analogWrite(solarLED, 30);	//Turn on the LED 30/255
					nodeStatus[6] = 1;
				}
			}
			//Else turn off the LED
			else
			{
				analogWrite(solarLED, LOW);
				nodeStatus[6] = 0;
			}
		}

	}
	else if (gameMode == 6)	//End
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}
	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}

	}
	else if (gameMode == 8)	//Debug Mode
	{
		if (gameModeChanged)
		{
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(yellow, 0, 1, 0, stripLength);
			wipeColor(blue, 0, 1, 0, stripLength);
			gameModeChanged = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
		}

		int oldPullValue = currentPullValue;
		currentPullValue = getScaledAnalog();
		if (currentPullValue >= (maxRange - 1)) fullPull = true;


		if (oldPullValue != currentPullValue)
		{
			if (fullPull)
			{
				nodeStatus[7] = 0;
				alarmState = false;
				report(0, commandNode);
				wipeColor(green, 0, 0, firstPixel(2), lastPixel(2));
				fullPull = false;
				delay(10);
			}
			else
			{
				nodeStatus[7] = 1;
				alarmState = true;
				report(0, commandNode);
				wipeColor(red, 0, 0, firstPixel(2), lastPixel(2));
			}
		}

		//If beam is broken
		byte oldState = inputStates[0];
		updateInputs();

		if (oldState != inputStates[0])
		{
			if (checkInput(0))
			{
				nodeStatus[7] = 8;
				report(0, commandNode);
				testedState = true;
				wipeColor(green, 0, 0, firstPixel(1), lastPixel(1));
			}
			else
			{
				nodeStatus[7] = 9;
				report(0, commandNode);
				testedState = false;
				wipeColor(red, 0, 0, firstPixel(1), lastPixel(1));
			}
		}
	}
	else  //Reset	
	{
		fieldReset();
	}
}

//Sends out one message when placed in Start
void NetworkResponseTest()
{
	if (gameMode == 1)	//Ready
	{
		if (gameModeChanged)
		{
			wipeColor(red, 0, 1, 0, stripLength);
			wipeColor(white, 0, 1, 0, stripLength);
			gameModeChanged = false;
			complete = false;
		}
	}
	else if (gameMode == 2)	//Start
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			complete = false;
			messagesSent++;
			nodeStatus[7] = messagesSent;
			nodeStatus[6] = messagesRecieved;
			report(0, commandNode);
		}

	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			report(0, commandNode);
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
		}

	}
	else  //Reset	
	{
		if (gameModeChanged)
		{
			messagesRecieved = 0;
			messagesSent = 0;
			nodeStatus[7] = messagesSent;
			nodeStatus[6] = messagesRecieved;

			solidColor(off, 0, 0, stripLength);
			towerSelected = false;
			updateInputs();
			gameModeChanged = false;
		}
	}

}

//Sends out a burst of 255 messages when placed in Start
void NetworkSpeedTest()
{
	if (gameMode == 1)	//Ready
	{
		if (gameModeChanged)
		{
			wipeColor(red, 0, 1, 0, stripLength);
			wipeColor(yellow, 0, 1, 0, stripLength);
			gameModeChanged = false;
			complete = false;
		}
	}
	else if (gameMode == 2)	//Start
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			complete = false;
		}

		if (messagesSent < 255)
		{
			messagesSent++;
			nodeStatus[7] = messagesSent;
			nodeStatus[6] = messagesRecieved;
			report(0, commandNode);
		}
	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			report(0, commandNode);
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
		}

	}
	else  //Reset	
	{
		if (gameModeChanged)
		{
			messagesRecieved = 0;
			messagesSent = 0;
			nodeStatus[7] = messagesSent;
			nodeStatus[6] = messagesRecieved;

			solidColor(off, 0, 0, stripLength);
			towerSelected = false;
			updateInputs();
			gameModeChanged = false;
		}
	}

}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>NeoPixel Methods</method>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region NeoPixel Methods

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>neoPix() </method>
///
/// <summary>Used to Parse Neopixel Commands </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void neoPix()
{
	//Set Local Variables
	uint32_t _color = colorValue(canIn[1]);
	uint8_t _firstPixel = 0;
	uint8_t _lastPixel = stripLength;
	uint8_t _mode = canIn[2];
	uint8_t _ring = canIn[4];
	uint8_t _wait = canIn[5];

	if (canIn[3] != 0)							//If Byte 3 is not 0 (all pixels)
	{
		_firstPixel = firstPixel(canIn[3]);		//Set firsPixel
		_lastPixel = lastPixel(canIn[3]);		//Set lastPixel
	}

	if (_mode == 0)															//If Mode is 0
	{
		solidColor(_color, _wait, 0, stripLength);							//Turn on Ligth Solid Color
	}
	else if (_mode == 1)													//If Mode is 1
	{
		solidColor(_color, _wait, _firstPixel, _lastPixel);					//Turn ring(s) solid color
	}
	else if (_mode == 2)													//Mode is 2
	{
		flashColorLatch(_color, _wait, _ring, _firstPixel, _lastPixel);		//Flash Color and latch to that color
	}
	else if (_mode == 3)													//If Mode is 3
	{
		flashColor(_color, _wait, _ring, _firstPixel, _lastPixel);			//Flash Color and turn off
	}
	else if (_mode == 4)													//If Mode is 4
	{
		wipeColor(_color, _wait, _ring, _firstPixel, _lastPixel);			//Wipe color up the strip
	}
	else if (_mode == 5)													//If Mode is 5
	{
		testPattern();														//Run Test Pattern
	}

	nodeStatus[1] = _color;													//Update color status
	nodeStatus[2] = _mode;													//Update mode status
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	colorValue() </method>
///
/// <summary>	Used to parse the color valuse based on current message </summary>
///
/// <param name="clrVlu">	current color value in message </param>
///
/// <returns>	returns color value for NeoPixels </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
uint32_t colorValue(uint8_t clrVlu)
{
	if (clrVlu == 1) return red;
	else if (clrVlu == 2) return green;
	else if (clrVlu == 3) return yellow;
	else if (clrVlu == 4) return blue;
	else if (clrVlu == 5) return white;
	else return off;
}
uint8_t colorCode(uint32_t _color)
{
	if (_color == red) return 1;
	else if (_color == green) return 2;
	else if (_color == yellow) return 3;
	else if (_color == blue) return 4;
	else if (_color == white) return 5;
	else return 0;
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	firstPixel() </method>
///
/// <summary>	calculates the first pixel location based on current message. </summary>
///
/// <param name="ringValue">	The ring value in current message </param>
///
/// <returns>	first pixel location </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
uint8_t firstPixel(uint8_t ringValue)
{
	uint8_t _firstPixel;

	//If ring value is greater than 0 then calculate the first pixel
	if (ringValue > 0)
	{
		_firstPixel = (pixPerRing * ringValue) - pixPerRing;
	}
	//Else it will be zero
	else
	{
		_firstPixel = 0;
	}

	//Return Value
	return _firstPixel;
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	lastPixel() </method>
///
/// <summary>	calculates the last pixel location based on current message </summary>
///
/// <param name="ringValue">	The ring value in current message </param>
///
/// <returns>	Returns last pixel location </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
uint8_t lastPixel(uint8_t ringValue)
{
	if (ringValue > 0) return 8 * ringValue;
	else return stripLength;
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	testPattern() </method>
///
/// <summary>	runs a short little light show to make sure all colors are working </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void testPattern()
{
	nodeStatus[1] = 6;	//Color
	nodeStatus[2] = 5;	//Mode
	solidColor(white, 500, 0, stripLength);
	delay(100);
	solidColor(red, 500, 0, stripLength);
	delay(100);
	solidColor(green, 500, 0, stripLength);
	delay(100);
	solidColor(yellow, 500, 0, stripLength);
	delay(100);
	solidColor(blue, 500, 0, stripLength);
	delay(100);
	solidColor(off, 500, 0, stripLength);
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	wipeColor() </method>
///
/// <summary>	Fills in neopixels one after another till the ring or stip is filled </summary>
///
/// <param name="_color">  		color </param>
/// <param name="_wait">	   	wait between pixels </param>
/// <param name="_times">   	number of times the wipe should repeat </param>
/// <param name="_startPix">	first pixel to light </param>
/// <param name="_endPix">  	last pixel to light </param>
////////////////////////////////////////////////////////////////////////////////////////////////////
void wipeColor(uint32_t _color, uint8_t _wait, uint8_t _times, uint8_t _startPix, uint8_t _endPix)
{
	nodeStatus[1] = colorCode(_color);
	nodeStatus[2] = 4;

	//If wait is zero set to defualt
	if (_wait == 0) _wait = 20;

	//if time is zero set to 1 so it will at least do it once
	if (_times == 0) _times = 1;

	//loop for the command number of times
	for (uint8_t i = 0; i < _times; i++)
	{
		//If looping more than once turn off the strip
		if (_times > 1)
		{
			//turn off pixels one at a time starting with the first and ending with the last
			for (uint8_t i = _startPix; i < _endPix; i++)
			{
				light.setPixelColor(i, off);	//Set pixel to off
				light.show();					//Turn it on
				delay(_wait);					//Wait
			}
		}
		//turn on pixels one at a time starting with the first and ending with the last
		for (uint8_t i = _startPix; i < _endPix; i++)
		{
			light.setPixelColor(i, _color);	//Set pixel to the color
			light.show();					//Turn it on
			delay(_wait);					//Wait
		}

	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	solidColor </method>
///
/// <summary>	turns on all pixels in a ring or strip to the same color at the same time </summary>
///
/// <param name="_color">  		color </param>
/// <param name="_wait">	   	wait before the next command </param>
/// <param name="_startPix">	first pixel to light </param>
/// <param name="_endPix">  	last pixel to light </param>
////////////////////////////////////////////////////////////////////////////////////////////////////
void solidColor(uint32_t _color, uint8_t _wait, uint8_t _startPix, uint8_t _endPix)
{
	nodeStatus[1] = colorCode(_color);
	nodeStatus[2] = 1;

	//If wait is 0 set to default
	if (_wait == 0) _wait = 10;

	//Set pixels to all the same color starting with the first and ending with the last
	for (uint16_t i = _startPix; i < _endPix; i++)
	{
		light.setPixelColor(i, _color);
	}
	light.show();		//Let there be light
	delay(_wait);		//Wait
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	flashColor() </method>
///
/// <summary>	Flashes one color on on and off a set number of times </summary>
///
/// <param name="_color">  		color </param>
/// <param name="_wait">	   	wait between pixels </param>
/// <param name="_times">   	number of times the wipe should repeat </param>
/// <param name="_startPix">	first pixel to light </param>
/// <param name="_endPix">  	last pixel to light </param>
////////////////////////////////////////////////////////////////////////////////////////////////////
void flashColor(uint32_t _color, uint8_t _wait, uint8_t _times, uint8_t _startPix, uint8_t _endPix)
{
	nodeStatus[1] = colorCode(_color);
	nodeStatus[2] = 3;

	//If Wait is 0 set default
	if (_wait == 0) _wait = 350;

	//If times is 0 set to 1 so it will at least do it once
	if (_times == 0) _times = 1;

	//Loop the commanded number of times
	for (int i = 0; i < _times; i++)
	{
		solidColor(_color, _wait, _startPix, _endPix);	//Set all pixels on that color
		solidColor(off, _wait, _startPix, _endPix);		//Set all pixels off
	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	flashColorLatch() </method>
///
/// <summary>	Flashes one color on on and off a set number of times
/// 			and then latches that color </summary>
///
/// <param name="_color">  		color </param>
/// <param name="_wait">	   	wait between pixels </param>
/// <param name="_times">   	number of times the wipe should repeat </param>
/// <param name="_startPix">	first pixel to light </param>
/// <param name="_endPix">  	last pixel to light </param>
////////////////////////////////////////////////////////////////////////////////////////////////////
void flashColorLatch(uint32_t _color, uint8_t _wait, uint8_t _times, uint8_t _startPix, uint8_t _endPix)
{
	nodeStatus[1] = colorCode(_color);
	nodeStatus[2] = 2;

	//If Wait is 0 set default
	if (_wait == 0) _wait = 350;

	//If times is 0 set to 1 so it will at least do it once
	if (_times == 0) _times = 1;

	//Loop the commanded number of times
	for (int i = 0; i < _times; i++)
	{
		solidColor(_color, _wait, _startPix, _endPix);	//Set all pixels on that color
		solidColor(off, _wait, _startPix, _endPix);		//Set all pixels off
	}

	solidColor(_color, _wait, _startPix, _endPix);		//Set all pixels on that color
}
#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>Status and Report Methods</method>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region Status and Report Methods

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	Reports() </method>
///
/// <summary>Used to Report Status of Node </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void report(uint8_t check, uint32_t dAddress)
{
	if (check) updateInputs();									//Update Inputs
	uint32_t dA = address(dAddress, nodeID);					//Get address
	int stat = CAN_FAIL;
	int timeOut = 0;

	if (dAddress != nodeID)
	{
		uint32_t dA = address(dAddress, nodeID);				//Get address
		stat = CAN.sendMsgBuf(dA, 0, sizeof(nodeStatus), nodeStatus, 2);	//Send message using only one buffer

		//do
		//{
		//	timeOut++;
		//	stat = CAN.sendMsgBuf(dA, 0, sizeof(nodeStatus), nodeStatus);	//Send message using only one buffer
		//} while ((stat != CAN_OK) && (timeOut < TIMEOUTVALUE));
	}
	else
	{
		Serial.print(commandNode);
		Serial.print(",");
		for (int i = 0; i < sizeof(nodeStatus); i++)
		{
			Serial.print(nodeStatus[i]);
			Serial.print(",");
		}
		Serial.println();
	}
}

void xbReport()
{
	xbSerial.print("$,");
	xbSerial.print(nodeID);
	xbSerial.print(",");
	for (int i = 0; i < sizeof(nodeStatus); i++)
	{
		xbSerial.print(nodeStatus[i]);
		xbSerial.print(",");
	}
	xbSerial.println();

}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	updateInputs </method>
///
/// <summary>	Updates all inputs pins on this node </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void updateInputs()
{
	//Loop through all pins
	for (uint8_t i = 0; i < sizeof(inputPins); i++)
	{
		//If pin is not 99 it is used
		if (inputPins[i] != 99)
		{
			checkInput(i);	//Check input
		}
	}

}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	checkInput() </method>
///
/// <summary>	Checks state of given inputs and updates node status 
/// 			assumes input goes to ground to close circuit</summary>
///
/// <param name="input">	input pin to check </param>
///
/// <returns>	true if input is low, false if input is high </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
boolean checkInput(uint8_t input)
{
	//Read pin state
	uint8_t newState = digitalRead(inputPins[input]);
	//Reverse Logic
	//if (newState == 0) newState = 1;
	//else newState = 0;

	//Get old state
	uint8_t oldState = inputStates[input];

	//If button state has changed 
	if (oldState != newState)
	{
		if (newState == 1)
		{
			inputStates[input] = 1;							//Update input status
			nodeStatus[4] |= (1 << input);	//Update node status
			return true;
		}
		else
		{
			inputStates[input] = 0;							//Update input status
			nodeStatus[4] &= ~(1 << input);	//Update node status
			return false;
		}
	}
	else
	{
		if (newState == 1) return true;
		else return false;
	}
}

void fieldReset()
{
	if (gameModeChanged)
	{
		solidColor(off, 0, 0, stripLength);
		towerSelected = false;
		updateInputs();
		gameModeChanged = false;

		currentLocation = 0;			//Current panel location in steps
		sunTower = 0;					//Sun's tower number
		sunLocation = 0;				//Sun's location in steps
		alignment = 0;					//0 = not aligned, 1 = 10 degrees, 2 = 5 degrees, 3 = aligned

		alarmState = false;
		sunState = false;
		testedState = false;

		if ((nodeID == 3) || (nodeID == 8))
		{
			digitalWrite(autoPin, LOW);
			digitalWrite(manPin, LOW);
		}
	}
}
#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>CANBUS Methods</method>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region CANBUS Methods

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	address() </method>
///
/// <summary>	calculates the address given the sending node and destination node's ID </summary>
///
/// <param name="destination">	Destination Node's ID </param>
/// <param name="id">		  	Sending Node's ID </param>
///
/// <returns>	The combined Address  </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
uint32_t address(uint32_t _destination, uint32_t _id)
{
	return ((_id << 5) | _destination);		//Sending node is the 5 last bits, destination is the first 5 bits
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	sendingNode() </method>
///
/// <summary>	gets the sending nodes ID </summary>
///
/// <returns>	returns the sending nodes ID </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
uint32_t sendingNode()
{
	if (destNode != nodeID)
	{
		uint32_t node = CAN.getCanId();	//Get whole ID
		return node >> 5;				//Last 5 bits are the sending nodes ID
	}
	else return nodeID;
}

void ChangeBaudRate(byte rate)
{
	Serial.print("CAN-BUS Startup: ");
	//If CANBUS begins 
	if (CAN_OK == CAN.begin(rate))						// init can bus : baudrate = 50k
	{
		wipeColor(green, 0, 0, firstPixel(3), lastPixel(3));	//If okay light ring green
		Serial.print(rate);
		Serial.println(" - OK");									//Report Okay

	}
	else
	{
		Serial.println("Not Good, check your pin# and connections");	//Report a problem
		wipeColor(yellow, 0, 0, firstPixel(3), lastPixel(3));			//If not okay light ring yellow												//If startup failed try again
	}
}
#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>Solar Panel</method>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region Solar Panel

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	homePanel() </method>
///
/// <summary>	used to home the solar panel </summary>
///
/// <returns>	nothing </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
void homePanel()
{
	if (checkInput(0))
	{
		for (int i = 0; i < 200; i++)
		{
			//Serial.println(checkInput(0));
			digitalWrite(dirPin, LOW); //High = CCW

			digitalWrite(stepPin, HIGH);
			delay(3);

			digitalWrite(stepPin, LOW);
			delay(3);

		}
	}

	delay(100);


	while (!checkInput(0))
	{
		//Serial.println(checkInput(0));
		digitalWrite(dirPin, HIGH); //High = CCW

		digitalWrite(stepPin, HIGH);
		delay(3);

		digitalWrite(stepPin, LOW);
		delay(3);

		currentLocation = 90;
	};

	delay(500);

	while (currentLocation != 0)
	{
		digitalWrite(dirPin, HIGH); //High = CCW

		digitalWrite(stepPin, HIGH);
		delay(10);

		digitalWrite(stepPin, LOW);
		delay(10);

		currentLocation--;
	}

	nodeStatus[6] = 9;
	xbReport();
	Serial.println("-----Panel Homed-----");
	
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	towerLocation() </method>
///
/// <summary>	used to home the solar panel </summary>
///
/// <returns>	tower location in steps </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
int towerLocation(int towerNum)
{
	homePanel();

	if (towerNum == 1) sunLocation = 1318;
	else if (towerNum == 2) sunLocation = 1450;
	else if (towerNum == 3) sunLocation = 0;
	else if (towerNum == 4) sunLocation = 150;
	else if (towerNum == 5) sunLocation = 282;
	else if (towerNum == 6) sunLocation = 518;
	else if (towerNum == 7) sunLocation = 650;
	else if (towerNum == 8) sunLocation = 800;
	else if (towerNum == 9) sunLocation = 950;
	else sunLocation = 1082;

	Serial.print("Sun Location = ");
	Serial.println(sunLocation);

	int half = 800;
	if(sunLocation >= 800) half = sunLocation - 800;
	else half = sunLocation + 800;

	Serial.print("Half = ");
	Serial.println(half);

	while (currentLocation != half)
	{
		digitalWrite(dirPin, LOW); //High = CCW

		digitalWrite(stepPin, HIGH);
		delay(2);

		digitalWrite(stepPin, LOW);
		delay(2);

		currentLocation--;
		if (currentLocation < 0) currentLocation = (maxSteps - 1) ;
		//Serial.println(currentLocation);
	}
	
	return sunLocation;
}

#pragma endregion



/*********************************************************************************************************
END FILE
*********************************************************************************************************/
