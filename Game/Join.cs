using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Game
{
    class Join
    {
        TcpClient client;

        public Join()
        {
            client = new TcpClient();
        }
        public void Connect(String server, String message)
        {
            try
            {


                Int32 port = 13000;
                client = new TcpClient(server, port);
                while (true)
                {
                    Recieve();
                    //Send();
                   

                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }

        public void Send(string action)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(action);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
        }

        public void Recieve()
        {
            Byte[] data = new Byte[256];
            NetworkStream stream = client.GetStream();
            String responseData = String.Empty;
            Int32 bytes = stream.Read(data, 0, data.Length);
        }





    }
}

   