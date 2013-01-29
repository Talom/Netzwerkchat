using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Netzwerk
{
    public class StatusChangedEventArgs : EventArgs
    {
        // The argument we're interested in is a message describing the event
        private string EventMsg;

        // Property for retrieving and setting the event message
        public string EventMessage
        {
            get
            {
                return EventMsg;
            }
            set
            {
                EventMsg = value;
            }
        }

        // Constructor for setting the event message
        public StatusChangedEventArgs(string strEventMsg)
        {
            EventMsg = strEventMsg;
        }
    }
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);


    public class NetzwerkInterface
    {
       private TcpListener tcpListener;
       private UdpClient udpListener;
       private Thread listenThread;
       private Thread udpThread;
       public static event StatusChangedEventHandler StatusChanged;
       private static StatusChangedEventArgs e;



       public NetzwerkInterface()
       {
           System.Diagnostics.Debug.WriteLine("Konstruktor");
           this.tcpListener = new TcpListener(IPAddress.Any, 3000);
           this.listenThread = new Thread(new ThreadStart(ListenForClients));
           this.listenThread.Start();

           IPEndPoint ep = new IPEndPoint(IPAddress.Any, 3000);
           this.udpListener = new UdpClient(ep);
           this.udpThread = new Thread(new ThreadStart(ListenForBroadcast));
           this.udpThread.Start();
       }

       public static void OnStatusChanged(StatusChangedEventArgs e)
       {
           StatusChangedEventHandler statusHandler = StatusChanged;
           if (statusHandler != null)
           {
               // Invoke the delegate
               statusHandler(null, e);
           }
       }
       public void sendBroadcast(String text)
       { 
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 3000);
            UdpClient client = new UdpClient(ep);
            UnicodeEncoding encoder = new UnicodeEncoding();
            Byte[] msg = encoder.GetBytes(text);
            client.Send(msg, msg.Length);
       }
       public void sendMessage(String text, String ip)
       {
           e = new StatusChangedEventArgs("User: " + text);
           OnStatusChanged(e);
           TcpClient client = new TcpClient(ip, 3000);
           NetworkStream stream = client.GetStream();

           byte[] message = new byte[4096];
          
              UnicodeEncoding encoder = new UnicodeEncoding();
              message = encoder.GetBytes(text);
           stream.Write(message, 0, message.Length);
           stream.Flush();
           client.Close();
       }

       private void ListenForBroadcast()
       {
           IPEndPoint ep = new IPEndPoint(IPAddress.Any, 3000);
           while (true)
           {
              Byte [] message = this.udpListener.Receive(ref ep);
           }

       }

       private void ListenForClients()
       {
           System.Diagnostics.Debug.WriteLine("Ich hör jetzt zu");
           this.tcpListener.Start();

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
               System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));

               String text = encoder.GetString(message, 0, bytesRead);

               //Protokoll kommt zur Anwendung
                   //if (text == "Pipapo")
                   //{
                   //    System.Diagnostics.Debug.WriteLine("Schreibe Protokollantwort");
                   //    byte[] buffer = encoder.GetBytes("Hello Client!");
                   //    clientStream.Write(buffer, 0, buffer.Length);
                   //    clientStream.Flush();
                   //}

                   //else
                   //{
                   //    System.Diagnostics.Debug.WriteLine("unbekanntes Protokoll");
                   //}
           }

           tcpClient.Close();
       }
    

    }
}
