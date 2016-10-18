using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.Windows.Forms;

namespace YBOT_Field_Control_2016
{
    public partial class CANBUS
    {
        public int commandNode = 31;
        public SerialPort canPort = new SerialPort();       //Default Com port
        public volatile bool newData;    //New Data Flag
        LogWriter lw = new LogWriter();
        CommonVariables cv = new CommonVariables();
        public CANRawData canRaw;
        private int baud = 115200;
        public bool rts = false;
        public bool dtr = false;
        public int messagesReceived = 0;
        public int messagesSent = 0;
        public StringBuilder logBuilder = new StringBuilder();
        public bool getCanData = false;
        Thread readPort;

        public delegate void CanReceivedEvent(object sender, CanMessageEventArgs e);
        public event CanReceivedEvent CanMessageReceived;
        private CanMessageEventArgs newCanMessage = new CanMessageEventArgs();

        protected virtual void OnCanReceivedEvent(CanMessageEventArgs e)
        {
            if (CanMessageReceived != null) CanMessageReceived(this, e);
        }
      
    }

    public class CanMessageEventArgs : EventArgs
    {
        public string canMessage { get; set; }
    }
}
