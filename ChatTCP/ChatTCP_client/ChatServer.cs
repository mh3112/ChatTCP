using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatTCP_client
{
    public class ChatServer
    {
        private TcpListener _listener;
        private Socket _socket;
        private List<Socket> _listClientEntities;

        public ChatServer()
        {
            try
            {
                this._listener = new TcpListener(IPAddress.Parse("127.0.0.1"), Int16.Parse("9090"));
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public ChatServer(string serverAddress, string serverPort)
        {
            try
            {
                this._listener = new TcpListener(IPAddress.Parse(serverAddress), Int16.Parse(serverPort));
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        /// <summary>
        /// Start server
        /// Step 1: TcpListener.Start()
        /// Step 2: Init ClientEntities
        /// </summary>
        public void Start()
        {
            try
            {
                this._listener.Start();
            }
            catch (Exception e)
            {
                throw (e);
            }

            this._listClientEntities = new List<Socket>();
        }

        public void AcceptConnection(out string status)
        {
            this._socket = this._listener.AcceptSocket();
            this._listClientEntities.Add(this._socket);

            status = "Client " + this._socket.RemoteEndPoint;
        }

        public void CreatThreadPerOneClient()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Excute), this._socket);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(BroadCast), this._socket);
        }

        private void Excute(Object socket) 
        {
            string message = "";
            while (true)
            {
                this.Receive((Socket)socket, out message);
                foreach (Socket _socket in this._listClientEntities)
                {
                    this.Send(_socket, "111111111111");
                }
            }
        }

        //private void Chat(object socket)
        //{
        //    string message = "";
        //    while (message.Replace("\0", "") != "exit")
        //    {
        //        this.Receive((Socket)socket, out message);
        //        this.Send((Socket)socket, message);
        //    }
        //    this._socket.Close();
        //}

        //private void BroadCast(object socket)
        //{
        //    // Hiện tại tham số object socket chỉ để cho đẹp các em chưa cần quan tâm đến nó.
        //    // Yêu cầu của anh là viết nội dung cho hàm BroadCast này.
        //    // BroadCast là quảng bá. Nghĩa là có n thằng client kết nối đến server.
        //    // Một thằng nào đó nhắn "tên của nó" thì tất cả mọi thằng(bao gồm chính nó) sẽ được server trả về
        //    // cùng 1 nội dung "Hello + tên thằng gửi".

        //    //Sau khi viết xong, các em vào hàm Wait, comment câu lệnh có gọi hàm Chat
        //    // và bỏ comment câu lệnh có hàm BroadCast cho anh.
        //    // Bật nhiều client để kiểm tra xem code của các em có hoạt động hay không? OK?

        //}

        public void Send(Socket socket, string message)
        {
            socket.Send(Encoding.Unicode.GetBytes(message));
        }

        public void Receive(Socket socket, out string message)
        {
            message = "";
            try
            {
                byte[] data = new byte[1024];
                socket.Receive(data);
                message = Encoding.Unicode.GetString(data).Replace("\0", "");
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void Stop()
        {
            this._listener.Stop();
        }
    }
}


