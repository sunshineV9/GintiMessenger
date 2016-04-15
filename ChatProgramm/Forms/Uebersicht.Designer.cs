namespace ChatProgramm
{
    partial class Uebersicht
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
            this.btnChat = new System.Windows.Forms.Button();
            this.tableLayoutContacts = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxContacts = new System.Windows.Forms.GroupBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kontaktanfrageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelteKontakt = new System.Windows.Forms.Button();
            this.groupBoxContacts.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChat
            // 
            this.btnChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChat.Location = new System.Drawing.Point(275, 30);
            this.btnChat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChat.Name = "btnChat";
            this.btnChat.Size = new System.Drawing.Size(120, 35);
            this.btnChat.TabIndex = 2;
            this.btnChat.Text = "Schreiben";
            this.btnChat.UseVisualStyleBackColor = true;
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // tableLayoutContacts
            // 
            this.tableLayoutContacts.AutoScroll = true;
            this.tableLayoutContacts.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutContacts.ColumnCount = 1;
            this.tableLayoutContacts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutContacts.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutContacts.Name = "tableLayoutContacts";
            this.tableLayoutContacts.RowCount = 1;
            this.tableLayoutContacts.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutContacts.Size = new System.Drawing.Size(262, 504);
            this.tableLayoutContacts.TabIndex = 6;
            this.tableLayoutContacts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tableLayoutContacts_MouseDown);
            // 
            // groupBoxContacts
            // 
            this.groupBoxContacts.Controls.Add(this.tableLayoutContacts);
            this.groupBoxContacts.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxContacts.Location = new System.Drawing.Point(0, 25);
            this.groupBoxContacts.Name = "groupBoxContacts";
            this.groupBoxContacts.Size = new System.Drawing.Size(268, 530);
            this.groupBoxContacts.TabIndex = 7;
            this.groupBoxContacts.TabStop = false;
            this.groupBoxContacts.Text = "Kontakte:";
            // 
            // btnEnd
            // 
            this.btnEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnd.Location = new System.Drawing.Point(275, 513);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(120, 35);
            this.btnEnd.TabIndex = 8;
            this.btnEnd.Text = "Beenden";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(373, 66);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(22, 23);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Green;
            this.panel2.Location = new System.Drawing.Point(373, 99);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(22, 23);
            this.panel2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 70);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "Offline";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 102);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 19);
            this.label2.TabIndex = 12;
            this.label2.Text = "Online";
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Location = new System.Drawing.Point(275, 468);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(120, 35);
            this.btnLogout.TabIndex = 13;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.kontaktanfrageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(422, 25);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(69, 19);
            this.toolStripMenuItem1.Text = "Optionen";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.Option_Click);
            // 
            // kontaktanfrageToolStripMenuItem
            // 
            this.kontaktanfrageToolStripMenuItem.Name = "kontaktanfrageToolStripMenuItem";
            this.kontaktanfrageToolStripMenuItem.Size = new System.Drawing.Size(100, 19);
            this.kontaktanfrageToolStripMenuItem.Text = "Kontaktanfrage";
            this.kontaktanfrageToolStripMenuItem.Click += new System.EventHandler(this.Kontaktanfrage_Click);
            // 
            // btnDelteKontakt
            // 
            this.btnDelteKontakt.Location = new System.Drawing.Point(276, 410);
            this.btnDelteKontakt.Name = "btnDelteKontakt";
            this.btnDelteKontakt.Size = new System.Drawing.Size(120, 50);
            this.btnDelteKontakt.TabIndex = 15;
            this.btnDelteKontakt.Text = "Kontakt entfernen";
            this.btnDelteKontakt.UseVisualStyleBackColor = true;
            this.btnDelteKontakt.Click += new System.EventHandler(this.btnDelteKontakt_Click);
            // 
            // Uebersicht
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(422, 555);
            this.Controls.Add(this.btnDelteKontakt);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.groupBoxContacts);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnChat);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(394, 502);
            this.Name = "Uebersicht";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Übersicht";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Uebersicht_FormClosing);
            this.groupBoxContacts.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnChat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutContacts;
        private System.Windows.Forms.GroupBox groupBoxContacts;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem kontaktanfrageToolStripMenuItem;
        private System.Windows.Forms.Button btnDelteKontakt;
    }
}

