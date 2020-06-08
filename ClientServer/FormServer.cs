using System;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class FormServer : Form
    {
        SimpleTCP.SimpleTcpServer server;
        public FormServer()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTCP.SimpleTcpServer();
            server.Delimiter = 0x13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataRecieved;
        }

        private void Server_DataRecieved(object sender, SimpleTCP.Message e)
        {
            textChat.Invoke((MethodInvoker)delegate ()
            {
                string myMessage = e.MessageString.Substring(0, e.MessageString.Length - 1);
                textChat.Text += myMessage + Environment.NewLine;
                e.ReplyLine(string.Format("You said: " + myMessage));
            });

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            textChat.Text += "Server Starting..." + Environment.NewLine;
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(textIP.Text);
            server.Start(ip, Convert.ToInt32(textPort.Text));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
            {
                server.Stop();
                textChat.Text += "Server stopped.";
            }
        }
    }
}