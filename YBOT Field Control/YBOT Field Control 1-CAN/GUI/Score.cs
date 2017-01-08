using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YBOT_Field_Control_2016
{
    public partial class Score : Form
    {
        const int autoCornersTestedPointValue = 15;
        const int autoEmergencyCycledPointValue = 35;
        const int manualEmergencyCycledPointValue = 100;
        const int emergencyCycledPenaltyPointValue = 250;
        const int minimumEmergencyCycled = 4;
        const int maxRockWeight = 128;
        const int rocketLaunchedPointValue = 100;
        const int teamPenalty = 200;

        enum RocketPosition : int {
            Loaded = 1,
            DoorClosed = 2,
            CrawlerMoved = 3,
            LaunchPosition = 5
        }

        public bool finalScore = false;
        public bool manualOverride = false;

        GameControl game;

        public Score () : this (new GameControl ()) { }

        public Score (GameControl game) {
            InitializeComponent();
            this.game = game;

            cbRocketPosition.TextChanged += OnValidation;
            tbRockWeight.Validated += OnValidation;
        }

        private void Score_Shown (object sender, EventArgs e) {
            InitScore ();
        }

        private void Score_FormClosed (object sender, FormClosedEventArgs e) {
            UpdateScore (true);
            finalScore = true;
        }

        private void InitScore () {
            var green = game.green;
            var red = game.red;

            tbAutoCornersTested.Text = green.autoTowerTested.ToString ();
            tbAutoEmergencyCycled.Text = green.autoEmergencyTowerCycled.ToString ();
            tbAutoSolarScore.Text = green.autoSolarPanelScore.ToString ();
            //tbManualSolar1Score.Text = green.manSolarPanelScore1.ToString ();
            //tbManualSolar2Score.Text = green.manSolarPanelScore2.ToString ();
            tbManualSolar1Score.Text = "0";
            tbManualSolar2Score.Text = "0";
            tbManualEmergencyCycled.Text = green.emergencyCleared.ToString ();
            if (green.emergencyCleared < minimumEmergencyCycled) {
                cbEmergencyCycledPenalty.Text = emergencyCycledPenaltyPointValue.ToString ();
            } else {
                cbEmergencyCycledPenalty.Text = "0";
            }

            tbGreenPenalty.Text = green.penalty.ToString ();
            tbRedPenalty.Text = red.penalty.ToString ();

            if (green.dq || (green.penalty >= 3)) {
                btnGreenDq.BackColor = Color.Lime;
            }

            if (red.dq || (red.penalty >= 3)) {
                btnRedDq.BackColor = Color.Red;
            }

            UpdateScore ();
        }

        protected void UpdateScore () {
            UpdateScore (false);
        }

        protected void UpdateScore (bool updateTeams) {
            var autoCornersTested = Convert.ToInt32 (tbAutoCornersTested.Text) * autoCornersTestedPointValue;
            var autoEmergencyCycled = Convert.ToInt32 (tbAutoEmergencyCycled.Text) * autoEmergencyCycledPointValue;
            var autoSolar = Convert.ToInt32 (tbAutoSolarScore.Text);
            var manualSolar1 = Convert.ToInt32 (tbManualSolar1Score.Text);
            var manualSolar2 = Convert.ToInt32 (tbManualSolar2Score.Text);
            var manualEmergencyCycled = Convert.ToInt32 (tbManualEmergencyCycled.Text) * manualEmergencyCycledPointValue;
            var emergencyCycledPenalty = Convert.ToInt32 (cbEmergencyCycledPenalty.Text);
            var rocketPositionMultiplier = Convert.ToInt32 (lbRocketPositionMulitplier.Text);
            var rockWeight = Convert.ToInt32 (tbRockWeight.Text);
            var rockScore = rockWeight * rocketPositionMultiplier;
            var rocketLaunched = Convert.ToInt32 (lbRocketLaunchedScore.Text);
            var autoScore = autoCornersTested + autoEmergencyCycled + autoSolar;
            var manualScore = manualSolar1 + manualSolar2 + manualEmergencyCycled - emergencyCycledPenalty + rockScore + rocketLaunched;
            var jointScore = autoScore + manualScore;

            var greenPenalty = Convert.ToInt32 (tbGreenPenalty.Text);
            var greenScore = jointScore - (greenPenalty * teamPenalty);

            var redPenalty = Convert.ToInt32 (tbRedPenalty.Text);
            var redScore = jointScore - (redPenalty * teamPenalty);

            lbAutoCornerTestedScore.Text = autoCornersTested.ToString ();
            lbAutoEmergencyCycledScore.Text = autoEmergencyCycled.ToString ();
            lbManualEmergencyCycledScore.Text = manualEmergencyCycled.ToString ();
            lbRockScore.Text = rockScore.ToString ();
            lbJointScore.Text = jointScore.ToString ();

            lbGreenPenaltyScore.Text = (greenPenalty * teamPenalty).ToString ();
            lbRedPenaltyScore.Text = (redPenalty * teamPenalty).ToString ();
            lbGreenScore.Text = greenScore.ToString ();
            lbRedScore.Text = redScore.ToString ();

            if (updateTeams) {
                var green = game.green;
                var red = game.red;

                green.autoTowerTested = autoCornersTested;
                green.autoEmergencyTowerCycled = autoEmergencyCycled;
                green.autoSolarPanelScore = autoSolar;

                //green.manSolarPanelScore1 = manualSolar1;
                //green.manSolarPanelScore2 = manualSolar2;
                green.emergencyCleared = manualEmergencyCycled;

                green.rockValue = rocketPositionMultiplier;
                green.rockWeight = rockWeight;
                green.rockScore = rockScore;
                green.rocketPosition = rocketPositionMultiplier;

                green.autoScore = autoScore;
                green.manScore = manualScore;
                green.score = jointScore;

                green.penalty = greenPenalty;
                if (btnGreenDq.BackColor == DefaultBackColor) {
                    green.dq = false;
                } else {
                    green.dq = true;
                }
                green.finalScore = greenScore;

                red.autoTowerTested = autoCornersTested;
                red.autoEmergencyTowerCycled = autoEmergencyCycled;
                red.autoSolarPanelScore = autoSolar;

                //red.manSolarPanelScore1 = manualSolar1;
                //red.manSolarPanelScore2 = manualSolar2;
                red.emergencyCleared = manualEmergencyCycled;

                red.rockValue = rocketPositionMultiplier;
                red.rockWeight = rockWeight;
                red.rockScore = rockScore;
                red.rocketPosition = rocketPositionMultiplier;

                red.autoScore = autoScore;
                red.manScore = manualScore;
                red.score = jointScore;

                red.penalty = redPenalty;
                if (btnRedDq.BackColor == DefaultBackColor) {
                    red.dq = false;
                } else {
                    red.dq = true;
                }
                red.finalScore = redScore;
            }
        }

        private void btnGreenDq_Click (object sender, EventArgs e) {
            if (btnGreenDq.BackColor == DefaultBackColor) {
                btnGreenDq.BackColor = Color.Lime;
            } else {
                btnGreenDq.BackColor = DefaultBackColor;
            }
        }

        private void btnRedDq_Click (object sender, EventArgs e) {
            if (btnRedDq.BackColor == DefaultBackColor) {
                btnRedDq.BackColor = Color.Red;
            } else {
                btnRedDq.BackColor = DefaultBackColor;
            }
        }

        private void btnFinalScore_Click (object sender, EventArgs e) {
            UpdateScore (true);
            finalScore = true;
        }

        private void btnUpdateScore_Click (object sender, EventArgs e) {
            UpdateScore ();
        }

        private void btnOverride_Click (object sender, EventArgs e) {
            if (!manualOverride) {
                tbAutoCornersTested.Enabled = true;
                tbAutoEmergencyCycled.Enabled = true;
                tbAutoSolarScore.Enabled = true;

                tbManualSolar1Score.Enabled = true;
                tbManualSolar2Score.Enabled = true;
                tbManualEmergencyCycled.Enabled = true;
                cbEmergencyCycledPenalty.Enabled = true;

                tbGreenPenalty.Enabled = true;
                tbRedPenalty.Enabled = true;

                btnGreenDq.Enabled = true;
                btnRedDq.Enabled = true;

                btnOverride.BackColor = Color.SteelBlue;
                manualOverride = true;

                tbAutoCornersTested.Validated += OnValidation;
                tbAutoEmergencyCycled.Validated += OnValidation;
                tbAutoSolarScore.Validated += OnValidation;
                tbManualSolar1Score.Validated += OnValidation;
                tbManualSolar2Score.Validated += OnValidation;
                tbManualEmergencyCycled.Validated += ManualEmergencyCycledAutoPenalty;
                tbManualEmergencyCycled.Validated += OnValidation;
                cbEmergencyCycledPenalty.Validated += OnValidation;
                tbGreenPenalty.Validated += PenaltyAutoDq;
                tbGreenPenalty.Validated += OnValidation;
                tbRedPenalty.Validated += PenaltyAutoDq;
                tbRedPenalty.Validated += OnValidation;
            } else {
                tbAutoCornersTested.Enabled = false;
                tbAutoEmergencyCycled.Enabled = false;
                tbAutoSolarScore.Enabled = false;

                tbManualSolar1Score.Enabled = false;
                tbManualSolar2Score.Enabled = false;
                tbManualEmergencyCycled.Enabled = false;
                cbEmergencyCycledPenalty.Enabled = false;

                tbGreenPenalty.Enabled = false;
                tbRedPenalty.Enabled = false;

                tbGreenPenalty.Enabled = false;
                tbRedPenalty.Enabled = false;

                btnGreenDq.Enabled = false;
                btnRedDq.Enabled = false;

                btnOverride.BackColor = DefaultBackColor;
                manualOverride = false;
                InitScore ();

                tbAutoCornersTested.Validated -= OnValidation;
                tbAutoEmergencyCycled.Validated -= OnValidation;
                tbAutoSolarScore.Validated -= OnValidation;
                tbManualSolar1Score.Validated -= OnValidation;
                tbManualSolar2Score.Validated -= OnValidation;
                tbManualEmergencyCycled.Validated -= ManualEmergencyCycledAutoPenalty;
                tbManualEmergencyCycled.Validated -= OnValidation;
                cbEmergencyCycledPenalty.Validated -= OnValidation;
                tbGreenPenalty.Validated -= PenaltyAutoDq;
                tbGreenPenalty.Validated -= OnValidation;
                tbRedPenalty.Validated -= PenaltyAutoDq;
                tbRedPenalty.Validated -= OnValidation;
            }
        }

        private void cbRocketLaunched_CheckedChanged (object sender, EventArgs e) {
            var cb = sender as CheckBox;
            if (cb != null) {
                if (cb.Checked) {
                    lbRocketLaunchedScore.Text = rocketLaunchedPointValue.ToString ();
                } else {
                    lbRocketLaunchedScore.Text = "0";
                }

                UpdateScore ();
            }
        }

        private void TowerIntegerValidation (object sender, CancelEventArgs e) {
            var tb = sender as TextBox;
            if (tb != null) {
                try {
                    var number = Convert.ToInt32 (tb.Text);
                    if ((number < 0) || (number > 10)) {
                        MessageBox.Show ("Invalid number of towers\n" +
                            "Must be between 0 and 10");
                        e.Cancel = true;
                    }
                } catch (FormatException) {
                    MessageBox.Show ("Invalid integer format");
                    e.Cancel = true;
                } catch (OverflowException) {
                    MessageBox.Show ("Number too large");
                    e.Cancel = true;
                }
            } else {
                e.Cancel = true;
            }
        }

        private void IntegerValidation (object sender, CancelEventArgs e) {
            var tb = sender as TextBox;
            if (tb != null) {
                try {
                    var number = Convert.ToInt32 (tb.Text);
                } catch (FormatException) {
                    MessageBox.Show ("Invalid integer format");
                    e.Cancel = true;
                } catch (OverflowException) {
                    MessageBox.Show ("Number too large");
                    e.Cancel = true;
                }
            } else {
                e.Cancel = true;
            }
        }

        private void RockWeightValidation (object sender, CancelEventArgs e) {
            var tb = sender as TextBox;
            if (tb != null) {
                try {
                    var number = Convert.ToInt32 (tb.Text);
                    if ((number < 0) || (number > maxRockWeight)) {
                        MessageBox.Show ("Invalid rock weight\n" +
                            "Must be between 0 and " + maxRockWeight.ToString ());
                        e.Cancel = true;
                    }
                } catch (FormatException) {
                    MessageBox.Show ("Invalid integer format");
                    e.Cancel = true;
                } catch (OverflowException) {
                    MessageBox.Show ("Number too large");
                    e.Cancel = true;
                }
            } else {
                e.Cancel = true;
            }
        }

        private void PenaltyValidation (object sender, CancelEventArgs e) {
            var tb = sender as TextBox;
            if (tb != null) {
                try {
                    var number = Convert.ToInt32 (tb.Text);
                    if ((number < 0) || (number > 3)) {
                        MessageBox.Show ("Invalid number of penalties\n" +
                            "Must be between 0 and 3");
                        e.Cancel = true;
                    }
                } catch (FormatException) {
                    MessageBox.Show ("Invalid integer format");
                    e.Cancel = true;
                } catch (OverflowException) {
                    MessageBox.Show ("Number too large");
                    e.Cancel = true;
                }
            } else {
                e.Cancel = true;
            }
        }

        private void PenaltyAutoDq (object sender, EventArgs e) {
            var tb = sender as TextBox;
            if (tb != null) {
                try {
                    var penalties = Convert.ToInt32 (tb.Text);
                    if (penalties >= 3) {
                        if (tb.Name.Contains ("Green")) {
                            btnGreenDq.BackColor = Color.Lime;
                        } else if (tb.Name.Contains ("Red")) {
                            btnGreenDq.BackColor = Color.Red;
                        }
                    } else {
                        if (tb.Name.Contains ("Green")) {
                            btnGreenDq.BackColor = DefaultBackColor;
                        } else if (tb.Name.Contains ("Red")) {
                            btnGreenDq.BackColor = DefaultBackColor;
                        }
                    }
                } catch {
                    //
                }
            } else {
            }
        }

        private void ManualEmergencyCycledAutoPenalty (object sender, EventArgs e) {
            var tb = sender as TextBox;
            if (tb != null) {
                try {
                    var towers = Convert.ToInt32 (tb.Text);
                    if (towers < minimumEmergencyCycled) {
                        cbEmergencyCycledPenalty.Text = emergencyCycledPenaltyPointValue.ToString ();
                    } else {
                        cbEmergencyCycledPenalty.Text = "0";
                    }
                } catch {
                    //
                }
            }
        }

        private void OnValidation (object sender, EventArgs e) {
            UpdateScore ();
        }

        private void cbRocketPosition_TextChanged (object sender, EventArgs e) {
            var cb = sender as ComboBox;
            if (cb != null) {
                var rocketPositionMultiplier = RocketPosition.Loaded;

                cbRocketLaunched.Enabled = false;
                lbRocketLaunchedScore.Text = "0";

                switch (cb.Text) {
                case "Door Closed":
                    rocketPositionMultiplier = RocketPosition.DoorClosed;
                    break;
                case "Crawler Moved":
                    rocketPositionMultiplier = RocketPosition.CrawlerMoved;
                    break;
                case "Launch Position":
                    rocketPositionMultiplier = RocketPosition.LaunchPosition;
                    cbRocketLaunched.Enabled = true;
                    if (cbRocketLaunched.Checked) {
                        lbRocketLaunchedScore.Text = rocketLaunchedPointValue.ToString ();
                    }
                    break;
                default:
                    rocketPositionMultiplier = RocketPosition.Loaded;
                    break;
                }

                lbRocketPositionMulitplier.Text = ((int)rocketPositionMultiplier).ToString ();

                UpdateScore ();
            }
        }
    }
}
