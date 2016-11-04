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
        private string _teamColor;        //Team color "red" or "green"
        public string teamColor
        {
            get
            {
                return _teamColor;
            }
        }

        private int _score;               //Team's score
        public int score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }

        private int _autoCount;           //Autonomous Mode Counter
        public int autoCount
        {
            get
            {
                return _autoCount;
            }
            set
            {
                _autoCount = value;
            }
        }

        private int _finalScore;          //Team's final score
        public int finalScore
        {
            get
            {
                return _finalScore;
            }
            set
            {
                _finalScore = value;
            }
        }

        private int _penalty;             //Penalty amount
        public int penalty
        {
            get
            {
                return _penalty;
            }
            set
            {
                _penalty = value;
            }
        }

        private bool _dq;                 //Team DQ flag
        public bool dq
        {
            get
            {
                return _dq;
            }
            set
            {
                _dq = value;
            }
        }

        private string _matchResult;      //Match Result string
        public string matchResult
        {
            get
            {
                return _matchResult;
            }
            set
            {
                _matchResult = value;
            }
        }

        private bool _autoFinished;       //Autonomous mode finished flag
        public bool autoFinished
        {
            get
            {
                return _autoFinished;
            }
            set
            {
                _autoFinished = value;
            }
        }

        private int _autoScore;           //Autonomous mode score
        public int autoScore
        {
            get
            {
                return _autoScore;
            }
            set
            {
                _autoScore = value;
            }
        }

        private int _manScore;            //Middle round score
        public int manScore
        {
            get
            {
                return _manScore;
            }
            set
            {
                _manScore = value;
            }
        }

        private int _endGameScore;        //End of game score
        public int endGameScore
        {
            get
            {
                return _endGameScore;
            }
            set
            {
                _endGameScore = value;
            }
        }

        private bool _autoMan;            //Mantonomous Flag
        public bool autoMan
        {
            get
            {
                return _autoMan;
            }
            set
            {
                _autoMan = value;
            }
        }

        //------------------------------------------------------------------------------------------------\\
        //Current year's game variables
        //------------------------------------------------------------------------------------------------\\


        public Team(string teamColor)
        {
            _teamColor = teamColor;
        }

        /// <summary>
        /// Resets variable
        /// </summary>
        public void reset()
        {
            score = 0;
            autoCount = 0;
            finalScore = 0;
            penalty = 0;
            dq = false;
            matchResult = null;
            autoFinished = false;
            autoScore = 0;
            manScore = 0;
            endGameScore = 0;
            autoMan = false;
        }
    }
}
