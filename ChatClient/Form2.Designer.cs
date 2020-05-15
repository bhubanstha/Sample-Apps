namespace ChatClient
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel55 = new System.Windows.Forms.Panel();
            this.pnlUserList = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ctrlTyping = new chat.TypingBox();
            this.panel55.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel55
            // 
            this.panel55.AutoScroll = true;
            this.panel55.AutoScrollMargin = new System.Drawing.Size(5, 0);
            this.panel55.AutoScrollMinSize = new System.Drawing.Size(5, 0);
            this.panel55.Controls.Add(this.pnlUserList);
            this.panel55.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel55.Location = new System.Drawing.Point(0, 0);
            this.panel55.Name = "panel55";
            this.panel55.Size = new System.Drawing.Size(238, 450);
            this.panel55.TabIndex = 0;
            // 
            // pnlUserList
            // 
            this.pnlUserList.AutoScroll = true;
            this.pnlUserList.AutoScrollMargin = new System.Drawing.Size(5, 0);
            this.pnlUserList.AutoScrollMinSize = new System.Drawing.Size(5, 0);
            this.pnlUserList.BackColor = System.Drawing.Color.Gray;
            this.pnlUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUserList.Location = new System.Drawing.Point(0, 0);
            this.pnlUserList.Name = "pnlUserList";
            this.pnlUserList.Size = new System.Drawing.Size(238, 450);
            this.pnlUserList.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMargin = new System.Drawing.Size(5, 0);
            this.panel2.AutoScrollMinSize = new System.Drawing.Size(5, 0);
            this.panel2.Controls.Add(this.pnlMessage);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(238, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(578, 450);
            this.panel2.TabIndex = 1;
            // 
            // pnlMessage
            // 
            this.pnlMessage.AutoScroll = true;
            this.pnlMessage.AutoScrollMargin = new System.Drawing.Size(5, 0);
            this.pnlMessage.AutoScrollMinSize = new System.Drawing.Size(5, 0);
            this.pnlMessage.BackColor = System.Drawing.Color.Silver;
            this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMessage.Location = new System.Drawing.Point(0, 0);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Size = new System.Drawing.Size(578, 396);
            this.pnlMessage.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.panel3.Controls.Add(this.ctrlTyping);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 396);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(578, 54);
            this.panel3.TabIndex = 0;
            // 
            // ctrlTyping
            // 
            this.ctrlTyping.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ctrlTyping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlTyping.Location = new System.Drawing.Point(0, 0);
            this.ctrlTyping.Name = "ctrlTyping";
            this.ctrlTyping.Size = new System.Drawing.Size(578, 54);
            this.ctrlTyping.TabIndex = 0;
            this.ctrlTyping.Value = "Type here...";
            this.ctrlTyping.OnHitEnter += new chat.TypingBox.HitEnter(this.ctrlTyping_OnHitEnter);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(816, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel55);
            this.Name = "Form2";
            this.Text = "Chapt App";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel55.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel55;
        private System.Windows.Forms.Panel pnlUserList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private chat.TypingBox ctrlTyping;
        private System.Windows.Forms.Panel pnlMessage;
    }
}