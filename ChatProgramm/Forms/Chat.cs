using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;

namespace ChatProgramm
{
    public partial class Chat : Form
    {
        #region Vars
        const int MAX_LENGTH = 250;

        Nachricht nachricht;
        Kontakt kontakt;
        int actRow;
        const int PORT = 5555;
        #endregion // Vars

        #region Constructor
        public Chat(Kontakt _kontact)
        {
            kontakt = _kontact;
            actRow = 0;

            InitializeComponent();
            
            txtChat.MaxLength = MAX_LENGTH;
            Text = $"Chat mit {kontakt.Name}";

            Name = Text;

            DrawChat();
        }
        #endregion // Constructor

        #region Events
        #region ButtonEvents
        private void btnLeaf_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                nachricht = new Nachricht(txtChat.Text, new Kontakt(Benutzer.ID), kontakt, DateTime.Now.ToString());

                AddToChat(nachricht, 1);

                nachricht.Send(PORT);
                
                nachricht.SaveToDB();
                
                txtChat.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler beim Senden", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion // ButtonEvents
        #endregion // Events

        #region Methods
        /// <summary>
        /// Zeichnet den Chatverlauf zwischen Benutzer und Kontakt in das TableLayoutPanel
        /// </summary>
        private void DrawChat()
        {
            tableLayoutChat.Controls.Clear();

            List<Nachricht> chat = Benutzer.GetChatHistory(kontakt);
            
            if (chat != null)
            {
                for (int i = 0; i < chat.Count; i++)
                {
                    if (chat[i].Sender.Name == Benutzer.Name)
                    {
                        AddToChat(chat[i], 1);
                    }
                    else
                    {
                        AddToChat(chat[i], 0);
                    }
                }
            }
        }

        /// <summary>
        /// Fügt eine neue Nachricht in das TableLayoutPanel ein
        /// </summary>
        /// <param name="_nachricht"></param>
        /// <param name="_rechts"></param>
        public void AddToChat(Nachricht _nachricht, int _rechts)
        {
            Nachricht nachricht = _nachricht;
            int rechts = _rechts;

            if (InvokeRequired)
            {
                Invoke(new Action<Nachricht, int>(AddToChat), nachricht, rechts);
            }
            else
            {
                ControlChatListElement element = new ControlChatListElement(nachricht.Text, nachricht.TimeStamp);

                if (_rechts == 1)
                {
                    element.Anchor = AnchorStyles.Right;
                }
                
                tableLayoutChat.Controls.Add(element, rechts, actRow);
                actRow++;
            }
        }
        #endregion // Methods

        private void txtChat_TextChanged(object sender, EventArgs e)
        {
            lblTextLength.Text = "Zeichen: " + txtChat.Text.Length.ToString() + "/250";
        }
    }
}
