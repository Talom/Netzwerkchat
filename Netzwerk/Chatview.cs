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
        public Chatview()
        {
            InitializeComponent();
            connection = new NetzwerkInterface();
            
        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            Message nachricht = new Message("MSG", "", "Björn", "ONL", this.textBox1.Text);
            connection.sendMessage(nachricht.ToString(), "127.0.0.1");
            chat_txtbx.Text += "\r\n" + nachricht.getZeit()+": "+nachricht.getUser()+": ";
            chat_txtbx.Text += nachricht.getBody();
        }

        private void chat_txtbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Message nachricht = new Message("MSG", "", "Björn", "ONL", this.textBox1.Text);
            connection.sendBroadcast(nachricht.ToString());
        }

    }
}
