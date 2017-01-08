using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;

namespace YBOT_Field_Control_2016
{
    public partial class GameControl : Form
    {           
        public GameControl()
        {
            InitializeComponent();
            this.fc = new Field_Control();
        }

        public GameControl(Field_Control _fc)
        {
            InitializeComponent();
            this.fc = _fc;
        }

        #region Game Controls
        
        #region Penalties

        #region Green Penalties
        private void lblGreenPenalty1_Click(object sender, EventArgs e)
        {
            if (lblGreenPenalty1.BackColor == GameControl.DefaultBackColor)
            {
                lblGreenPenalty1.BackColor = Color.Lime;
                lblGreenPenalty1.ForeColor = Color.Black;
                GD.lblGreenPenalty1.BackColor = Color.Lime;
                GD.lblGreenPenalty1.ForeColor = Color.Black;
                this.green.penalty++;
                this.GameLog("Green Penalty: ADD");
            }
            else
            {
                lblGreenPenalty1.BackColor = GameControl.DefaultBackColor;
                lblGreenPenalty1.ForeColor = Color.Lime;
                GD.lblGreenPenalty1.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenPenalty1.ForeColor = GameControl.DefaultBackColor;
                this.green.penalty--;
                this.GameLog("Green Penalty: Subtract");
            }
        }

        private void lblGreenPenalty2_Click(object sender, EventArgs e)
        {
            if (lblGreenPenalty2.BackColor == GameControl.DefaultBackColor)
            {
                lblGreenPenalty2.BackColor = Color.Lime;
                lblGreenPenalty2.ForeColor = Color.Black;
                GD.lblGreenPenalty2.BackColor = Color.Lime;
                GD.lblGreenPenalty2.ForeColor = Color.Black;
                this.green.penalty++;
                this.GameLog("Green Penalty: ADD");
            }
            else
            {
                lblGreenPenalty2.BackColor = GameControl.DefaultBackColor;
                lblGreenPenalty2.ForeColor = Color.Lime;
                GD.lblGreenPenalty2.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenPenalty2.ForeColor = GameControl.DefaultBackColor;
                this.green.penalty--;
                this.GameLog("Green Penalty: Subtract");
            }
        }

        private void lblGreenPenalty3_Click(object sender, EventArgs e)
        {
            if (lblGreenPenalty3.BackColor == GameControl.DefaultBackColor)
            {
                lblGreenPenalty3.BackColor = Color.Lime;
                lblGreenPenalty3.ForeColor = Color.Black;
                GD.lblGreenPenalty3.BackColor = Color.Lime;
                GD.lblGreenPenalty3.ForeColor = Color.Black;
                this.green.penalty++;
                this.GameLog("Green Penalty: ADD");
            }
            else
            {
                lblGreenPenalty3.BackColor = GameControl.DefaultBackColor;
                lblGreenPenalty3.ForeColor = Color.Lime;
                GD.lblGreenPenalty3.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenPenalty3.ForeColor = GameControl.DefaultBackColor;
                this.green.penalty--;
                this.GameLog("Green Penalty: Subtract");
            }
        }

        private void lblGreenDQ_Click(object sender, EventArgs e)
        {
            if (lblGreenDQ.BackColor == GameControl.DefaultBackColor)
            {
                lblGreenDQ.BackColor = Color.Lime;
                lblGreenDQ.ForeColor = Color.Black;
                GD.lblGreenDQ.BackColor = Color.Lime;
                GD.lblGreenDQ.ForeColor = Color.Black;
                this.green.dq = true;
                this.GameLog("Green DQ: True");
            }
            else
            {
                lblGreenDQ.BackColor = GameControl.DefaultBackColor;
                lblGreenDQ.ForeColor = Color.Lime;
                GD.lblGreenDQ.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenDQ.ForeColor = GameControl.DefaultBackColor;
                this.green.dq = false;
                this.GameLog("Green DQ: False");
            }
        }

