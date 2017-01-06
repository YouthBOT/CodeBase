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
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void GameStartUp()
        {
            begin();        //reset variables and flags       

            practiceMode = false;
            gameMode = this.fc.ChangeGameMode(GameModes.ready);

            //Wait for the solar panel to home.
            while(this.fc.node[11].byte6 != 9)
            {
                Application.DoEvents();
            }

            sunTower = rndNum.Next(1, 11);
            string str = ("7,1,4," + sunTower.ToString());
            this.fc.SendMessage(11, str);
            Thread.Sleep(20);
            str = ("7,1,1,");
            this.fc.SendMessage(sunTower, str);
            Thread.Sleep(20);

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

            //Wait for the solar panel to home.
            while (this.fc.node[11].byte6 != 9)
            {
                Application.DoEvents();
            }

            sunTower = rndNum.Next(1, 11);
            string str = ("7,1,4," + sunTower.ToString());
            this.fc.SendMessage(11, str);
            Thread.Sleep(20);
            str = ("7,1,1,");
            this.fc.SendMessage(sunTower, str);
            Thread.Sleep(20);

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
                    string str = ("7,0,0,");
                    this.fc.SendMessage(sunTower, str);
                    Thread.Sleep(20);
                    sunTower = rndNum.Next(1, 11);
                    str = ("7,1,4," + sunTower.ToString());
                    this.fc.SendMessage(11, str);
                    Thread.Sleep(20);
                    str = ("7,1,1,");
                    this.fc.SendMessage(sunTower, str);
                    Thread.Sleep(20);

                    //this.fc.RobotTransmitters("both", State.off, State.on); //Turn on transmitter to Manual Mode
                    this.fc.RingBell();                        //Ring bell
                    Thread.Sleep(200);
                    GameLog("AutoMode Over");                   //Update Log
                    GameLog("Transmitters ON");                 //Update Log

                    this.fc.ClearNodeState();

                }
                else ManualMode();
            }
        }

        public void AutoMode()
        {
            //Automode Code here
        }

        public void ManualMode()
        {
            //Manual Mode Code
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


    }
}
