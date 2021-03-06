﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Threading;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using config;
using Common;
using System.Reflection;
using core_server.Properties;

namespace server
{
    class Program 
    {
        public static void Main()
        {
            string adress = Config.Server.Adress;
            if (adress == "0.0.0.0")
            {
                adress = "*";
            }
            string s = string.Format("http://{0}:{1}/", adress, Config.Server.Port);

            SimpleListener(s);

            Console.WriteLine("Press Any Key To Exit...");
            Console.ReadKey(false);
        }

        // This example requires the System and System.Net namespaces.
        public static void SimpleListener(string prefix)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefix == null || prefix == string.Empty)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            listener.Prefixes.Add(prefix);

            listener.Start();
            Console.WriteLine("Listening...");
            
            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                ProcessRequest(context);
            }
            //listener.Stop();
        }

        private static void ProcessRequest(HttpListenerContext context)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                // Obtain a request object.
                HttpListenerRequest request = context.Request;
                Console.WriteLine(request.Headers);
                string body = GetRequestPostData(request);
                Console.WriteLine(body);
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                byte[] buffer;
                if (request.UserAgent == "Test_Tracker")
                {
                    string responseString = "Ok";
                    buffer = Encoding.UTF8.GetBytes(responseString);
                } else
                {
                    buffer = Encoding.UTF8.GetBytes(Resources.index);
                }
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            });
        }
        public static string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (Stream body = request.InputStream) // here we have data
            {
                using (StreamReader reader = new StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
