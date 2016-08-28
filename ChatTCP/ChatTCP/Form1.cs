using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatTCP
{
    public partial class Form1 : Form
    {
        private Thread _receiveThread;
        private ChatClient _chatClient;
        private string _message;
        public Form1()
        {
            InitializeComponent();
        }
        /*
         *  try
            {
                this._receiveThread.Join();
            }
            catch (Exception e) 
            {
                throw (e);
            }
         * */
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this._chatClient = new ChatClient();
            bool connected = this._chatClient.Connect();
            if(connected)
            {
                this.CreateReceiveThread();    
            }
        }

        public void CreateReceiveThread()
        {
            this._receiveThread = new Thread(Excute);
            this._receiveThread.Start();
        }

        private void Excute()
        {
            while (true)
            {
                this._chatClient.Receive(out _message);
                Thread.Sleep(1000); 
                //this.listMessage.Items.Add(_message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtSend.Text;
            listMessage.Items.Add(message);
            _chatClient.Send(message);

        }
    }
}
