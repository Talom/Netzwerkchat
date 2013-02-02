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
           Console.WriteLine("Trololo");
           Console.ReadLine();


        }
    }
    class network
    {
        private TcpListener tcpListener;
        private UdpClient udpClient;
        private Thread listenThread;
        private Thread udpThread;
        IPEndPoint ep;

        public network(int port)
        {
            this.tcpListener = new TcpListener(IPAddress.Any, port);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            //listenThread.IsBackground = true;
            this.listenThread.Start();
            ep = new IPEndPoint(IPAddress.Any, port);
            this.udpClient = new UdpClient(ep);
            this.udpThread = new Thread(new ThreadStart(ListenforBroadcasts));
            //udpThread.IsBackground = true;
            udpThread.Start();
        }

        private void ListenforBroadcasts()
        {
            while (true)
            {
                Byte [] array = udpClient.Receive(ref ep);

                UnicodeEncoding encoder = new UnicodeEncoding();
                System.Console.WriteLine(encoder.GetString(array));

            }
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
            }

            tcpClient.Close();
        }
    }
}
