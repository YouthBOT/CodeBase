using System;

namespace YBotSqlWrapper
{
    public class YBotSqlData
    {
        protected static YBotSqlData _global = new YBotSqlData ();
        public static YBotSqlData Global {
            get {
                return _global;
            }
        }

        public Tournaments tournaments;
        public Schools schools;
		public string currentTournament;
		public int currentMatchNuumber;

        protected YBotSqlData () {
            tournaments = new Tournaments ();
            schools = new Schools ();
			currentTournament = string.Empty;
			currentMatchNuumber = -1;
        }
    }
}
