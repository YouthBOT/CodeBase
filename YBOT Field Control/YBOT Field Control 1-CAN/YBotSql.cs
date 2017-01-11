using System;
using System.Data;
using System.Threading;
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
                    Console.WriteLine ("SQL state: {0}", sql.State);
                    return !((sql.State == ConnectionState.Broken) ||
                        (sql.State == ConnectionState.Closed) ||
                        (sql.State == ConnectionState.Connecting));
                } else {
                    Console.WriteLine ("SQL state: null");
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

        public void AddLog (string text, string type) {
            if (sql != null) {
                var command = new MySqlCommand (
                    "INSERT INTO event_log (event_id, event_type, event_message)" +
                    string.Format ("VALUES (NOW(), '{0}', '{1}');", type, text),
                    sql);
                command.ExecuteNonQuery ();
            } else {
                Console.WriteLine ("SQL server connection is null");
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
