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

            tbManualEmergencyCycled.Validated += (sender, args) => {
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
            };
        }

        private void Score_Shown (object sender, EventArgs e) {
            InitScore ();
        }

        private void Score_FormClosed (object sender, FormClosedEventArgs e) {
            InitScore ();
            finalScore = true;
        }

        private void InitScore () {
            var green = game.green;
            var red = game.red;

            tbGreenPenalty.Text = green.penalty.ToString ();
            tbRedPenalty.Text = red.penalty.ToString ();

            if (green.dq) {
                btnGreenDq.BackColor = Color.Lime;
            }

            if (red.dq) {
                btnRedDq.BackColor = Color.Red;
            }

            green.score = green.finalScore;
            red.score = red.finalScore;

            green.finalScore = green.score - (green.penalty * teamPenalty);
            red.finalScore = red.score - (red.penalty * teamPenalty);

            tbGreenScore.Text = green.finalScore.ToString();
            tbRedScore.Text = red.finalScore.ToString();
        }

        protected void UpdateScore () {
            var autoCornersTested = Convert.ToInt32 (tbAutoCornersTested.Text) * autoCornersTestedPointValue;
            var autoEmergencyCycled = Convert.ToInt32 (tbAutoEmergencyCycled.Text) * autoEmergencyCycledPointValue;
            var autoSolar = Convert.ToInt32 (tbAutoSolarScore.Text);
            var manSolar1 = Convert.ToInt32 (tbManualSolar1Score.Text);
            var manSolar2 = Convert.ToInt32 (tbManualSolar2Score.Text);
            var manualEmergencyCycled = Convert.ToInt32 (tbManualEmergencyCycled.Text) * manualEmergencyCycledPointValue;
            var emergencyCycledPenalty = Convert.ToInt32 (cbEmergencyCycledPenalty.Text);
            var rocketPositionMultiplier = Convert.ToInt32 (lbRocketPositionMulitplier.Text);
            var rockWeight = Convert.ToInt32 (tbRockWeight.Text);
            var rockScore = rockWeight * rocketPositionMultiplier;
            var rocketLaunched = 0;
            if (cbRocketLaunched.Checked) {
                rocketLaunched = rocketLaunchedPointValue;
            }
            var autoScore = autoCornersTested + autoEmergencyCycled + autoSolar;
            var manualScore = manSolar1 + manSolar2 + manualEmergencyCycled - emergencyCycledPenalty + rockScore + rocketLaunched;
            var jointScore = autoScore + manualScore;

            var greenPenalty = Convert.ToInt32 (tbGreenPenalty.Text) * teamPenalty;
            var greenScore = jointScore - greenPenalty;

            var redPenalty = Convert.ToInt32 (tbRedPenalty.Text) * teamPenalty;
            var redScore = jointScore - redPenalty;

            lbAutoCornerTestedScore.Text = autoCornersTested.ToString ();
            lbAutoEmergencyCycledScore.Text = autoEmergencyCycled.ToString ();
            lbManualEmergencyCycledScore.Text = manualEmergencyCycled.ToString ();
        }

        private void btnGreenDq_Click (object sender, EventArgs e) {
            if (!game.green.dq) {
                btnGreenDq.BackColor = Color.Lime;
                game.green.dq = true;
            } else {
                btnGreenDq.BackColor = DefaultBackColor;
                game.green.dq = false;
            }
        }

        private void btnRedDq_Click (object sender, EventArgs e) {
            if (!game.red.dq) {
                btnRedDq.BackColor = Color.Red;
                game.red.dq = true;
            } else {
                btnRedDq.BackColor = DefaultBackColor;
                game.red.dq = false;
            }
        }

        private void btnFinalScore_Click (object sender, EventArgs e) {
            UpdateScore ();
            finalScore = true;
        }

        private void btnUpdateScore_Click (object sender, EventArgs e) {
            InitScore ();
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

                tbGreenScore.Enabled = true;
                tbRedScore.Enabled = true;

                btnOverride.BackColor = Color.SteelBlue;
                manualOverride = true;
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

                tbGreenScore.Enabled = false;
                tbRedScore.Enabled = false;

                btnOverride.BackColor = DefaultBackColor;
                manualOverride = false;
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
            }
        }

        private void cbRocketPosition_TextChanged (object sender, EventArgs e) {
            var cb = sender as ComboBox;
            if (cb != null) {
                var rocketPositionMultiplier = RocketPosition.Loaded;

                switch (cb.Text) {
                case "Door Closed":
                    rocketPositionMultiplier = RocketPosition.DoorClosed;
                    break;
                case "Crawler Moved":
                    rocketPositionMultiplier = RocketPosition.CrawlerMoved;
                    break;
                case "Launch Position":
                    rocketPositionMultiplier = RocketPosition.LaunchPosition;
                    break;
                default:
                    rocketPositionMultiplier = RocketPosition.Loaded;
                    break;
                }

                lbRocketPositionMulitplier.Text = ((int)rocketPositionMultiplier).ToString ();
            }
        }
    }
}
