using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Spiegelserver
{
    class Program
    {
       
        static void Main(string[] args)
        {

           int x;
           x = Convert.ToInt32(Console.ReadLine());
           network nt = new network(x);

        }
    }
    class network
    {
        private TcpListener tcpListener;
        private UdpClient udpClient;
        private Thread listenThread;

        public network(int port)
        {
            this.tcpListener = new TcpListener(IPAddress.Any, port);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }


        private void ListenForClients()
        {
            System.Console.WriteLine("Ich hör jetzt zu");
            tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();

                //create a thread to handle communication
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                UnicodeEncoding encoder = new UnicodeEncoding();
                System.Console.WriteLine(encoder.GetString(message, 0, bytesRead));

                String text = encoder.GetString(message, 0, bytesRead);

                //Protokoll kommt zur Anwendung
                if (text == "Pipapo")
                {
                    System.Console.WriteLine("Schreibe Protokollantwort");
                    byte[] buffer = encoder.GetBytes("Hello Client!");
                    clientStream.Write(buffer, 0, buffer.Length);
                    clientStream.Flush();
                }

                else
                {
                    System.Console.WriteLine("unbekanntes Protokoll");
                }
            }

            tcpClient.Close();
        }
    }
}
