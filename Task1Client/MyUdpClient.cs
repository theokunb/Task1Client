using System;
using System.Net;
using System.Net.Sockets;

namespace Task1Client
{
    internal class MyUdpClient : IDisposable
    {
        public MyUdpClient(IPAddress ip,int port)
        {
            endPoint = new IPEndPoint(ip, port);
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        private readonly Socket udpSocket;
        private readonly EndPoint endPoint;

        public void Send(byte[] data,MyTcpClient myTcpClient)
        {
            udpSocket.SendTo(data, endPoint);
            if (myTcpClient.IsRecived("accepted"))
                return;
            else
                udpSocket.Send(data);
        }
        public void Dispose()
        {
            udpSocket.Close();
        }
    }
}
