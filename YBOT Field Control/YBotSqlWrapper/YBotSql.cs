using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Renci.SshNet;

namespace YBotSqlWrapper
{
    public delegate void SqlMessageHandler (object sender, SqlMessageArgs args);
    public delegate void SqlStatusHandler (object sender);

    public class YbotSql
    {
        protected static YbotSql _instance = new YbotSql ();
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

        protected YbotSql () {
            
        }

        public event SqlMessageHandler SqlMessageEvent;
        public event SqlStatusHandler SqlConnectedEvent;

        public void Connect (string serverIp, string password, bool useSsh = true) {
            Thread connectThread = new Thread (() => {
                if (useSsh) {
                    ConnectSsh (serverIp, password);
                    serverIp = "127.0.0.1";
                }
                ConnectMySql (serverIp, password);
            });

            connectThread.Start ();
        }

        protected void ConnectSsh (string server, string password) {
            if (ssh == null) { // if the ssh object is not null, then a tunnel has already been created
                ssh = new SshClient (server, 22, "youthbot", password);
                SqlMessageEvent?.Invoke (this, new SqlMessageArgs ("Connecting to SSH server..."));

                try {
                    ssh.Connect ();
                } catch (Exception ex) {
                    string text = "failure\n" + ex.ToString ();
                    SqlMessageEvent?.Invoke (this, new SqlMessageArgs (text));
                    return;
                }

                if (ssh.IsConnected) {
                    SqlMessageEvent?.Invoke (this, new SqlMessageArgs ("connected\n"));

                    var tunnel = new ForwardedPortLocal ("127.0.0.1", 3306, "127.0.0.1", 3306);
                    ssh.AddForwardedPort (tunnel);
                    tunnel.Start ();
                }
            }
        }

        protected void ConnectMySql (string server, string password) {
            var sb = new MySqlConnectionStringBuilder ();
            sb.Server = server;
            sb.Port = 3306;
            sb.Database = "ybot";
            sb.UserID = "youthbot";
            sb.Password = password;

            sql = new MySqlConnection (sb.ConnectionString);

            try {
                SqlMessageEvent?.Invoke (this, new SqlMessageArgs ("Connecting to MySQL server..."));
                sql.Open ();
                SqlMessageEvent?.Invoke (this, new SqlMessageArgs ("connected\n"));
                SqlConnectedEvent?.Invoke (this);

                return;
            } catch (Exception ex) {
                if (ssh != null) {
                    ssh.Disconnect ();
                    ssh.Dispose ();
                }

                string text = "failure\n" + ex.ToString ();
                SqlMessageEvent?.Invoke (this, new SqlMessageArgs (text));
                return;
            }
        }

        public void Disconnect () {
            sql.Close ();
            sql.Dispose ();

            if (ssh != null) {
                ssh.Disconnect ();
                ssh.Dispose ();
            }
        }

        public async void AddLog (string text, string type) {
            if ((sql != null) && (IsConnected)) {
                var command = new MySqlCommand (
                    "INSERT INTO event_log (event_id, event_type, event_message) " +
                    string.Format ("VALUES (NOW(), '{0}', '{1}');", type, text),
                    sql);

                try {
                    await command.ExecuteNonQueryAsync();
                } catch (MySqlException) {
                    //
                }
            }
        }

