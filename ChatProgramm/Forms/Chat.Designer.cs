namespace ChatProgramm
{
    partial class Chat
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
            this.txtChat = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tableLayoutChat = new System.Windows.Forms.TableLayoutPanel();
            this.btnLeaf = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTextLength = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtChat
            // 
            this.txtChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChat.Location = new System.Drawing.Point(13, 471);
            this.txtChat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtChat.MaxLength = 250;
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(550, 164);
            this.txtChat.TabIndex = 0;
            this.txtChat.Text = "";
            this.txtChat.TextChanged += new System.EventHandler(this.txtChat_TextChanged);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(235, 645);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(200, 50);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Nachricht Senden";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tableLayoutChat
            // 
            this.tableLayoutChat.AutoScroll = true;
            this.tableLayoutChat.ColumnCount = 1;
            this.tableLayoutChat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutChat.Location = new System.Drawing.Point(4, 25);
            this.tableLayoutChat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutChat.Name = "tableLayoutChat";
            this.tableLayoutChat.RowCount = 1;
            this.tableLayoutChat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutChat.Size = new System.Drawing.Size(568, 431);
            this.tableLayoutChat.TabIndex = 2;
            // 
            // btnLeaf
            // 
            this.btnLeaf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeaf.Location = new System.Drawing.Point(443, 645);
            this.btnLeaf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLeaf.Name = "btnLeaf";
            this.btnLeaf.Size = new System.Drawing.Size(120, 50);
            this.btnLeaf.TabIndex = 3;
            this.btnLeaf.Text = "Verlassen";
            this.btnLeaf.UseVisualStyleBackColor = true;
            this.btnLeaf.Click += new System.EventHandler(this.btnLeaf_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutChat);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(576, 461);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Verlauf";
            // 
            // lblTextLength
            // 
            this.lblTextLength.AutoSize = true;
            this.lblTextLength.Location = new System.Drawing.Point(12, 640);
            this.lblTextLength.Name = "lblTextLength";
            this.lblTextLength.Size = new System.Drawing.Size(106, 19);
            this.lblTextLength.TabIndex = 4;
            this.lblTextLength.Text = "Zeichen: 0/250";
            // 
            // Chat
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(576, 709);
            this.Controls.Add(this.lblTextLength);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLeaf);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtChat);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Chat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat mit Freund";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtChat;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TableLayoutPanel tableLayoutChat;
        private System.Windows.Forms.Button btnLeaf;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTextLength;
    }
}