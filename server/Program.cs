using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Threading;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace server
{
    class Config
    {
        public static IPAddress Adress { get; set; } = IPAddress.Any;
        public static int Port { get; set; } = 8080;
        public static int Wait { get; set; } = 300;
    }

    class Status
    {
        public string Id { get; set; }
        public string Time { get; set; }
        public string Stat { get; set; }
    }

    class Program
    {
        /*
        public static void Main()
        {

        }
        */
    }

    class Server
    {
        public static void Main()
        {
            TcpListener server = new TcpListener(Config.Adress, Config.Port);

            server.Start();
            Console.WriteLine("El server se ha iniciado en {0}.{1}Esperando una conexión...", Config.Adress, Environment.NewLine);


            while (true)
            {
                TcpClient Client = server.AcceptTcpClient();
                try{
                    StartThread(Client);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                }
                finally
                {
                    // Stop listening for new clients.
                    server.Stop();
                    Console.WriteLine("\nHit enter to continue...");
                    Console.Read();
                }
            }

        }

        static void StartThread(TcpClient Cli)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
            TcpClient Client = Cli as TcpClient;

                Console.WriteLine("Un cliente conectado.");

                // Get a stream object for reading and writing
                NetworkStream stream = Client.GetStream();

                Console.WriteLine("Connected!");

                Byte[] bytes = new Byte[8192];
                String data = null;

                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    //stream.Write(msg, 0, msg.Length);
                    //Console.WriteLine("Sent: {0}", data);

                }

                response(Encoding.UTF8.GetBytes("Hola Existencia"), stream);

                stream.Close();

                // Shutdown and end connection
                Client.Close();

     
            });
        }

        private static void response(byte[] data, NetworkStream stream)
        {
            Byte[] response = Encoding.UTF8.GetBytes(string.Format("HTTP/1.1 200 OK\r\n\r\n{0}", data));
            stream.Write(response, 0, response.Length);

        }
    }
}
