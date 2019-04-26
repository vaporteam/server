using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Threading;

namespace server
{
    class Config
    {
        public static IPAddress Adress { get; set; } = IPAddress.Any;
        public static int Port { get; set; } = 8080;
    }

    class Status
    {
        public string Id { get; set; }
        public string Time { get; set; }
        public string Stat { get; set; }
    }

        class Program
    {
        static void Main(string[] args)
        {
            Program main = new Program();
            main.server_start();  //starting the server

            Console.ReadLine();
        }

        TcpListener server = new TcpListener(Config.Adress, Config.Port);

        private void server_start()
        {
            server.Start();
            accept_connection();  //accepts incoming connections
        }

        private void accept_connection()
        {
            server.BeginAcceptTcpClient(handle_connection, server);  //this is called asynchronously and will run in a different thread
        }

        private void handle_connection(IAsyncResult result)  //the parameter is a delegate, used to communicate between threads
        {
            accept_connection();  //once again, checking for any other incoming connections
            TcpClient client = server.EndAcceptTcpClient(result);  //creates the TcpClient

            NetworkStream ns = client.GetStream();

            /* here you can add the code to send/receive data */
            int i;
            String data = string.Empty;
            Byte[] bytes = new Byte[8192];
            while ((i = ns.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);
            }
            //byte[] msg = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\n\r\n");
            //ns.WriteAsync(msg, 0, msg.Length);
            //Console.WriteLine("Sent: {0}", msg);
            //client.Close();
        }
    }
}
