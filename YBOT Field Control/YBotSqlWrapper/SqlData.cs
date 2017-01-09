using System;

namespace YBotSqlWrapper
{
    public class SqlData
    {
        protected static SqlData _global = new SqlData ();
        public static SqlData Global {
            get {
                return _global;
            }
        }

        public Tournaments tournaments;
        public Schools schools;

        protected SqlData () {
            tournaments = new Tournaments ();
            schools = new Schools ();
        }
    }
}
