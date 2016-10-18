using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBOT_Field_Control_2016
{
    public partial class GameControl
    {

        Field_Control fc;
        GameDisplay GD = new GameDisplay();
        Time time = new Time();
        LogWriter lw = new LogWriter();
        fileStructure fs = new fileStructure();
        StringBuilder logBuilder = new StringBuilder();

        private string filePath                     //Construct filePath to Node data
        {
            get
            {
                string path = fs.xmlFilePath;
                return path;
            }
        }
        private string xmlHeader                    //Construct xml Header
        {
            get
            {
                string header = fs.xmlHeader;
                return header;
            }
        }
        private int matchNumber = 0;

        private string timeOfDay =                //Time of day stamp
              DateTime.Now.Hour.ToString() + "_"
            + DateTime.Now.Minute.ToString() + "_"
            + DateTime.Now.Second.ToString();


        public Team red = new Team("red");            //New team 
        public Team green = new Team("green");        //New team

        public int autoModeTime = 30;   //Autonomous Mode time in secs
        public int manAutoTime = 20;    //Mantonomous Mode start time
        public int midModeTime = 120;    //Mid Mode time + automode time


        //Game flags
        public GameModes gameMode = GameModes.off;
        public bool practiceMode = false;   //True when game is in practice mode


        //------------------------------------------------------------------------------------------------\\
        //Current year's game variables
        //------------------------------------------------------------------------------------------------\\


    }
}
