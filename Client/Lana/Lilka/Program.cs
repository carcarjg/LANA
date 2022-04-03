using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;

namespace Lilka
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Loopback, 1234);
            TcpListener listener = new TcpListener(ep);
            listener.Start();

            Console.WriteLine(@"  
            ===================================================  
                   Started listening requests at: {0}:{1}  
            ===================================================",
            ep.Address, ep.Port);

            // Run the loop continuously; this is the server.
            while (true)
            {
                const int bytesize = 1024 * 1024;

                string message = null;
                byte[] buffer = new byte[bytesize];

                var sender = listener.AcceptTcpClient();
                sender.GetStream().Read(buffer, 0, bytesize);

                // Read the message and perform different actions
                message = cleanMessage(buffer);

                // Save the data sent by the client;
                callissue callissue = JsonConvert.DeserializeObject<callissue>(message); // Deserialize

                byte[] bytes = System.Text.Encoding.Unicode.GetBytes("ACK");
                sender.GetStream().Write(bytes, 0, bytes.Length); // Send the response

            }
        }
        private static string cleanMessage(byte[] bytes)
        {
            string message = System.Text.Encoding.Unicode.GetString(bytes);

            string messageToPrint = null;
            foreach (var nullChar in message)
            {
                if (nullChar != '\0')
                {
                    messageToPrint += nullChar;
                }
            }
            return messageToPrint;
        }

        // Sends the message string using the bytes provided.
        private static void sendMessage(byte[] bytes, TcpClient client)
        {
            client.GetStream()
                .Write(bytes, 0,
                bytes.Length); // Send the stream
        }

        //Responce of 0x1379 is good, 0x1111 is for Incorect UID
        private static byte[] sendMessage(byte[] messageBytes,string IP, int port)
        {
            const int bytesize = 1024 * 1024;
            try // Try connecting and send the message bytes
            {
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(IP, port); // Create a new connection
                NetworkStream stream = client.GetStream();

                stream.Write(messageBytes, 0, messageBytes.Length); // Write the bytes
                Console.WriteLine("Connected to the server");
                Console.WriteLine(messageBytes);
                Console.WriteLine("Waiting for response...");

                messageBytes = new byte[bytesize]; // Clear the message

                // Receive the stream of bytes
                stream.Read(messageBytes, 0, messageBytes.Length);

                // Clean up
                stream.Dispose();
                client.Close();
            }
            catch (Exception e) // Catch exceptions
            {
                Console.WriteLine(e.Message);
            }

            return messageBytes; // Return response
        }
    }

    #region JSON Templates
    class callissue
    {
        public string UID { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        //public string
    }
    class statusupdate
    {
        public string UID { get; set; }
        public string code { get; set; }
    }
    #endregion
}
