using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netzwerk
{
    public partial class Chatview : Form
    {
        NetzwerkInterface connection;
        Dictionary<String, String> kontaktliste; 
        public Chatview()
        {
            InitializeComponent();
            kontaktliste = new Dictionary<string,string>();
            connection = new NetzwerkInterface();
            connection.OnBroadcastRecieved += OnBroadcastRecieved;
            connection.OnMessageRecieved += OnMessage;
            
            
        }

        private void OnBroadcastRecieved(Message msg, String ip)
        {
            ip = ip.Split(':')[0];
            if (!kontaktliste.ContainsKey(ip))
            {
                kontaktliste.Add(ip, msg.getUser());
                // kontaktlistbx.Items.AddRange(kontaktliste.Values.ToArray());
                if (kontaktlistbx.InvokeRequired)
                {
                    kontaktlistbx.Invoke((MethodInvoker)delegate
                    {
                        kontaktlistbx.Items.AddRange(kontaktliste.Values.ToArray());
                    }
                    );
                }
                else 
                {
                    kontaktlistbx.Items.AddRange(kontaktliste.Values.ToArray());
                }
            }
        }

        private void OnMessage(Message msg)
        {
            if (chat_txtbx.InvokeRequired)
            {
                chat_txtbx.Invoke((MethodInvoker)delegate
                    {
                        chat_txtbx.Text += "\r\n" + msg.getZeit() + ": " + msg.getUser() + ": ";
                        chat_txtbx.Text += msg.getBody();
                    });
            }
            else 
            {
                chat_txtbx.Text += "\r\n" + msg.getZeit() + ": " + msg.getUser() + ": ";
                chat_txtbx.Text += msg.getBody();
            }
        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            int index =  kontaktlistbx.SelectedIndex;
            Message nachricht = new Message("MSG", "", "Björn", "ONL", this.textBox1.Text);
            String ip = kontaktliste.Keys.ToArray().GetValue(index).ToString();
            connection.sendMessage(nachricht.ToString(), ip);
            //chat_txtbx.Text += "\r\n" + nachricht.getZeit()+": "+nachricht.getUser()+": ";
            //chat_txtbx.Text += nachricht.getBody();
            OnMessage(nachricht);
        }

        private void chat_txtbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Message nachricht = new Message("SOL", "", "Björn", "ONL", "Hallo ich bin der Brotkasten");
            connection.sendBroadcast(nachricht.ToString());
        }

    }
}
