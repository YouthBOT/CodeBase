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

        private int _autoTowerTested;           //Number of towers tested in automode
        public int autoTowerTested
        {
            get { return _autoTowerTested; }
            set { _autoTowerTested = value; }
        }

        private int _autoEmergencyTowerCycled;  //Number of towers cycled in automode
        public int autoEmergencyTowerCycled
        {
            get { return _autoEmergencyTowerCycled; }
            set { _autoEmergencyTowerCycled = value; }
        }

        private int _autoSolarPanelScore;       //Solar panel score for autonomous mode
        public int autoSolarPanelScore
        {
            get { return _autoSolarPanelScore; }
            set { _autoSolarPanelScore = value; }
        }

        private int _emergencyCleared;          //Number of towers cleared of emergency during manual mode
        public int emergencyCleared
        {
            get { return _emergencyCleared; }
            set { _emergencyCleared = value; }
        }

        private bool _rocketBonus;              //If the teams earn the rocket bonus
        public bool rocketBonus
        {
            get { return _rocketBonus; }
            set { _rocketBonus = value; }
        }

        private int _rocketPosition;            //Rocket position at the end of the game
        public int rocketPosition
        {
            get { return _rocketPosition; }
            set { _rocketPosition = value; }
        }

        private int _rockWeight;                //Rock weight in ounces
        public int rockWeight
        {
            get { return _rockWeight; }
            set { _rockWeight = value; }
        }

        private int _rockValue;                 //Final rock value
        public int rockValue
        {
            get { return _rockValue; }
            set { _rockValue = value; }
        }

        private int _rockScore;                 //Final rock score
        public int rockScore
        {
            get { return _rockScore; }
            set { _rockScore = value; }
        }


        //Team color 
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

            autoEmergencyTowerCycled = 0;
            autoSolarPanelScore = 0;
            autoTowerTested = 0;
            rocketBonus = false;
            emergencyCleared = 0;
            rocketPosition = 0;
            rockScore = 0;
            rockValue = 0;
            rockWeight = 0;
        }
    }
}
