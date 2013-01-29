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
    public partial class Kontaktview : Form
    {
        private Button button1;
    
        public Kontaktview()
        {
            connection = new NetzwerkInterface();
        }
        public List<String> userlist
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Netzwerk.Chatview[] chat
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public NetzwerkInterface connection
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(101, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Kontaktview
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button1);
            this.Name = "Kontaktview";
            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.sendMessage("Server, bist du da?");
        }
    }
}
