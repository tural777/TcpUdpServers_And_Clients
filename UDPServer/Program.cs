using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipAddress = IPAddress.Any;
            var port = 27001;

            var socket = new Socket(AddressFamily.InterNetwork,
                                    SocketType.Dgram,
                                    ProtocolType.Udp);

            var ipEndPoint = new IPEndPoint(ipAddress, port);

            socket.Bind(ipEndPoint);

            while (true)
            {
                var bytes = new byte[socket.ReceiveBufferSize];
                EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0); // butun ip adresler ve portlar gelen
                var lenght = socket.ReceiveFrom(bytes, ref endPoint);

                var msg = Encoding.UTF8.GetString(bytes, 0, lenght);
                Console.WriteLine(msg);
            }


        }
    }
}
