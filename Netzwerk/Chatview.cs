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
        public Chatview()
        {
            InitializeComponent();
        }

     

       

        private void button1_Click(object sender, EventArgs e)
        {
            NetzwerkInterface connection = new NetzwerkInterface();
            connection.sendMessage("Server, bist du da?");
        }
    }
}
