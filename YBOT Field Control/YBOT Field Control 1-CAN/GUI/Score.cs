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

            tbGreenPenalty.Validated += PenaltyAutoDq;
            tbRedPenalty.Validated += PenaltyAutoDq;
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
            tbManualSolar1Score.Text = green.manSolarPanelScore1.ToString ();
            tbManualSolar2Score.Text = green.manSolarPanelScore2.ToString ();
            tbManualEmergencyCycled.Text = green.emergencyCleared.ToString ();
            if (green.emergencyCleared < minimumEmergencyCycled) {
                cbEmergencyCycledPenalty.Text = emergencyCycledPenaltyPointValue.ToString ();
            } else {
                cbEmergencyCycledPenalty.Text = "0";
            }

            tbGreenPenalty.Text = green.penalty.ToString ();
            tbRedPenalty.Text = red.penalty.ToString ();

            if (green.dq || (green.penalty >= 3)) {
                cbGreenDq.Checked = true;
            }

            if (red.dq || (red.penalty >= 3)) {
                cbRedDq.Checked = true;
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

            if (cbGreenDq.Checked || cbGreenDidntPlay.Checked) {
                greenScore = 0;
            }

            var redPenalty = Convert.ToInt32 (tbRedPenalty.Text);
            var redScore = jointScore - (redPenalty * teamPenalty);

            if (cbRedDq.Checked || cbRedDidntPlay.Checked) {
                redScore = 0;
            }

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

                green.manSolarPanelScore1 = manualSolar1;
                green.manSolarPanelScore2 = manualSolar2;
                green.emergencyCleared = manualEmergencyCycled;

                green.rockWeight = rockWeight;
                green.rockScore = rockScore;
                green.rocketPosition = rocketPositionMultiplier;
                green.rocketBonus = cbRocketLaunched.Checked;

                green.autoScore = autoScore;
                green.manScore = manualScore;
                green.score = jointScore;

                green.penalty = greenPenalty;
                if (cbGreenDq.Checked) {
                    green.dq = false;
                } else {
                    green.dq = true;
                }

                if (!green.dq) {
                    green.finalScore = greenScore;
                } else {
                    green.finalScore = 0;
                }

                if (!cbGreenDidntPlay.Checked) {
                    green.matchResult = "P";
                } else {
                    green.matchResult = "NP";
                }

                red.autoTowerTested = autoCornersTested;
                red.autoEmergencyTowerCycled = autoEmergencyCycled;
                red.autoSolarPanelScore = autoSolar;

                red.manSolarPanelScore1 = manualSolar1;
                red.manSolarPanelScore2 = manualSolar2;
                red.emergencyCleared = manualEmergencyCycled;

                red.rockWeight = rockWeight;
                red.rockScore = rockScore;
                red.rocketPosition = rocketPositionMultiplier;
                red.rocketBonus = cbRocketLaunched.Checked;

                red.autoScore = autoScore;
                red.manScore = manualScore;
                red.score = jointScore;

                red.penalty = redPenalty;
                if (cbRedDq.Checked) {
                    red.dq = false;
                } else {
                    red.dq = true;
                }

                if (!red.dq) {
                    red.finalScore = redScore;
                } else {
                    red.finalScore = 0;
                }

                if (!cbRedDidntPlay.Checked) {
                    red.matchResult = "P";
                } else {
                    red.matchResult = "NP";
                }
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

        protected void TextBoxValidation (object sender, CancelEventArgs e) {
            var tb = sender as TextBox;
            if (tb != null) {
                try {
                    var number = Convert.ToInt32 (tb.Text);

                    var tag = tb.Tag as string;
                    if (tag != null) {
                        if (!string.IsNullOrWhiteSpace (tag)) {
                            try {
                                var indexComma = tag.IndexOf (',');
                                var min = Convert.ToInt32 (tag.Substring (1, indexComma - 1));
                                var max = Convert.ToInt32 (tag.Substring (indexComma + 1, tag.Length - indexComma - 2));
                                if ((number < min) || (number > max)) {
                                    MessageBox.Show (string.Format ("Invalid number of towers\nMust be between {0} and {1}", min, max));
                                    e.Cancel = true;
                                }
                            } catch {
                                //
                            }
                        }
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
                            cbGreenDq.Checked = true;
                        } else if (tb.Name.Contains ("Red")) {
                            cbRedDq.Checked = true;
                        }
                    } else {
                        if (tb.Name.Contains ("Green")) {
                            cbGreenDq.Checked = false;
                        } else if (tb.Name.Contains ("Red")) {
                            cbRedDq.Checked = false;
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

        private void CheckedChanged (object sender, EventArgs e) {
            var cb = sender as CheckBox;
            if (cb != null) {
                if ((cb.Name.Contains ("Green")) && cb.Checked) {
                    if (cb.Name.Contains ("Dq")) {
                        cbGreenDidntPlay.Checked = false;
                    } else {
                        cbGreenDq.Checked = false;
                    }
                } else if ((cb.Name.Contains ("Red")) && cb.Checked) {
                    if (cb.Name.Contains ("Dq")) {
                        cbRedDidntPlay.Checked = false;
                    } else {
                        cbRedDq.Checked = false;
                    }
                }

                UpdateScore ();
            }
        }

        private void cbRocketPosition_TextChanged (object sender, EventArgs e) {
            var cb = sender as ComboBox;
            if (cb != null) {
                var rocketPositionMultiplier = RocketPosition.Loaded;

                cbRocketLaunched.Enabled = false;
                cbRocketLaunched.Checked = false;
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