        private void btnDisableGreen_Click(object sender, EventArgs e)
        {
            if (btnDisableGreen.BackColor == GameControl.DefaultBackColor)
            {

                btnDisableGreen.BackColor = Color.Lime;
                btnDisableGreen.ForeColor = Color.Black;
                GD.lblGreenScore.BackColor = Color.Lime;
                GD.lblGreenScore.ForeColor = Color.Black;
                this.fc.RobotTransmitters("green", State.off, State.off);
                this.GameLog("Green Disabled = True");
            }
            else
            {
                btnDisableGreen.BackColor = GameControl.DefaultBackColor;
                btnDisableGreen.ForeColor = Color.Lime;
                GD.lblGreenScore.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenScore.ForeColor = Color.Lime;
                this.fc.RobotTransmitters("green", State.on, State.on);
                this.GameLog("Green Disabled = False");
            }
        }
        #endregion

        #region Red Penalties
        private void lblRedPenalty1_Click(object sender, EventArgs e)
        {
            if (lblRedPenalty1.BackColor == GameControl.DefaultBackColor)
            {
                lblRedPenalty1.BackColor = Color.Red;
                lblRedPenalty1.ForeColor = Color.Black;
                GD.lblRedPenalty1.BackColor = Color.Red;
                GD.lblRedPenalty1.ForeColor = Color.Black;
                this.red.penalty++;
                this.GameLog("Red Penalty: ADD");
            }
            else
            {
                lblRedPenalty1.BackColor = GameControl.DefaultBackColor;
                lblRedPenalty1.ForeColor = Color.Red;
                GD.lblRedPenalty1.BackColor = GameControl.DefaultBackColor;
                GD.lblRedPenalty1.ForeColor = GameControl.DefaultBackColor;
                this.red.penalty--;
                this.GameLog("Red Penalty: Subtract");
            }
        }

        private void lblRedPenalty2_Click(object sender, EventArgs e)
        {
            if (lblRedPenalty2.BackColor == GameControl.DefaultBackColor)
            {
                lblRedPenalty2.BackColor = Color.Red;
                lblRedPenalty2.ForeColor = Color.Black;
                GD.lblRedPenalty2.BackColor = Color.Red;
                GD.lblRedPenalty2.ForeColor = Color.Black;
                this.red.penalty++;
                this.GameLog("Red Penalty: ADD");
            }
            else
            {
                lblRedPenalty2.BackColor = GameControl.DefaultBackColor;
                lblRedPenalty2.ForeColor = Color.Red;
                GD.lblRedPenalty2.BackColor = GameControl.DefaultBackColor;
                GD.lblRedPenalty2.ForeColor = GameControl.DefaultBackColor;
                this.red.penalty--;
                this.GameLog("Red Penalty: Subtract");
            }
        }

        private void lblRedPenalty3_Click(object sender, EventArgs e)
        {
            if (lblRedPenalty3.BackColor == GameControl.DefaultBackColor)
            {
                lblRedPenalty3.BackColor = Color.Red;
                lblRedPenalty3.ForeColor = Color.Black;
                GD.lblRedPenalty3.BackColor = Color.Red;
                GD.lblRedPenalty3.ForeColor = Color.Black;
                this.red.penalty++;
                this.GameLog("Red Penalty: ADD");
            }
            else
            {
                lblRedPenalty3.BackColor = GameControl.DefaultBackColor;
                lblRedPenalty3.ForeColor = Color.Red;
                GD.lblRedPenalty3.BackColor = GameControl.DefaultBackColor;
                GD.lblRedPenalty3.ForeColor = GameControl.DefaultBackColor;
                this.red.penalty--;
                this.GameLog("Red Penalty: Subtract");
            }
        }

        private void lblRedDQ_Click(object sender, EventArgs e)
        {
            if (lblRedDQ.BackColor == GameControl.DefaultBackColor)
            {
                lblRedDQ.BackColor = Color.Red;
                lblRedDQ.ForeColor = Color.Black;
                GD.lblRedDQ.BackColor = Color.Red;
                GD.lblRedDQ.ForeColor = Color.Black;
                this.red.dq = true;
                this.GameLog("Red DQ: True");
            }
            else
            {
                lblRedDQ.BackColor = GameControl.DefaultBackColor;
                lblRedDQ.ForeColor = Color.Red;
                GD.lblRedDQ.BackColor = GameControl.DefaultBackColor;
                GD.lblRedDQ.ForeColor = GameControl.DefaultBackColor;
                this.red.dq = false;
                this.GameLog("Red DQ: False");
            }
        }

