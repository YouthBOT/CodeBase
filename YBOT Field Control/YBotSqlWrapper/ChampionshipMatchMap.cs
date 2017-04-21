using System;
using System.Collections.Generic;

namespace YBotSqlWrapper
{
    public class ChampionshipMatchMap
    {
        protected static ChampionshipMatchMap instance = new ChampionshipMatchMap();
        public static ChampionshipMatchMap Instance {
            get {
                return instance;
            }
        }

        int[] _matchMap;

        public int this[int index] {
            get {
                return _matchMap[index];
            }
        }

        public ChampionshipMatchMap() {
            _matchMap = new int[29];
            _matchMap[0] = 0;
            _matchMap[1] = 111;
            _matchMap[2] = 121;
            _matchMap[3] = 131;
            _matchMap[4] = 141;
            _matchMap[5] = 151;
            _matchMap[6] = 161;
            _matchMap[7] = 112;
            _matchMap[8] = 122;
            _matchMap[9] = 132;
            _matchMap[10] = 142;
            _matchMap[11] = 152;
            _matchMap[12] = 162;
            _matchMap[13] = 211;
            _matchMap[14] = 221;
            _matchMap[15] = 231;
            _matchMap[16] = 241;
            _matchMap[17] = 212;
            _matchMap[18] = 222;
            _matchMap[19] = 232;
            _matchMap[20] = 242;
            _matchMap[21] = 331;
            _matchMap[22] = 341;
            _matchMap[23] = 332;
            _matchMap[24] = 342;
            _matchMap[25] = 311;
            _matchMap[26] = 321;
            _matchMap[27] = 312;
            _matchMap[28] = 322;
        }
    }

    public class ChampionshipBracketMap
    {
        protected static ChampionshipBracketMap instance = new ChampionshipBracketMap();
        public static ChampionshipBracketMap Instance {
            get {
                return instance;
            }
        }

        Dictionary<int, int> _matchMap;

        public int this[int index] {
            get {
                return _matchMap[index];
            }
        }

        public ChampionshipBracketMap() {
            _matchMap = new Dictionary<int, int>();
            _matchMap.Add(12, 220);
            _matchMap.Add(14, 230);
            _matchMap.Add(16, 240);
            _matchMap.Add(22, 310); // winner of finals round 1
            _matchMap.Add(24, 320); // winder of finals round 2
            _matchMap.Add(21, 330); // loser of finals round 1
            _matchMap.Add(23, 340); // loser of finals round 2
        }
    }
}
