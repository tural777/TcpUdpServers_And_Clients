using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            var ipAddress = IPAddress.Loopback;
            var port = 27001;

            EndPoint endPoint = new IPEndPoint(ipAddress, port);

            try
            {
                while (true)
                {
                    var text = Console.ReadLine();
                    var bytes = Encoding.UTF8.GetBytes(text);

                    socket.SendTo(bytes, endPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