        private void btnDisableRed_Click(object sender, EventArgs e)
        {
            if (btnDisableRed.BackColor == GameControl.DefaultBackColor)
            {

                btnDisableRed.BackColor = Color.Red;
                btnDisableRed.ForeColor = Color.Black;
                GD.lblRedScore.BackColor = Color.Red;
                GD.lblRedScore.ForeColor = Color.Black;
                this.fc.RobotTransmitters("red", State.off, State.off);
                this.GameLog("Red Disabled = True");
            }
            else
            {
                btnDisableRed.BackColor = GameControl.DefaultBackColor;
                btnDisableRed.ForeColor = Color.Red;
                GD.lblRedScore.BackColor = GameControl.DefaultBackColor;
                GD.lblRedScore.ForeColor = Color.Red;
                this.fc.RobotTransmitters("red", State.on, State.on);
                this.GameLog("Red Disabled = False");
            }
        }

        #endregion

        #endregion

        #region Game Controls

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            this.autoModeTime = 30;
            this.manAutoTime = 20;
            this.midModeTime = 120;

            disableGameButtons();
            btnStop.BackColor = GameControl.DefaultBackColor;
            btnStartGame.BackColor = Color.LimeGreen;
            ClearDisplay();
            this.GameStartUp();
            this.gameTimer.Start();
            this.time.countDownStart(2, 1);
            this.time.timesUp = false;
            this.MainGame();
        }

        private void btnPracticeMode_Click(object sender, EventArgs e)
        {
            disableGameButtons();
            btnStop.BackColor = GameControl.DefaultBackColor;
            btnPracticeMode.BackColor = Color.LimeGreen;
            ClearDisplay();
            this.GameStartUp(GameModes.debug);
            Thread.Sleep(200);
            this.time.elapsedTime.Restart();
            this.time.timesUp = false;
            this.practiceTimer.Start();
            this.MainGame();
        }

        private void btnAutoMode_Click(object sender, EventArgs e)
        {
            disableGameButtons();
            btnStop.BackColor = GameControl.DefaultBackColor;
            btnAutoMode.BackColor = Color.LimeGreen;
            ClearDisplay();
            this.GameStartUp(GameModes.autonomous);
            Thread.Sleep(200);
            this.GameLog("Auto Mode Started");
            this.time.elapsedTime.Restart();
            this.time.timesUp = false;
            this.practiceTimer.Start();
            this.MainGame();
        }

        private void btnManualMode_Click(object sender, EventArgs e)
        {
            disableGameButtons();
            btnStop.BackColor = GameControl.DefaultBackColor;
            btnManualMode.BackColor = Color.LimeGreen;
            ClearDisplay();
            this.GameStartUp(GameModes.manual);
            Thread.Sleep(200);
            this.GameLog("Middle Mode Started");
            this.time.elapsedTime.Restart();
            this.time.timesUp = false;
            this.practiceTimer.Start();
            this.MainGame();
        }

        private void btnTestMode_Click(object sender, EventArgs e)
        {
            if (btnTestMode.BackColor == DefaultBackColor)
            {
                disableGameButtons();
                btnStop.BackColor = GameControl.DefaultBackColor;
                btnTestMode.BackColor = Color.LimeGreen;
                this.testTimer.Start();
                TestMode();
            }
            else
            {
                btnStop.PerformClick();
            }
        }

