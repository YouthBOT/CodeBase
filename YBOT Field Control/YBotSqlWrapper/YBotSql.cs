﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using HelpfulUtilites;

namespace YBotSqlWrapper
{
    public delegate void SqlMessageHandler(object sender, SqlMessageArgs args);
    public delegate void SqlStatusHandler(object sender);

    public class YbotSql
    {
        protected static YbotSql _instance = new YbotSql();
        public static YbotSql Instance {
            get {
                return _instance;
            }
        }

        private MySqlConnection sql;
        private SshClient ssh;

        public bool IsConnected {
            get {
                if (sql != null) {
                    return !((sql.State == ConnectionState.Broken) ||
                        (sql.State == ConnectionState.Closed) ||
                        (sql.State == ConnectionState.Connecting));
                } else {
                    return false;
                }
            }
        }

        protected YbotSql() {

        }

        public event SqlMessageHandler SqlMessageEvent;
        public event SqlStatusHandler SqlConnectedEvent;

        public void Connect(string serverIp, string password, bool useSsh = true) {
            Thread connectThread = new Thread(() => {
                if (useSsh) {
                    ConnectSsh(serverIp, password);
                    serverIp = "127.0.0.1";
                }
                ConnectMySql(serverIp, password);
            });

            connectThread.Start();
        }

