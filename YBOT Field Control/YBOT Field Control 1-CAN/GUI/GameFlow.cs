using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace YBOT_Field_Control_2016
{
    public partial class GameControl
    {
        #region Game and Scoring Variables



        #endregion

        //Initiate game class
        public void begin()
        {
            this.red.reset();                   //Reset Red variables
            this.green.reset();                 //Reset Green variables
            gameMode = this.fc.ChangeGameMode(GameModes.reset);

            this.fc.ClearNodeState();

            sunTower = 0;
            solarAligned = false;
            for (int i = 0; i < emergencyTowers.Length; i++)
            {
                eTowers[i] = emergencyTowers[i];
            }
            //emerTower = -1;
            alarmCouter = 3;
            solarChanged = false;
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void GameStartUp()
        {
            begin();        //reset variables and flags       

            practiceMode = false;
            gameMode = this.fc.ChangeGameMode(GameModes.ready);

            //string str = ("7,1,3,");
            //this.fc.SendMessage(solarPanel, str);
            //Thread.Sleep(20);

            //Wait for the solar panel to home.
            //Thread.Sleep(10000);

            ChangeSunTower();

            //Ring Bell
            this.fc.RingBell();
            Thread.Sleep(200);

            //Set to Automode
            gameMode = this.fc.ChangeGameMode(GameModes.autonomous);
            GameLog("Game Started");

            //Turn on Robot Transmiters
            //this.fc.RobotTransmitters("both", State.on, State.on);
            //GameLog("Transmitters On");
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void GameStartUp(GameModes mode)
        {
            begin();        //reset variables and flags       

            practiceMode = false;
            gameMode = this.fc.ChangeGameMode(GameModes.ready);

            ////Home The Solar Panel
            //string str = ("7,1,3,");
            //this.fc.SendMessage(solarPanel, str);
            //Thread.Sleep(20);

            //Wait for the solar panel to home.
            //Thread.Sleep(10000);

            ChangeSunTower();

            //Ring Bell
            this.fc.RingBell();
            Thread.Sleep(200);

            //Set to Automode
            gameMode = this.fc.ChangeGameMode(mode);
            GameLog("Game Started");

            //Turn on Robot Transmiters
            //if (mode == GameModes.autonomous) this.fc.RobotTransmitters("both", State.on, State.on);
            //else this.fc.RobotTransmitters("both", State.off, State.on);
            //GameLog("Transmitters On");

            //If practice mode
            if (mode == GameModes.debug)
            {
                gameMode = this.fc.ChangeGameMode(GameModes.debug);
            }
        }

        /// <summary>
        /// End a Game
        /// </summary>
        public void GameShutDown()
        {
            //Turn off Transmitters
            //this.fc.RobotTransmitters("both", State.off, State.off);
            //GameLog("Transmitters Off");

            //Sound buzzer
            this.fc.SoundBuzzer();

            GameLog("Towers Off");
            GameLog("Game End");
        }

        /// <summary>
        /// Main Game control :  enters and exits game modes
        /// </summary>
        public void MainGame()
        {
            //If Automode enter AutoMode
            if (gameMode == GameModes.autonomous | gameMode == GameModes.mantonomous)
            {
                if (this.fc.switchMode)
                {
                    this.fc.switchMode = false;
                    GameLog("Start AutoMode");
                }
                else AutoMode();

            }
            //If not autoMode and not Endmode enter MidMode
            else if (gameMode == GameModes.manual)
            {
                //Do this between rounds
                if (this.fc.switchMode)
                {
                    this.fc.switchMode = false;

                    //Pick new sun tower
                    ChangeSunTower();
                    solarChanged = false;
                    
                    //this.fc.RobotTransmitters("both", State.off, State.on); //Turn on transmitter to Manual Mode

                    this.fc.RingBell();                        //Ring bell
                    Thread.Sleep(200);

                    GameLog("AutoMode Over");                   //Update Log
                    GameLog("Transmitters ON");                 //Update Log

                    this.fc.ClearNodeState(true);

                    solarChange.elapsedTime.Start();

                    GetEmerTower();

                }
                else ManualMode();
            }
        }

        public void AutoMode()
        {
            //if solar panel is aligned
            if(this.fc.node[11].byte6 > 0)
            {
                //if aligned start timer
                if(!solarAligned)
                {
                    solarTime.elapsedTime.Reset();
                    solarAligned = true;
                }

                //Calculate score
                int value = 0;
                if (this.fc.node[11].byte6 == 1) value = 4;
                else if (this.fc.node[11].byte6 == 2) value = 7;
                else if (this.fc.node[11].byte6 == 3) value = 10;

                int tempScore = solarTime.elapsedTime.Elapsed.Seconds * value;
                this.joint.autoSolarPanelScore = tempScore;          
             }

            int testedTowers = 0;
            int cycledTowers = 0;

            //see if the towers have been tested or cycled
            for (int i = 0; i < emergencyTowers.Length; i++)
            {
                int tower = emergencyTowers[i];
                if (this.fc.node[tower].tested == true) testedTowers++;
                if (this.fc.node[tower].deviceCycled == true) cycledTowers++;
            }

            joint.autoEmergencyTowerCycled = cycledTowers;
            joint.autoTowerTested = testedTowers;

            //Calculate scores
            if (!this.joint.autoMan)
            {
                int towerScore = (testedTowers * 50) + (cycledTowers * 100);
                this.joint.score = towerScore + this.joint.autoSolarPanelScore;
                this.joint.autoScore = this.joint.score;

                this.red.score = this.joint.score;
                this.red.autoScore = this.red.score;

                this.green.score = this.joint.score;
                this.green.autoScore = this.green.score;
            }

        }

        public void ManualMode()
        {
            //if solar panel is aligned
            if (this.fc.node[11].byte6 > 0)
            {
                //if aligned start timer
                if (!solarAligned)
                {
                    solarTime.elapsedTime.Reset();
                    solarAligned = true;
                }

                //Calculate score
                int value = 0;
                if (this.fc.node[11].byte6 == 1) value = 4;
                else if (this.fc.node[11].byte6 == 2) value = 7;
                else if (this.fc.node[11].byte6 == 3) value = 10;

                int tempScore = solarTime.elapsedTime.Elapsed.Seconds * value;

                //If the time is over a minute pick new tower
                if (solarChange.elapsedTime.Elapsed.Seconds > 59)
                {
                    if (!solarChanged) ChangeSunTower();
                    this.joint.manSolarPanelScore2 = tempScore;
                }
                else
                {
                    this.joint.manSolarPanelScore1 = tempScore;
                }

            }

            //If Alarm has been cleared score the tower
            if (!this.fc.node[emergencyTower].alarmState)
            {
                joint.emergencyCleared++;
                if (joint.emergencyCleared < 4) GetEmerTower();
            }

            //Calculate Scores
            this.joint.manScore = (this.joint.emergencyCleared * 100);
            this.joint.manScore += (this.joint.manSolarPanelScore1 + this.joint.manSolarPanelScore2);
            if (this.joint.emergencyCleared < 4) this.joint.manScore -= 250;

            this.joint.score = (this.joint.autoScore + this.joint.manScore);

            this.red.score = this.joint.score;
            this.red.autoScore = this.red.score;

            this.green.score = this.joint.score;
            this.green.autoScore = this.green.score;

            //if (emerTower == -1)
            //{
            //    GetEmergencyTower();
            //}
            //else if (!fc.node[emerTower].alarmState)
            //{
            //    this.joint.emergencyCleared++;
            //    if (this.joint.emergencyCleared < 4) GetEmergencyTower();
            //}
        }

        /// <summary>
        /// Writes text to log file
        /// </summary>
        /// <param name="text">Text as string</param>
        private void GameLog(string text)
        {
            DateTime now = DateTime.Now;
            string time = now.TimeOfDay.ToString();
            string s = string.Format("{0} : {1}", time, text);
            logBuilder.AppendLine(s);
        }

        public void LogGame()
        {
            string file = string.Format("\\Match {0} - Log", matchNumber.ToString());       //File name
            string folder = string.Format("Matches\\Match {0}", matchNumber.ToString());    //Folder path
            this.lw.Log(logBuilder.ToString(), file, folder);
            this.fc.writeLogs(folder);
            logBuilder.Clear();
        }

        //------------------------------------------------------------------------------------------------\\
        //Current year's game methods
        //------------------------------------------------------------------------------------------------\\

        private void GetEmerTower()
        {
            //Get Next Emergency Tower
            int num = rndNum.Next(0, alarmCouter);
            emergencyTower = eTowers[num];

            //Set Alarm State to true
            this.fc.node[emergencyTower].alarmState = true;

            //Send Alarm to Tower
            string str = null;
            str = ("7,1,2,");
            this.fc.SendMessage(emergencyTower, str);
            Thread.Sleep(20);

            eTowers[num] = 0;
            Array.Reverse(eTowers);
            if (alarmCouter > 0) alarmCouter--;

        }

        private void GetEmergencyTower()
        {
            bool atleastOneTowerTested = false;

            if (emergencyTower == -1) // first emergency tower has not been chosen
            {
                foreach (var tower in emergencyTowers)
                {
                    atleastOneTowerTested = fc.node[tower].tested;
                }
            }
            else
            {
                atleastOneTowerTested = true;
            }

            if (atleastOneTowerTested)
            {
                int towersChecked = 0;
                int currentTower = emergencyTower;

                while ((currentTower == emergencyTower) && (towersChecked != 15))
                {
                    int num = rndNum.Next(0, 3);
                    emergencyTower = emergencyTowers[num];
                    if (fc.node[emergencyTower].tested)
                    {
                        fc.node[emergencyTower].alarmState = true;

                        string str = null;
                        str = ("7,1,2,");
                        this.fc.SendMessage(emergencyTower, str);
                        Thread.Sleep(20);
                        break;
                    }
                    else
                    {
                        emergencyTower = currentTower;
                        towersChecked &= 1 << num;
                    }
                }
            }
        }

        private void ChangeSunTower()
        {
            //Turn off Current Sun Tower
            string str = ("7,0,0,");
            this.fc.SendMessage(sunTower, str);
            Thread.Sleep(20);

            //Tell Solar Panel what the new sun tower is
            sunTower = rndNum.Next(1, 11);
            str = ("7,1,4," + sunTower.ToString());
            this.fc.SendMessage(11, str);
            Thread.Sleep(20);

            //Tell the new sun tower to turn on
            str = ("7,1,1,");
            this.fc.SendMessage(sunTower, str);
            Thread.Sleep(20);

            //Reset Solar Panel Timers and Flags
            solarTime.elapsedTime.Reset();
            solarAligned = false;
            solarChange.elapsedTime.Stop();
            solarChanged = true;
        }
    }
}
