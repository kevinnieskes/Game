using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApplication3
{
    class Program
    {
        public void connect()
        {
            TcpListener server = null;
            try
            {
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Any;
                server = new TcpListener(localAddr, port);
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;
                while (true)
                {
                    Console.Write("Server Online. \n");
                    Console.Write("Waiting for a connection... \n");
                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!\n");
                    data = null;
                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();
                    int i;
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}\n", data);

                        // Process the data sent by the client.
                        Console.WriteLine("Enter Message: \n");
                        data = Console.ReadLine();


                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}\n", data);
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        public void Send(string action)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(action);
            // NetworkStream stream = client.GetStream();
            // stream.Write(data, 0, data.Length);
        }

        public void Recieve()
        {
            Byte[] data = new Byte[256];
            //NetworkStream stream = client.GetStream();
            String responseData = String.Empty;
            // Int32 bytes = stream.Read(data, 0, data.Length);
        }

    }
}

