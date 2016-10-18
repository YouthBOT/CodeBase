using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YBOT_Field_Control_2016
{
    public partial class Score : Form
    {

        public bool finalScore = false;
        public bool manualOverride = false;
        GameControl game;

        public Score()
        {
            InitializeComponent();
            this.game = new GameControl();
        }

        public Score(GameControl _game)
        {
            InitializeComponent();
            this.game = _game;
        }

        private void Score_Shown(object sender, EventArgs e)
        {
            //ENTER THIS YEAR'S SCORE CODE HERE

            this.game.green.score = this.game.green.finalScore;
            this.game.red.score = this.game.red.finalScore;

            this.game.green.finalScore = this.game.green.score - (this.game.green.penalty * 10);
            this.game.red.finalScore = this.game.red.score - (this.game.red.penalty * 10);

            this.mtbGreenFinalScore.Text = this.game.green.finalScore.ToString();
            this.mtbRedFinalScore.Text = this.game.red.finalScore.ToString();

        }

        private void updateFinalScore()
        {

            //ENTER THIS YEAR'S SCORE CODE HERE

            this.game.green.score = this.game.green.finalScore;
            this.game.red.score = this.game.red.finalScore;

            this.game.green.finalScore = this.game.green.score - (this.game.green.penalty * 10);
            this.game.red.finalScore = this.game.red.score - (this.game.red.penalty * 10);

            this.mtbGreenFinalScore.Text = this.game.green.finalScore.ToString();
            this.mtbRedFinalScore.Text = this.game.red.finalScore.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.BackColor == DefaultBackColor)
            {
                button1.BackColor = Color.Lime;
                this.game.green.dq = true;
            }
            else
            {
                button1.BackColor = DefaultBackColor;
                this.game.green.dq = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.BackColor == DefaultBackColor)
            {
                button2.BackColor = Color.Red;
                this.game.red.dq = true;
            }
            else
            {
                button2.BackColor = DefaultBackColor;
                this.game.red.dq = false;
            }
        }

        private void btnFinalScore_Click(object sender, EventArgs e)
        {

            updateFinalScore();



            this.finalScore = true;

        }

        private void btnUpdateScore_Click(object sender, EventArgs e)
        {
            updateFinalScore();
        }


        private void MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            MessageBox.Show("Enter Valid Number", "ERROR");
        }

        private void Score_FormClosed(object sender, FormClosedEventArgs e)
        {
            updateFinalScore();
            this.finalScore = true;
        }

    }
}
