using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace WorkingWithFile_UdpServer
{
    class Server
    {
        static void Main(string[] args)
        {
            var ipAddress = IPAddress.Parse("192.168.1.105"); // Your IP Address
            var port = 27001;

            using (var socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp))
            {

                var endPoint = new IPEndPoint(ipAddress, port);
                socket.Bind(endPoint);




                var length = 0;
                var buffer = new byte[66000];
                List<byte> byteList = new List<byte>();
                EndPoint endPointReceive = new IPEndPoint(IPAddress.Any, 0);




                try
                {

                    int temp = 0;

                    while (true)
                    {
                        Console.WriteLine("Waiting...");

                        length = socket.ReceiveFrom(buffer, ref endPointReceive);

                        Console.WriteLine($"length: {length}");


                        for (int i = 0; i < length; i++)
                        {
                            byteList.Add(buffer[i]);
                        }

                        temp++;
                        if (temp == 2) break;

                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }




                Console.WriteLine("list count: " + byteList.Count);

                // File writed
                FileStream fs = File.Create("uno.jpeg");
                fs.Write(byteList.ToArray());


                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                Console.WriteLine("End");


            }
        }
    }
}
