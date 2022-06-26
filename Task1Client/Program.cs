using System.IO;
using System.Net;

namespace Task1Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 5)
                return;
            if (!IPAddress.TryParse(args[0], out IPAddress ip))
                return;
            if (!int.TryParse(args[1], out int port))
                return;
            if (!int.TryParse(args[2], out int udpPort))
                return;
            if (!File.Exists(args[3]))
                return;

            var bytes = File.ReadAllBytes(args[3]);
            if (bytes.Length > 10 * 1024 * 1024)
                return;

            if (!int.TryParse(args[4], out int timeout))
                return;
            Client client = new Client(ip, port, udpPort, args[3], timeout);
            client.Start();
        }
    }
}
