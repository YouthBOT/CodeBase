using System;
using System.Linq;
using System.Collections.Generic;

namespace YBotSqlWrapper
{
    public class Tournaments : IEnumerable<Tournament>
    {
        protected List<Tournament> tournaments;

        public Tournament this[int id] {
            get {
                return tournaments[id];
            }
        }

        public Tournament this[string name] {
            get {
                foreach (var t in tournaments) {
                    if (name == t.name) {
                        return t;
                    }
                }
                return null;
            }
        }

        public Tournaments () {
            tournaments = new List<Tournament> ();
        }

        public Tournaments (IEnumerable<Tournament> tournaments) : this ()  {
            foreach (var t in tournaments) {
                this.tournaments.Add (t);
            }
        }

        public void Add (Tournament tournament) {
            tournaments.Add (tournament);
        }

        public IEnumerator<Tournament> GetEnumerator () {
            return tournaments.GetEnumerator ();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
            return GetEnumerator ();
        }
    }

    public class Tournament
    {
        public int id;
        public DateTime date;
        public string name;

        public Tournament (int id, DateTime date, string name) {
            this.id = id;
            this.date = date;
            this.name = name;
        }
    }
}
