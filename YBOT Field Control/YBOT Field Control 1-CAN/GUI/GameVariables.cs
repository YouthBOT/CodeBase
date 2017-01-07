﻿using System;
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

        private string timeOfDay = DateTime.Now.ToString("HH_mm_ss"); //Time of day stamp

        public Team red = new Team("red");            //New team 
        public Team green = new Team("green");        //New team

        public int autoModeTime = 30;   //Autonomous Mode time in secs
        public int manAutoTime = 20;    //Mantonomous Mode start time
        public int midModeTime = 120;   //Mid Mode time + automode time

        //Game flags
        public GameModes gameMode = GameModes.off;
        public bool practiceMode = false;   //True when game is in practice mode

        //------------------------------------------------------------------------------------------------\\
        //Current year's game variables
        //------------------------------------------------------------------------------------------------\\

        private Random rndNum = new Random();   //Random Number
        private int sunTower = 0;               //Tower number to be the sun tower
        public Team joint = new Team("joint");  //Joint team values
    }
}
