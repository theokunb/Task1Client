using System.IO;
using System.Net;
using System.Text;

namespace Task1Client
{
    public class Client
    {
        public Client(IPAddress ip,int tcpPort, int udpPort,string filepath,int timeout)
        {
            this.ip = ip;
            this.tcpPort = tcpPort;
            this.filepath = filepath;
            this.udpPort = udpPort;
            this.timeout = timeout;
        }
        private readonly IPAddress ip;
        private readonly int tcpPort;
        private readonly int udpPort;
        private readonly string filepath;
        private readonly int timeout;


        public void Start()
        {

            using (MyTcpClient tcpClient = new MyTcpClient(ip, tcpPort, timeout))
            {
                if (!tcpClient.IsConnected)
                    return;
                tcpClient.Send(Encoding.UTF8.GetBytes(Path.GetFileName(filepath)));
                tcpClient.IsRecived("yeah i got file name");
                tcpClient.Send(Encoding.UTF8.GetBytes(udpPort.ToString()));
                tcpClient.IsRecived("yeah i got udp port");

                using (MyUdpClient udpClient = new MyUdpClient(ip, udpPort))
                {
                    using (var binaryReader = new BinaryReader(File.OpenRead(filepath)))
                    {
                        byte[] sendBuffer;
                        do
                        {
                            sendBuffer = binaryReader.ReadBytes(256);
                            udpClient.Send(sendBuffer, tcpClient);
                        }
                        while (sendBuffer.Length > 0);
                    }
                    udpClient.Send(Encoding.UTF8.GetBytes("all done"), tcpClient);
                }
            }
        }
    }
}
