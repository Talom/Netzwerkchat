using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Netzwerk
{

    public class NetzwerkInterface
    {
       private TcpListener tcpListener;
       private UdpClient udpListener;
       private Thread listenThread;
       private Thread udpThread;
       private int port;

       public delegate void delMessageRecieved(Message msg);
       public delMessageRecieved OnMessageRecieved;

       public NetzwerkInterface()
       {
           OnMessageRecieved += delegate(Message msg) { };
           port = 1234;
           System.Diagnostics.Debug.WriteLine("Konstruktor");
           //this.tcpListener = new TcpListener(IPAddress.Any, port);
           this.tcpListener = new TcpListener(IPAddress.Any, 3001);
           this.listenThread = new Thread(new ThreadStart(ListenForClients));
           this.listenThread.IsBackground = true;
           this.listenThread.Start();

           //IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
           IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
           this.udpListener = new UdpClient(ep);
           this.udpThread = new Thread(new ThreadStart(ListenForBroadcast));
           this.udpThread.IsBackground = true;
           this.udpThread.Start();
       }

       //public static void OnStatusChanged(StatusChangedEventArgs e)
       //{
       //    StatusChangedEventHandler statusHandler = StatusChanged;
       //    if (statusHandler != null)
       //    {
       //        // Invoke the delegate
       //        statusHandler(null, e);
       //    }
       //}
       public void sendBroadcast(String text)
       { 
            IPEndPoint ep = new IPEndPoint(IPAddress.Broadcast, port);
            //UdpClient client = new UdpClient(ep);
            UnicodeEncoding encoder = new UnicodeEncoding();
            Byte[] msg = encoder.GetBytes(text);
            udpListener.Send(msg, msg.Length, ep);
            //client.Close();
       }
       public void sendMessage(String text, String ip)
       {
          // e = new StatusChangedEventArgs("User: " + text);
           // OnStatusChanged(e);
           TcpClient client = new TcpClient(ip, port);
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
           IPEndPoint ep = new IPEndPoint(IPAddress.Broadcast, port);
           while (true)
           {
              Byte [] message = this.udpListener.Receive(ref ep);
              UnicodeEncoding encoder = new UnicodeEncoding();
              System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, message.Length));
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
               Message msg = new Message(encoder.GetString(message, 0, bytesRead));
               OnMessageRecieved(msg);
               //String text = encoder.GetString(message, 0, bytesRead);
               break;
           }

           tcpClient.Close();
       }
    

    }
}
