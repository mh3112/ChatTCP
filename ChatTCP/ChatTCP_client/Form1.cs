using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatTCP_client
{
    public partial class Form1 : Form
    {
        ChatServer server;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.server = new ChatServer();
            server.Start();

            this.serverState.Text = "Sever's state: Start";                        
        }

         

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.server.Stop();
            this.serverState.Text = "Sever's state: Stop";
        }

        private void listLog_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string serverState = "";
            server.AcceptConnection(out serverState);
            server.CreatThreadPerOneClient();

            //Add client 
            listLog.Items.Add(serverState);
        }  
    }
}
