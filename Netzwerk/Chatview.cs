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
            Message nachricht = new Message("MSG", "", "Björn", "ONL", this.textBox1.Text + textBox1.Text.Length);
            connection.sendMessage(nachricht.ToString(), "127.0.0.1");
        }

    }
}