        private void TestMode()
        {
            this.autoModeTime = 5;
            this.manAutoTime =  2;
            this.midModeTime = 30;

            this.btnMatchNext.PerformClick();

            ClearDisplay();
            this.GameStartUp();
            this.gameTimer.Start();
            this.time.countDownStart(0, 30);
            this.time.timesUp = false;
            this.MainGame();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //Turn on all Hub Channel
            this.fc.FieldAllOff();
            this.gameTimer.Stop();
            this.practiceTimer.Stop();
            this.testTimer.Stop();
            btnStop.BackColor = Color.Red;
            btnStartGame.BackColor = GameControl.DefaultBackColor;
            btnAutoMode.BackColor = GameControl.DefaultBackColor;
            btnManualMode.BackColor = GameControl.DefaultBackColor;
            btnPracticeMode.BackColor = GameControl.DefaultBackColor;
            btnTestMode.BackColor = GameControl.DefaultBackColor;
            this.gameMode = GameModes.off;
            enableGameButtons();
            this.GameLog("Field Off");
            this.LogGame();
        }

        private void btnMatchNext_Click(object sender, EventArgs e)
        {
            if (this.gameMode == GameModes.off)
            {
                matchNumber++;
                lblMatchNumber.Text = "Match " + matchNumber.ToString();
                GD.lblMatchNumber.Text = "Match " + matchNumber.ToString();
                GetTeamNames();
                ClearDisplay();
            }
        }

        private void btnMatchPrev_Click(object sender, EventArgs e)
        {
            if (this.gameMode == GameModes.off)
            {
                if (matchNumber > 0) matchNumber--;
                lblMatchNumber.Text = "Match " + matchNumber.ToString();
                GD.lblMatchNumber.Text = "Match " + matchNumber.ToString();
                GetTeamNames();
                ClearDisplay();
            }
        }

        private void lblMatchNumber_Click(object sender, EventArgs e)
        {
            GetTeamNames();
        }

        private void btnGameDisplay_Click(object sender, EventArgs e)
        {
            if (Screen.AllScreens.Length > 1)//Check for Multiple Displays
            {

                // Important !
                GD.StartPosition = FormStartPosition.Manual;

                // Get the second monitor screen
                Screen screen = GetSecondaryScreen();

                // set the location to the top left of the second screen
                GD.Location = screen.WorkingArea.Location;

                // set it fullscreen
                GD.Size = new Size(screen.WorkingArea.Width, screen.WorkingArea.Height);

                // Show the form
                GD.Show();//Shows Display on Second Display

            }
            else GD.Show(); //Shows Display on the only display
            btnGameDisplay.Visible = false; //Disables the button
        }

        private void btnScoreGame_Click(object sender, EventArgs e)
        {
            Score score = new Score(this);
            score.Show();

            while (!score.finalScore)
            {
                Application.DoEvents();
            }

            ScoreGame();
            RecordGame();

            score.Close();

            btnStop.BackColor = Color.Red;
            btnStartGame.BackColor = GameControl.DefaultBackColor;
            btnPracticeMode.BackColor = GameControl.DefaultBackColor;
            gameMode = fc.ChangeGameMode(GameModes.off);

            lblGreenScore.Text = green.finalScore.ToString();
            lblRedScore.Text = red.finalScore.ToString();
            GD.lblGreenScore.Text = green.finalScore.ToString();
            GD.lblRedScore.Text = red.finalScore.ToString();

            btnStop.PerformClick();
        }

        private void lblGreenScore_Click(object sender, EventArgs e)
        {
            ClearDisplay();
        }

        private void lblRedScore_Click(object sender, EventArgs e)
        {
            ClearDisplay();
        }

        private void lblGameClock_Click(object sender, EventArgs e)
        {
            ClearDisplay();
        }

        private void UpdateGame()
        { 
            this.MainGame();
            this.updateDisplays();
            if (!red.autoMan && (this.fc.node[3].gameMode == GameModes.mantonomous.ToString()))
            {
                red.autoMan = true;
                this.btnRedMantonomous.BackColor = Color.Red;
                this.btnRedMantonomous.ForeColor = Color.Black;
            }
            if (!green.autoMan && (this.fc.node[8].gameMode == GameModes.mantonomous.ToString()))
            {
                green.autoMan = true;
                this.btnGreenMantonomous.BackColor = Color.Lime;
                this.btnGreenMantonomous.ForeColor = Color.Black;
            }
        }

