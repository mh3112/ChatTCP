using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatTCP
{
    class ChatClient
    {
        private string _serverAdress;
        private string _serverPort;
        private TcpClient _client;
        private byte[] _dataSend;
        private byte[] _dataReceive;
        

        public ChatClient()
        {
            this._serverAdress = "127.0.0.1";
            this._serverPort = "9090";  
        }

        public ChatClient(string serverAddress, string serverPort)
        {
            this._serverAdress = serverAddress;
            this._serverPort = serverPort;
        }

        public bool Connect()
        {
            bool connected = false;
            this._client = new TcpClient();
            try
            {
                _client.Connect(IPAddress.Parse(this._serverAdress), Int16.Parse(this._serverPort));
                if (this._client.Connected) {
                    connected = true;
                }
            }
            catch (Exception e) {
                throw (e);
            }
            return connected;
        }

        public void Disconnect()
        {
           
            this._client.Close();
        }

        public void Send(string message)
        {
            try
            {
                _dataSend = new byte[1024];
                this._dataSend = Encoding.Unicode.GetBytes(message);
                this._client.GetStream().Write(this._dataSend, 0, this._dataSend.Length);
            }
            catch (Exception e)
            {
                throw (e);
            }
            
        }

        public void Receive(out string message)
        {
            message = "";
            try
            {
                _dataReceive = new byte[1024];
                this._client.GetStream().Read(_dataReceive, 0, _dataReceive.Length);
                message = Encoding.Unicode.GetString(this._dataReceive).Replace("\0", "");
            }
            catch (Exception e)
            {
                this._client.Close();
                throw (e);
            }            
        }
    }
}
