using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipAddress = IPAddress.Parse("192.168.1.105"); // Your IP Address
            var port = 27001;

            var socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            var endPoint = new IPEndPoint(ipAddress, port);

            try
            {
                socket.Connect(endPoint);

                if (socket.Connected)
                {
                    Console.WriteLine("Connected to the server..");

                    while (true)
                    {
                        var msg = Console.ReadLine();
                        var bytes = Encoding.UTF8.GetBytes(msg);

                        socket.Send(bytes);
                    }

                }
                else
                {
                    Console.WriteLine("Can not connect to the server..");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not connect to the server..");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
