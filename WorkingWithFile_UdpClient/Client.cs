using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace WorkingWithFile_UdpClient
{
    class Client
    {
        static void Main(string[] args)
        {

            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName);

            string imagePath = startupPath + "/Images/uno.jpeg";

            var bytes = ConvertImageToByteArray(imagePath);



            var socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp);


            var ipAddress = IPAddress.Parse("192.168.1.105");
            var port = 27001;
            var endPoint = new IPEndPoint(ipAddress, port);



            // bytes.Length = 120769 byte
            // udp ile ~65kb gondermek olur max.
            socket.SendTo(bytes.Take(60000).ToArray(), endPoint);
            socket.SendTo(bytes.Skip(60000).Take(60769).ToArray(), endPoint);


            Console.WriteLine("Sent");


            // Release the socket.
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }


        public static byte[] ConvertImageToByteArray(string imagePath)
        {
            byte[] imageByteArray = null;
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                imageByteArray = new byte[reader.BaseStream.Length];
                for (int i = 0; i < reader.BaseStream.Length; i++)
                    imageByteArray[i] = reader.ReadByte();
            }

            return imageByteArray;
        }
    }
}
