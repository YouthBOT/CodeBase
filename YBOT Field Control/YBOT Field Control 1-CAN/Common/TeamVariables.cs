using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBOT_Field_Control_2016
{ 
    public class Team
    {
        //public Variables
        public string teamColor;        //Team color "red" or "green"
        public int score;               //Team's score
        public int autoCount;           //Autonomous Mode Counter
        public int finalScore;          //Team's final score
        public int penalty;             //Penalty amount
        public bool dq;                 //Team DQ flag
        public string matchResult;      //Match Result string
        public bool autoFinished;       //Autonomous mode finished flag
        public int autoScore;           //Autonomous mode score
        public int manScore;            //Middle round score
        public int endGameScore;        //End of game score
        public bool autoMan;            //Mantonomous Flag

        //------------------------------------------------------------------------------------------------\\
        //Current year's game variables
        //------------------------------------------------------------------------------------------------\\


        public Team(string _teamColor)
        {
            teamColor = _teamColor;
        }

        /// <summary>
        /// Resets variable
        /// </summary>
        public void reset()
        {
            this.score = 0;
            this.autoCount = 0;
            this.finalScore = 0;
            this.penalty = 0;
            this.dq = false;
            this.matchResult = null;
            this.autoFinished = false;
            this.autoScore = 0;
            this.manScore = 0;
            this.endGameScore = 0;
            this.autoMan = false;

        }

    }

}
