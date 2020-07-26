using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipAddress = IPAddress.Parse("192.168.1.105"); // Your IP Address
            var port = 27001;

            using (var socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp))
            {

                var endPoint = new IPEndPoint(ipAddress, port);

                socket.Bind(endPoint);

                var backlog = 10; // queue
                socket.Listen(backlog);

                while (true)
                {

                    Console.WriteLine($"Listening on {socket.LocalEndPoint}");
                    var client = socket.Accept(); // Eger qoshulan yoxdusa novbeti setire kechmir.

                    Task.Run(() =>
                    {

                        Console.WriteLine($"{client.RemoteEndPoint} connected...");

                        var length = 0;
                        var bytes = new byte[1024]; // 1024 bayt-bayt oxumasi uchun.

                        do
                        {

                            length = client.Receive(bytes);
                            var msg = Encoding.UTF8.GetString(bytes, 0, length);

                            Console.WriteLine($"{client.RemoteEndPoint} : {msg}");

                            if (msg == "ok")
                            {
                                client.Shutdown(SocketShutdown.Both);
                                client.Dispose();
                                break;
                            }

                        } while (true);


                    });

                }

            }

        }
    }
}
