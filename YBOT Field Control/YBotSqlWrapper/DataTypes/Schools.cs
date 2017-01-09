using System;
using System.Linq;
using System.Collections.Generic;

namespace YBotSqlWrapper
{
    public class Schools : IEnumerable<School>
    {
        protected List<School> schools;

        public School this[int id] {
            get {
                return schools[id];
            }
        }

        public School this[string name] {
            get {
                foreach (var s in schools) {
                    if (name == s.name) {
                        return s;
                    }
                }
                return null;
            }
        }

        public Schools () {
            schools = new List<School> ();
        }

        public Schools (IEnumerable<School> schools) : this () {
            foreach (var s in schools) {
                this.schools.Add (s);
            }
        }

        public void Add (School school) {
            schools.Add (school);
        }

        public IEnumerator<School> GetEnumerator () {
            return schools.GetEnumerator ();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
            return GetEnumerator ();
        }
    }

    public class School
    {
        public int id;
        public string name;

        public School (int id, string name) {
            this.id = id;
            this.name = name;
        }
    }
}
