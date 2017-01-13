using System;
using System.Net;
using System.Windows.Forms;
using YBotSqlWrapper;

namespace YBOT_Field_Control_2016
{
    public partial class SqlConnectWindow : Form
    {
        YbotSql sql;

        public SqlConnectWindow () {
            InitializeComponent ();

            ActiveControl = passwordTextBox;

            sql = YbotSql.Instance;
            if (sql.IsConnected) {
                connectionButton.Enabled = false;
            } else {
                passwordTextBox.KeyDown += KeyPressEvent;
                serverIpTextBox.KeyDown += KeyPressEvent;
            }
        }

        private void KeyPressEvent (object sender, KeyEventArgs args) {
            if (args.KeyData == Keys.Enter) {
                connectionButton.PerformClick ();
            }
        }

        private void connectionButton_Click (object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace (serverIpTextBox.Text)) {
                MessageBox.Show ("Please enter a IP address or host name");
                return;
            } 

            if (string.IsNullOrWhiteSpace (passwordTextBox.Text)) {
                MessageBox.Show ("Please enter a password");
                return;
            }

            messageTextbox.Text = string.Empty;

            sql.SqlMessageEvent += OnSqlMessageEvent;
            sql.SqlConnectedEvent += OnSqlConnectedEvent;

            sql.Connect (serverIpTextBox.Text, passwordTextBox.Text, sshCheckBox.Checked);
        }

        protected void OnSqlMessageEvent (object sender, SqlMessageArgs args) {
            if (messageTextbox.InvokeRequired) {
                messageTextbox.Invoke ((MethodInvoker)delegate () {
                    messageTextbox.Text = messageTextbox.Text + args.message;
                });
            } else {
                messageTextbox.Text = messageTextbox.Text + args.message;
            }
        }

        protected void OnSqlConnectedEvent (object sender) {
            sql.SqlMessageEvent -= OnSqlMessageEvent;
            sql.SqlConnectedEvent -= OnSqlConnectedEvent;

            if (InvokeRequired) {
                Invoke ((MethodInvoker)delegate () {
                    Close ();
                });
            } else {
                Close ();
            }

            sql.GetGlobalData ();
        }
    }
}