        private void ScoreGame()
        {
            //Add convert additional points to intergers here

            if (this.red.dq || this.red.penalty == 3)
            {
                this.red.finalScore = 0;
                this.red.matchResult = "L";
            }
            if (this.green.dq || this.green.penalty == 3)
            {
                this.green.finalScore = 0;
                this.green.matchResult = "L";
            }

            if (this.green.finalScore > this.red.finalScore)
            {
                this.green.matchResult = "W";
                this.red.matchResult = "L";
            }
            else if (this.red.finalScore > this.green.finalScore)
            {
                this.red.matchResult = "W";
                this.green.matchResult = "L";
            }
            else if (this.green.finalScore == this.red.finalScore && !this.green.dq && !this.red.dq)
            {
                this.red.matchResult = "T";
                this.green.matchResult = "T";
            }
        }

        private void RecordGame()
        {
            string file = "\\Match " + matchNumber.ToString() + " - Score";
            string file2 = "\\Match Scores";
            string folder = "Matches\\" + "Match " + matchNumber.ToString();
            string folder2 = "Matches\\";

            string greenTeam = (matchNumber.ToString() + "\t" + lblGreenTeam.Text.ToString() + "\t" + this.green.finalScore.ToString()
                               + "\t" + this.green.penalty.ToString() + "\t" + this.green.dq.ToString() + "\t" + this.green.matchResult);
            string greenTeam2 = ("THIS YEAR'S STUFF TO RECORD");
            string redTeam = (matchNumber.ToString() + "\t" + lblRedTeam.Text.ToString() + "\t" + this.red.finalScore.ToString()
                             + "\t" + this.red.penalty.ToString() + "\t" + this.red.dq.ToString() + "\t" + this.red.matchResult);
            string redTeam2 = ("THIS YEAR'S STUFF TO RECORD");
            string field = ("Match Number" + "\t" + "Team Name" + "\t" + "Final Score" + "\t" + "Penalties" + "\t" + "DQ" + "\t" + "Result");
            string field2 = ("THIS YEAR'S STUFF TO RECORD");

            string text = ("\r\n" + field + "\t" + field2 + "\r\n" + greenTeam + "\t" + greenTeam2 + "\r\n" + redTeam + "\t" + redTeam2);

            try
            {
                this.lw.WriteLog(text, file, folder);
                this.lw.WriteLog(text, file2, folder2);
            }
            catch
            {
                MessageBox.Show("Game Score was not recorded");
                return;
            }

        }

        private void GetTeamNames()
        {
            string greenTeam = null;
            string redTeam = null;
            string content = null;
            string path = filePath;
            string file = this.fs.setupFilePath + @"\Teams.txt";

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch
            {
                path = null;
            }

            try
            {
                if (File.Exists(file))
                {
                    //StreamReader sr = new StreamReader(file, System.Text.Encoding.Default);
                    Stream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader sr = new StreamReader(stream);

                    for (int i = 0; i < matchNumber; i++)
                    {
                        content = sr.ReadLine();
                        string[] teams = content.Split(new string[] { "\t" }, StringSplitOptions.None);//Delimited the Tab keycode
                        greenTeam = teams[0];
                        redTeam = teams[1];
                    }
                    sr.Close();
                    sr.Dispose();
                }
                else
                {
                    greenTeam = "Green Team";
                    redTeam = "Red Team";
                }
            }
            catch
            {
                greenTeam = "Green Team";
                redTeam = "Red Team";
            }
            if (greenTeam == null) greenTeam = "Green Team";
            if (redTeam == null) redTeam = "Red Team";


            lblGreenTeam.Text = greenTeam;
            lblRedTeam.Text = redTeam;
            GD.lblGreenTeam.Text = greenTeam;
            GD.lblRedTeam.Text = redTeam;
        }

        private void disableGameButtons()
        {
            btnStartGame.Enabled = false;
            btnAutoMode.Enabled = false;
            btnManualMode.Enabled = false;
            btnPracticeMode.Enabled = false;
            btnTestMode.Enabled = false;
        }

        private void enableGameButtons()
        {
            btnStartGame.Enabled = true;
            btnAutoMode.Enabled = true;
            btnManualMode.Enabled = true;
            btnPracticeMode.Enabled = true;
            btnTestMode.Enabled = true;
        }

        #endregion

        #region Display

