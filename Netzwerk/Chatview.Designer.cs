﻿namespace Netzwerk
{
    partial class Chatview
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.send_btn = new System.Windows.Forms.Button();
            this.chat_txtbx = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 231);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(496, 73);
            this.textBox1.TabIndex = 0;
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(12, 310);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(75, 23);
            this.send_btn.TabIndex = 1;
            this.send_btn.Text = "Senden";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // chat_txtbx
            // 
            this.chat_txtbx.Location = new System.Drawing.Point(13, 13);
            this.chat_txtbx.Multiline = true;
            this.chat_txtbx.Name = "chat_txtbx";
            this.chat_txtbx.ReadOnly = true;
            this.chat_txtbx.Size = new System.Drawing.Size(495, 212);
            this.chat_txtbx.TabIndex = 2;
            this.chat_txtbx.TextChanged += new System.EventHandler(this.chat_txtbx_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Chatview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 345);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chat_txtbx);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.textBox1);
            this.Name = "Chatview";
            this.Text = "Chatview";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.TextBox chat_txtbx;
        private System.Windows.Forms.Button button1;

    }
}

