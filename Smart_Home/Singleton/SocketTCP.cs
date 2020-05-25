using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Home.Singleton
{
    public class SocketTCP
    {
        private static readonly Lazy<SocketTCP> socket = new Lazy<SocketTCP>(() => new SocketTCP());
        public static NetworkStream stream;
        public static TcpClient client;
        public static SocketTCP GetInstance()
        {
            client = new TcpClient();


            return socket.Value;
        }

        public static NetworkStream GetStream()
        {
            stream = client.GetStream();
            return client.GetStream();
        }


    }
}