        private void updateDisplays()
        {
            lblGreenScore.Text = this.green.score.ToString();
            lblRedScore.Text = this.red.score.ToString();
            GD.lblGreenScore.Text = this.green.score.ToString();
            GD.lblRedScore.Text = this.red.score.ToString();
        }



        public Screen GetSecondaryScreen()
        {
            if (Screen.AllScreens.Length == 1)
            {
                return null;
            }

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Primary == false)
                {
                    return screen;
                }
            }

            return null;
        }

        private void ClearDisplay()
        {
            if (this.gameMode == GameModes.off)
            {
                lblGameClock.Text = "2:00";
                GD.lblGameClock.Text = "2:00";

                lblRedScore.Text = "000";
                lblGreenScore.Text = "000";
                GD.lblGreenScore.Text = "000";
                GD.lblRedScore.Text = "000";

                lblGreenPenalty1.BackColor = GameControl.DefaultBackColor;
                lblGreenPenalty1.ForeColor = Color.Lime;
                GD.lblGreenPenalty1.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenPenalty1.ForeColor = GameControl.DefaultBackColor;

                lblGreenPenalty2.BackColor = GameControl.DefaultBackColor;
                lblGreenPenalty2.ForeColor = Color.Lime;
                GD.lblGreenPenalty2.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenPenalty2.ForeColor = GameControl.DefaultBackColor;

                lblGreenPenalty3.BackColor = GameControl.DefaultBackColor;
                lblGreenPenalty3.ForeColor = Color.Lime;
                GD.lblGreenPenalty3.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenPenalty3.ForeColor = GameControl.DefaultBackColor;

                lblGreenDQ.BackColor = GameControl.DefaultBackColor;
                lblGreenDQ.ForeColor = Color.Lime;
                GD.lblGreenDQ.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenDQ.ForeColor = GameControl.DefaultBackColor;

                lblRedPenalty1.BackColor = GameControl.DefaultBackColor;
                lblRedPenalty1.ForeColor = Color.Red;
                GD.lblRedPenalty1.BackColor = GameControl.DefaultBackColor;
                GD.lblRedPenalty1.ForeColor = GameControl.DefaultBackColor;

                lblRedPenalty2.BackColor = GameControl.DefaultBackColor;
                lblRedPenalty2.ForeColor = Color.Red;
                GD.lblRedPenalty2.BackColor = GameControl.DefaultBackColor;
                GD.lblRedPenalty2.ForeColor = GameControl.DefaultBackColor;

                lblRedPenalty3.BackColor = GameControl.DefaultBackColor;
                lblRedPenalty3.ForeColor = Color.Red;
                GD.lblRedPenalty3.BackColor = GameControl.DefaultBackColor;
                GD.lblRedPenalty3.ForeColor = GameControl.DefaultBackColor;

                lblRedDQ.BackColor = GameControl.DefaultBackColor;
                lblRedDQ.ForeColor = Color.Red;
                GD.lblRedDQ.BackColor = GameControl.DefaultBackColor;
                GD.lblRedDQ.ForeColor = GameControl.DefaultBackColor;

                //Current Year's Game

                btnRedMantonomous.BackColor = GameControl.DefaultBackColor;
                btnRedMantonomous.ForeColor = Color.Red;
                btnGreenMantonomous.BackColor = GameControl.DefaultBackColor;
                btnGreenMantonomous.ForeColor = Color.Lime;

            }
        }

        #endregion

        #region Timers
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            timeKeeper();
            updateTime();
            UpdateGame();
        }

        private void practiceTimer_Tick(object sender, EventArgs e)
        {
            UpdateGame();
        }

        private void updateTime()
        {
            lblGameClock.Text = this.time.countDownStatus();
            GD.lblGameClock.Text = lblGameClock.Text.ToString();
        }

        private void timeKeeper()
        {
            if (this.time.timesUp)
            {
                if (!this.fc.switchMode)
                {
                    updateDisplays();
                    this.GameShutDown();
                    this.gameMode = this.fc.ChangeGameMode(GameModes.end);
                    lblGameClock.Text = ("0:00");
                    this.GameLog("Game Stopped");
                    this.gameTimer.Stop();
                    this.LogGame();
                }
            }

            else if (this.gameMode == GameModes.autonomous && !this.time.Timer(this.autoModeTime))
            {

                if (this.time.Timer(this.manAutoTime))
                {
                    lblGameClock.ForeColor = Color.Blue;
                    GD.lblGameClock.ForeColor = Color.Blue;
                }
                else
                {
                    lblGameClock.ForeColor = Color.Red;
                    GD.lblGameClock.ForeColor = Color.Red;
                }
            }

            else if (this.gameMode == GameModes.autonomous && this.time.Timer(this.autoModeTime))
            {

                this.gameMode = this.fc.ChangeGameMode(GameModes.manual);
            }

            else if (this.gameMode == GameModes.manual && !this.time.Timer(this.midModeTime))
            {
                lblGameClock.ForeColor = Color.Black;
                GD.lblGameClock.ForeColor = Color.Black;
            }
            else
            {
                lblGameClock.ForeColor = Color.Black;
                GD.lblGameClock.ForeColor = Color.Black;
                this.gameMode = this.fc.ChangeGameMode(GameModes.end);
                this.updateDisplays();
                this.GameShutDown();
                this.GameLog("Game Stopped");
            }
        }

        private void testTimer_Tick(object sender, EventArgs e)
        {
            if(this.gameMode == GameModes.end)
            {
                this.red.finalScore = this.red.score;
                this.green.finalScore = this.green.score;
                this.ScoreGame();
                this.RecordGame();
                this.fc.FieldAllOff();
                this.gameMode = GameModes.off;

                if(this.red.score != this.green.score)
                {
                    string file = "\\Match - BAD Scores";
                    string folder = "Matches\\";
                    string text = string.Format("Match# {0} - Red = {1} | Green = {2}", matchNumber, this.red.finalScore, this.green.finalScore);
                    this.lw.WriteLog(text, file, folder);
                }

                this.TestMode();
            }
        }
        #endregion

        #endregion

        //------------------------------------------------------------------------------------------------\\
        //Current year's game methods
        //------------------------------------------------------------------------------------------------\\

        private void btnGreenPlus_Click(object sender, EventArgs e)
        {
        }

        private void btnGreenMinus_Click(object sender, EventArgs e)
        {
        }

        private void btnRedPlus_Click(object sender, EventArgs e)
        {
        }

        private void btnRedMinus_Click(object sender, EventArgs e)
        {
        }

        private void btnGreenMantonomous_Click(object sender, EventArgs e)
        {
            if (btnGreenMantonomous.BackColor == DefaultBackColor && this.gameMode == GameModes.autonomous)
            {
                btnGreenMantonomous.BackColor = Color.Lime;
                btnGreenMantonomous.ForeColor = Color.Black;
    
                this.green.autoMan = true;
                this.GameLog("Green ManTonomous");

                for (int i = 6; i <= 10; i++)
                {
                    this.fc.ChangeGameMode(i, GameModes.mantonomous);
                }
                this.fc.RobotTransmitters("green", State.off, State.on);
            }
        }

        private void btnRedMantonomous_Click(object sender, EventArgs e)
        {
            if (btnRedMantonomous.BackColor == DefaultBackColor && this.gameMode == GameModes.autonomous)
            {
                btnRedMantonomous.BackColor = Color.Red;
                btnRedMantonomous.ForeColor = Color.Black;

                this.red.autoMan = true;
                this.GameLog("Red ManTonomous");

                for (int i = 1; i <= 5; i++)
                {
                    this.fc.ChangeGameMode(i, GameModes.mantonomous);
                }
                this.fc.RobotTransmitters("red", State.off, State.on);
            }
        }

        private void GameControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.btnStop.PerformClick();
            this.fc.ChangeGameMode(GameModes.off);
            this.gameMode = GameModes.off;
        }

    }

    //Vertical Progress Bar 
    public class VerticalProgressBar : ProgressBar
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x04;
                return cp;
            }
        }
        protected override System.Drawing.Size DefaultSize { get { return new System.Drawing.Size(23, 100); } }  
    }
}