        public async void AddMatch (Match match) {
            if ((sql != null) && (IsConnected)) {
                var query = "SELECT match_id FROM matches " +
                    string.Format ("WHERE tournament_id={0} and match_number={1};", match.tournamentId, match.matchNumber);

                var command = new MySqlCommand (query, sql);
                var reader = await command.ExecuteReaderAsync ();

                if (reader.HasRows) {
                    await reader.ReadAsync ();
                    var id = Convert.ToInt32 (reader[0]);
                    reader.Close ();

                    query = "UPDATE matches " +
                        "SET played = 1, " +
                        string.Format ("red_team = {0}, red_score = {1}, red_penalty = {2}, red_dq = {3}, red_result = '{4}', ",
                            match.redTeam, match.redScore, match.redPenalty, match.redDq, match.redResult) +
                        string.Format ("green_team = {0}, green_score = {1}, green_penalty = {2}, green_dq = {3}, green_result = '{4}', ",
                            match.greenTeam, match.greenScore, match.greenPenalty, match.greenDq, match.greenResult) +
                        string.Format ("auto_corners_tested = {0}, auto_emergency_cycled = {1}, auto_solar_panel = {2}, ",
                            match.autoCornersTested, match.autoEmergencyCycled, match.autoSolarPanel) +
                        string.Format ("manual_solar_panel_1 = {0}, manual_solar_panel_2 = {1}, manual_emergency_cleared = {2}, ",
                            match.manSolarPanel1, match.manSolarPanel2, match.manualEmergencyCleared) +
                        string.Format ("rocket_position = {0}, rock_weight = {1}, rock_score = {2}, rocket_bonus = {3} ",
                            match.rocketPosition, match.rockWeight, match.rockScore, match.rocketBonus) +
                        string.Format ("WHERE match_id = {0};", id);

                    command = new MySqlCommand (query, sql);
                    await command.ExecuteNonQueryAsync ();
                } else {
                    reader.Close ();

                    query = "INSERT INTO matches " +
                        "(tournament_id, match_number, played, " +
                        "red_team, red_score, red_penalty, red_dq, red_result, " +
                        "green_team, green_score, green_penalty, green_dq, green_result, " +
                        "auto_corners_tested, auto_emergency_cycled, auto_solar_panel, " +
                        "manual_solar_panel_1, manual_solar_panel_2, manual_emergency_cleared, " +
                        "rocket_position, rock_weight, rock_score, rocket_bonus) " +
                        string.Format ("VALUES ({0}, {1}, 1, ", match.tournamentId, match.matchNumber) +
                        string.Format ("{0}, {1}, {2}, {3}, '{4}', ", match.redTeam, match.redScore, match.redPenalty, match.redDq, match.redResult) +
                        string.Format ("{0}, {1}, {2}, {3}, '{4}', ", match.greenTeam, match.greenScore, match.greenPenalty, match.greenDq, match.greenResult) +
                        string.Format ("{0}, {1}, {2}, ", match.autoCornersTested, match.autoEmergencyCycled, match.autoSolarPanel) +
                        string.Format ("{0}, {1}, {2}, ", match.manSolarPanel1, match.manSolarPanel2, match.manualEmergencyCleared) +
                        string.Format ("{0}, {1}, {2}, {3});", match.rocketPosition, match.rockWeight, match.rockScore, match.rocketBonus);

                    command = new MySqlCommand (query, sql);
                    await command.ExecuteNonQueryAsync ();
                }
            }
        }

        public async void GetGlobalData () {
            if ((sql != null) && (IsConnected)) {
                try {
                    var query = "SELECT * FROM tournaments " +
                        string.Format ("WHERE YEAR(tournament_date)={0};", DateTime.Now.Year);
                    var command = new MySqlCommand (query,sql);
                    var reader = await command.ExecuteReaderAsync ();
                    while (await reader.ReadAsync ()) {
                        try {
                            var id = Convert.ToInt32 (reader[0]);
                            var date = (DateTime)reader[1];
                            var name = (string)reader[2];

                            var t = new Tournament (id, date, name);
                            YBotSqlData.Global.tournaments.Add (t);
                        } catch (Exception ex) {
                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(ex.ToString()));
                        }
                    }
                    reader.Close ();

                    query = "SELECT * FROM schools;";
                    command = new MySqlCommand (query, sql);
                    reader = await command.ExecuteReaderAsync ();
                    while (await reader.ReadAsync ()) {
                        try {
                            var id = Convert.ToInt32 (reader[0]);
                            var name = (string)reader[1];

                            var s = new School (id, name);
                            YBotSqlData.Global.schools.Add (s);
                        } catch (Exception ex) {
                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(ex.ToString()));
                        }
                    }
                    reader.Close ();
                } catch (MySqlException ex) {
                    SqlMessageEvent?.Invoke (this, new SqlMessageArgs (ex.ToString ()));
                }
            }
        }
    }

    public class SqlMessageArgs : EventArgs
    {
        public string message;

        public SqlMessageArgs (string message) {
            this.message = message;
        }
    }
}
