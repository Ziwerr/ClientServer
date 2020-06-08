using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FormClient : Form
    {
        SimpleTCP.SimpleTcpClient client;
        public FormClient()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            client = new SimpleTCP.SimpleTcpClient().Connect(textIP.Text, Convert.ToInt32(textPort.Text));
            btnSend.Enabled = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string myMessage = client.WriteLineAndGetReply(textMessage.Text, TimeSpan.FromSeconds(3)).MessageString;
            myMessage = myMessage.Substring(0, myMessage.Length - 1);
            textChat.Text += myMessage + Environment.NewLine;
        }
    }
}
