using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Task1Client
{
    internal class MyTcpClient : IDisposable
    {
        public MyTcpClient(IPAddress ip,int port,int timeout)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(ip, port);
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.ReceiveTimeout = timeout;
            try
            {
                tcpSocket.Connect(iPEndPoint);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private readonly Socket tcpSocket;

        public bool IsConnected => tcpSocket.Connected;



        public void Send(byte[] data)
        {
            tcpSocket.Send(data);
        }

        public bool IsRecived(string keyword)
        {
            byte[] buffer = new byte[128];
            int size = 0;
            try
            {
                size = tcpSocket.Receive(buffer);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var response = Encoding.UTF8.GetString(buffer, 0, size);
            return response == keyword;
        }

        public void Dispose()
        {
            tcpSocket.Close();
        }




    }
}