        protected void ConnectSsh(string server, string password) {
            if (ssh == null) { // if the ssh object is not null, then a tunnel has already been created
                ssh = new SshClient(server, 22, "youthbot", password);
                SqlMessageEvent?.Invoke(this, new SqlMessageArgs("Connecting to SSH server..."));

                try {
                    ssh.Connect();
                } catch (Exception ex) {
                    string text = "failure\n" + ex.ToString();
                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs(text));
                    return;
                }

                if (ssh.IsConnected) {
                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs("connected\n"));

                    var tunnel = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                    ssh.AddForwardedPort(tunnel);
                    tunnel.Start();
                }
            }
        }

        protected void ConnectMySql(string server, string password) {
            var sb = new MySqlConnectionStringBuilder();
            sb.Server = server;
            sb.Port = 3306;
            sb.Database = "ybot";
            sb.UserID = "youthbot";
            sb.Password = password;

            sql = new MySqlConnection(sb.ConnectionString);

            try {
                SqlMessageEvent?.Invoke(this, new SqlMessageArgs("Connecting to MySQL server..."));
                sql.Open();
                SqlMessageEvent?.Invoke(this, new SqlMessageArgs("connected\n"));
                SqlConnectedEvent?.Invoke(this);

                return;
            } catch (MySqlException ex) {
                if (ssh != null) {
                    ssh.Disconnect();
                    ssh.Dispose();
                }

                string text = "failure\n" + ex.ToString();
                SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, text));
                return;
            }
        }

        public void Disconnect() {
            sql.Close();
            sql.Dispose();

            if (ssh != null) {
                ssh.Disconnect();
                ssh.Dispose();
            }
        }

        public async void AddLog(string text, string type) {
            if ((sql != null) && (IsConnected)) {
                if (text.IsNotEmpty()) {
                    var query = "INSERT INTO event_log (event_id, event_type, event_message";
                    if (YBotSqlData.Global.currentTournament.IsNotEmpty()) {
                        query += ", tournament_id";
                    }
                    if (YBotSqlData.Global.currentMatchNumber != -1) {
                        query += ", match_number";
                    }
                    query += ") ";
                    query += string.Format("VALUES (NOW(), '{0}', '{1}'", type, text);
                    if (YBotSqlData.Global.currentTournament.IsNotEmpty()) {
                        query += string.Format(", '{0}'", YBotSqlData.Global.tournaments[YBotSqlData.Global.currentTournament].id);
                    }
                    if (YBotSqlData.Global.currentMatchNumber != -1) {
                        query += string.Format(", '{0}'", YBotSqlData.Global.currentMatchNumber);
                    }
                    query += ");";

                    var command = new MySqlCommand(query, sql);

                    try {
                        await command.ExecuteNonQueryAsync();
                    } catch (MySqlException ex) {
                        SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                    }
                }
            }
        }

        public async void AddMatch(Match match) {
            if ((sql != null) && (IsConnected)) {
                var query = "SELECT match_id FROM matches " +
                    string.Format("WHERE tournament_id={0} and match_number={1};", match.tournamentId, match.matchNumber);

                var command = new MySqlCommand(query, sql);
                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync()) {
                    var obj = reader[0];
                    if (obj.GetType() != typeof(System.DBNull)) {
                        var id = Convert.ToInt32(obj);
                        reader.Close();

                        if (match.tournamentId == 5) {
                            query = "UPDATE matches " +
                                "SET played = 1, " +
                                string.Format("green_team = {0}, green_score = {1}, green_penalty = {2}, green_dq = {3}, green_result = '{4}', ",
                                    match.greenTeam, match.greenScore, match.greenPenalty, match.greenDq, "S") +
                                string.Format("auto_corners_tested = {0}, auto_emergency_cycled = {1}, auto_solar_panel = {2}, ",
                                    match.autoCornersTested, match.autoEmergencyCycled, match.autoSolarPanel) +
                                string.Format("manual_solar_panel_1 = {0}, manual_solar_panel_2 = {1}, manual_emergency_cleared = {2}, ",
                                    match.manSolarPanel1, match.manSolarPanel2, match.manualEmergencyCleared) +
                                string.Format("rocket_position = {0}, rock_weight = {1}, rock_score = {2}, rocket_bonus = {3} ",
                                    match.rocketPosition, match.rockWeight, match.rockScore, match.rocketBonus) +
                                string.Format("WHERE match_id = {0};", id);
                        } else {
                            query = "UPDATE matches " +
                                "SET played = 1, " +
                                string.Format("red_team = {0}, red_score = {1}, red_penalty = {2}, red_dq = {3}, red_result = '{4}', ",
                                    match.redTeam, match.redScore, match.redPenalty, match.redDq, match.redResult) +
                                string.Format("green_team = {0}, green_score = {1}, green_penalty = {2}, green_dq = {3}, green_result = '{4}', ",
                                    match.greenTeam, match.greenScore, match.greenPenalty, match.greenDq, match.greenResult) +
                                string.Format("auto_corners_tested = {0}, auto_emergency_cycled = {1}, auto_solar_panel = {2}, ",
                                    match.autoCornersTested, match.autoEmergencyCycled, match.autoSolarPanel) +
                                string.Format("manual_solar_panel_1 = {0}, manual_solar_panel_2 = {1}, manual_emergency_cleared = {2}, ",
                                    match.manSolarPanel1, match.manSolarPanel2, match.manualEmergencyCleared) +
                                string.Format("rocket_position = {0}, rock_weight = {1}, rock_score = {2}, rocket_bonus = {3} ",
                                    match.rocketPosition, match.rockWeight, match.rockScore, match.rocketBonus) +
                                string.Format("WHERE match_id = {0};", id);
                        }

                        command = new MySqlCommand(query, sql);

                        try {
                            await command.ExecuteNonQueryAsync();
                        } catch (MySqlException ex) {
                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                        }

                        #region Championship Hacking

                        if ((match.tournamentId == 5) && (match.matchNumber > 100)) {
                            if (match.matchNumber.ToString().EndsWith("2")) {
                                query = "SELECT green_score FROM matches " +
                                    string.Format("WHERE tournament_id=5 and match_number={0};", match.matchNumber - 1);

                                command = new MySqlCommand(query, sql);
                                reader = await command.ExecuteReaderAsync();

                                if (await reader.ReadAsync()) {
                                    obj = reader[0];
                                    if (obj.GetType() != typeof(System.DBNull)) {
                                        var firstMatchScore = Convert.ToInt32(obj);
                                        reader.Close();

                                        var averageScore = (double)(firstMatchScore + match.greenScore) / 2.0;

                                        query = "UPDATE matches " +
                                            "SET played = 1, " +
                                            string.Format("green_team = {0}, green_score = {1}, green_result = 'A' ",
                                                match.greenTeam, averageScore) +
                                            string.Format("WHERE match_id = {0};", id - 2);

                                        command = new MySqlCommand(query, sql);

                                        try {
                                            await command.ExecuteNonQueryAsync();
                                        } catch (MySqlException ex) {
                                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                                        }


                                        var roundNumber = match.matchNumber / 10;
                                        if ((roundNumber % 2 == 0) && (roundNumber < 30)) {
                                            var lowerBound = (roundNumber - 1) * 10;
                                            var upperBound = roundNumber * 10 + 9;

                                            query = "SELECT * FROM matches " +
                                                string.Format("WHERE match_number BETWEEN {0} AND {1};", lowerBound, upperBound);

                                            command = new MySqlCommand(query, sql);
                                            reader = await command.ExecuteReaderAsync();

                                            List<Match> matches = new List<Match>();
                                            while (await reader.ReadAsync()) {
                                                if (reader[13].ToString() == "S") {
                                                    var m = new Match();
                                                    m.greenTeam = Convert.ToInt32(reader[9]);
                                                    m.greenScore = Convert.ToInt32(reader[10]);

                                                    matches.Add(m);
                                                }
                                            }

                                            reader.Close();

                                            if (matches.Count == 4) {
                                                var team1Score = (double)(matches[0].greenScore + matches[1].greenScore) / 2.0;
                                                var team2Score = (double)(matches[2].greenScore + matches[3].greenScore) / 2.0;

                                                int winningTeam = 0;
                                                int losingTeam = 0;

                                                if (team1Score > team2Score) {
                                                    winningTeam = matches[0].greenTeam;
                                                    losingTeam = matches[2].greenTeam;
                                                } else if (team2Score > team1Score) {
                                                    winningTeam = matches[2].greenTeam;
                                                    losingTeam = matches[0].greenTeam;
                                                } else { // the average scores are equal
                                                    int highestTeam = -1;
                                                    int highestScore = -250;
                                                    for (int i = 0; i < 4; ++i) {
                                                        if (matches[i].greenScore > highestScore) {
                                                            highestScore = matches[i].greenScore;
                                                            highestTeam = i;
                                                        } else if ((matches[i].greenScore == highestScore) && (matches[i].greenTeam != highestTeam)) {
                                                            highestTeam = -1;
                                                        }
                                                    }

                                                    if (highestTeam != -1) {
                                                        if (highestTeam <= 1) {
                                                            winningTeam = matches[0].greenTeam;
                                                            losingTeam = matches[2].greenTeam;
                                                        } else {
                                                            winningTeam = matches[2].greenTeam;
                                                            losingTeam = matches[0].greenTeam;
                                                        }
                                                    }
                                                }

                                                if (winningTeam != 0) {
                                                    for (int i = 0; i < 3; ++i) {
                                                        query = "INSERT INTO matches " +
                                                            "(tournament_id, match_number, played, green_team) " +
                                                            string.Format("VALUES ({0}, {1}, 0, {2});",
                                                                          match.tournamentId,
                                                                          ChampionshipBracketMap.Instance[roundNumber] + i,
                                                                          winningTeam);

                                                        command = new MySqlCommand(query, sql);

                                                        try {
                                                            await command.ExecuteNonQueryAsync();
                                                        } catch (MySqlException ex) {
                                                            Console.WriteLine(ex.ToString());
                                                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                                                        }
                                                    }
                                                }

                                                if ((roundNumber >= 20) && (losingTeam != 0)) { // finals loser round 3rd and 4th place
                                                    for (int i = 0; i < 3; ++i) {
                                                        query = "INSERT INTO matches " +
                                                            "(tournament_id, match_number, played, green_team) " +
                                                            string.Format("VALUES ({0}, {1}, 0, {2});",
                                                                          match.tournamentId,
                                                                          ChampionshipBracketMap.Instance[roundNumber - 1] + i,
                                                                          losingTeam);

                                                        command = new MySqlCommand(query, sql);

                                                        try {
                                                            await command.ExecuteNonQueryAsync();
                                                        } catch (MySqlException ex) {
                                                            Console.WriteLine(ex.ToString());
                                                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion

                    } else {
                        await AddNewMatch(match);
                    }
                } else {
                    await AddNewMatch(match);
                }

                if (!reader.IsClosed) {
                    reader.Close();
                }
            }
        }

        private async Task AddNewMatch(Match match) {
            if (match.tournamentId == 6) {
                var query = "INSERT INTO matches " +
                    "(tournament_id, match_number, played, " +
                    "red_team, red_score, red_penalty, red_dq, red_result, " +
                    "green_team, green_score, green_penalty, green_dq, green_result, " +
                    "auto_corners_tested, auto_emergency_cycled, auto_solar_panel, " +
                    "manual_solar_panel_1, manual_solar_panel_2, manual_emergency_cleared, " +
                    "rocket_position, rock_weight, rock_score, rocket_bonus) " +
                    string.Format("VALUES ({0}, {1}, 1, ", match.tournamentId, match.matchNumber) +
                    string.Format("{0}, {1}, {2}, {3}, '{4}', ", match.redTeam, match.redScore, match.redPenalty, match.redDq, match.redResult) +
                    string.Format("{0}, {1}, {2}, {3}, '{4}', ", match.greenTeam, match.greenScore, match.greenPenalty, match.greenDq, match.greenResult) +
                    string.Format("{0}, {1}, {2}, ", match.autoCornersTested, match.autoEmergencyCycled, match.autoSolarPanel) +
                    string.Format("{0}, {1}, {2}, ", match.manSolarPanel1, match.manSolarPanel2, match.manualEmergencyCleared) +
                    string.Format("{0}, {1}, {2}, {3});", match.rocketPosition, match.rockWeight, match.rockScore, match.rocketBonus);

                var command = new MySqlCommand(query, sql);

                try {
                    await command.ExecuteNonQueryAsync();
                } catch (MySqlException ex) {
                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                }
            } else {
                SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.General, "Can't add matches to this tournament"));
            }
        }

        /*
        public async Task AverageChampionshipMatches(Match match) {
            if (match.matchNumber.ToString().EndsWith("2")) {
                var query = "SELECT green_score FROM matches " +
                    string.Format("WHERE tournament_id=5 and match_number={0};", match.matchNumber - 1);

                var command = new MySqlCommand(query, sql);
                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync()) {
                    var obj = reader[0];
                    if (obj.GetType() != typeof(System.DBNull)) {
                        var firstMatchScore = Convert.ToInt32(obj);
                        reader.Close();

                        var averageScore = (double)(firstMatchScore + match.greenScore) / 2.0;

                        query = "UPDATE matches " +
                            "SET played = 1, " +
                            string.Format("green_team = {0}, green_score = {1}, green_result = 'A' ",
                                match.greenTeam, averageScore) +
                            string.Format("WHERE match_id = {0};", id - 2);

                        command = new MySqlCommand(query, sql);

                        try {
                            await command.ExecuteNonQueryAsync();
                        } catch (MySqlException ex) {
                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                        }
                    }
                }
            }
        }
        */

        /*
        public async Task UpdateNextChampionshipRound(Match match) {
            if (match.matchNumber.ToString().EndsWith("2")) {
                var roundNumber = match.matchNumber / 10;
                if ((roundNumber % 2 == 0) && (roundNumber < 30)) {
                    var lowerBound = (roundNumber - 1) * 10;
                    var upperBound = roundNumber * 10 + 9;

                    var query = "SELECT * FROM matches " +
                        string.Format("WHERE match_number BETWEEN {0} AND {1};", lowerBound, upperBound);

                    var command = new MySqlCommand(query, sql);
                    var reader = await command.ExecuteReaderAsync();

                    List<Match> matches = new List<Match>();
                    while (await reader.ReadAsync()) {
                        if (reader[13].ToString() == "S") {
                            var m = new Match();
                            m.greenTeam = Convert.ToInt32(reader[9]);
                            m.greenScore = Convert.ToInt32(reader[10]);

                            matches.Add(m);
                        }
                    }

                    reader.Close();

                    if (matches.Count == 4) {
                        var team1Score = (double)(matches[0].greenScore + matches[1].greenScore) / 2.0;
                        var team2Score = (double)(matches[2].greenScore + matches[3].greenScore) / 2.0;

                        int winningTeam = 0;
                        int losingTeam = 0;

                        if (team1Score > team2Score) {
                            winningTeam = matches[0].greenTeam;
                            losingTeam = matches[2].greenTeam;
                        } else if (team2Score > team1Score) {
                            winningTeam = matches[2].greenTeam;
                            losingTeam = matches[0].greenTeam;
                        } else { // the average scores are equal
                            int highestTeam = -1;
                            int highestScore = -250;
                            for (int i = 0; i < 4; ++i) {
                                if (matches[i].greenScore > highestScore) {
                                    highestScore = matches[i].greenScore;
                                    highestTeam = i;
                                } else if ((matches[i].greenScore == highestScore) && (matches[i].greenTeam != highestTeam)) {
                                    highestTeam = -1;
                                }
                            }

                            if (highestTeam != -1) {
                                if (highestTeam <= 1) {
                                    winningTeam = matches[0].greenTeam;
                                    losingTeam = matches[2].greenTeam;
                                } else {
                                    winningTeam = matches[2].greenTeam;
                                    losingTeam = matches[0].greenTeam;
                                }
                            }
                        }

                        if (winningTeam != 0) {
                            for (int i = 0; i < 3; ++i) {
                                query = "INSERT INTO matches " +
                                    "(tournament_id, match_number, played, green_team) " +
                                    string.Format("VALUES ({0}, {1}, 0, {2});",
                                                  match.tournamentId,
                                                  ChampionshipBracketMap.Instance[roundNumber] + i,
                                                  winningTeam);

                                command = new MySqlCommand(query, sql);

                                try {
                                    await command.ExecuteNonQueryAsync();
                                } catch (MySqlException ex) {
                                    Console.WriteLine(ex.ToString());
                                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                                }
                            }
                        }

                        if ((roundNumber >= 20) && (losingTeam != 0)) { // finals loser round 3rd and 4th place
                            for (int i = 0; i < 3; ++i) {
                                query = "INSERT INTO matches " +
                                    "(tournament_id, match_number, played, green_team) " +
                                    string.Format("VALUES ({0}, {1}, 0, {2});",
                                                  match.tournamentId,
                                                  ChampionshipBracketMap.Instance[roundNumber - 1] + i,
                                                  losingTeam);

                                command = new MySqlCommand(query, sql);

                                try {
                                    await command.ExecuteNonQueryAsync();
                                } catch (MySqlException ex) {
                                    Console.WriteLine(ex.ToString());
                                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                                }
                            }
                        }
                    }

                }
            }
        }
        */

        public async void GetGlobalData() {
            if ((sql != null) && (IsConnected)) {
                try {
                    var query = "SELECT * FROM tournaments " +
                        string.Format("WHERE YEAR(tournament_date)={0};", DateTime.Now.Year);
                    var command = new MySqlCommand(query, sql);
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) {
                        try {
                            var id = Convert.ToInt32(reader[0]);
                            var date = (DateTime)reader[1];
                            var name = (string)reader[2];

                            var t = new Tournament(id, date, name);
                            YBotSqlData.Global.tournaments.Add(t);
                        } catch (Exception ex) {
                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                        }
                    }
                    reader.Close();

                    query = "SELECT * FROM schools;";
                    command = new MySqlCommand(query, sql);
                    reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) {
                        try {
                            var id = Convert.ToInt32(reader[0]);
                            var name = (string)reader[1];

                            var s = new School(id, name);
                            YBotSqlData.Global.schools.Add(s);
                        } catch (Exception ex) {
                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                        }
                    }
                    reader.Close();
                } catch (MySqlException ex) {
                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                }
            }
        }

        public async Task<Match> GetMatch(int tournamentId, int matchNumber) {
            var match = new Match();

            if ((sql != null) && (IsConnected)) {
                try {
                    var query = "SELECT * FROM matches " +
                        string.Format("WHERE tournament_id={0} ", tournamentId) +
                        string.Format("AND match_number={0};", matchNumber);
                    var command = new MySqlCommand(query, sql);
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) {
                        try {
                            match.matchNumber = Convert.ToInt32(reader[2]);
                            match.greenTeam = Convert.ToInt32(reader[9]);
                            match.redTeam = Convert.ToInt32(reader[4]);
                        } catch (Exception ex) {
                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                        }
                    }
                    reader.Close();
                } catch (MySqlException ex) {
                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                }
            }

            return match;
        }
    }

    public enum SqlMessageType
    {
        Exception,
        General
    }

    public class SqlMessageArgs : EventArgs
    {
        public string message;
        public SqlMessageType type;

        public SqlMessageArgs(string message) {
            type = SqlMessageType.General;
            this.message = message;
        }

        public SqlMessageArgs(SqlMessageType type, string message) {
            this.type = type;
            this.message = message;
        }
    }
}
