using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace ChatProgramm
{
    public partial class Uebersicht : Form
    {
        #region Vars
        const int PORT = 5555;
        bool stopThread;
        Thread UpdateListThread;
        #endregion // Vars

        #region Constructor
        public Uebersicht()
        {
            stopThread = false;

            InitializeComponent();
            
            DrawList();
            
            Server.Start(PORT);

            UpdateListThread = new Thread(new ThreadStart(UpdateList));
            UpdateListThread.Start();
        }
        #endregion // Constructor

        #region Events
        #region ButtonEvents
        private void btnChat_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Bitte wählen Sie einen Kontakt aus!", 
                                "Kein Kontakt ausgewählt", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Error);

                return;
            }

            Chat chat = new Chat(SelectedItem.Kontakt);

            chat.Show();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelteKontakt_Click(object sender, EventArgs e)
        {
            try
            {
                Benutzer.DeleteKontakt(SelectedItem.Kontakt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler bei Kontaktlöschen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion // ButtonEvents

        #region MenuEvents
        private void Kontaktanfrage_Click(object sender, EventArgs e)
        {
            Kontaktanfrage anfrage = new Kontaktanfrage();

            anfrage.ShowDialog();
        }

        private void Option_Click(object sender, EventArgs e)
        {
            Optionen optionen = new Optionen();

            optionen.ShowDialog();
        }
        #endregion // MenuEvents
        
        private void tableLayoutContacts_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (ControlContactListElement element in tableLayoutContacts.Controls)
            {
                element.Selected = false;
            }
        }
        
        private void Uebersicht_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult dr = MessageBox.Show("Möchten Sie sich wirklich ausloggen?", "Ausloggen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        stopThread = true;
                        UpdateListThread.Abort();
                        
                        Benutzer.Logout();

                        MessageBox.Show("Erfolgreich ausgeloggt!", "Logout erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Login login = new Login();

                        login.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Fehler bei Ausloggen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }

            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                DialogResult dr = MessageBox.Show("Möchten Sie das Programm wirklich beenden?", "Programm Beenden?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        stopThread = true;
                        UpdateListThread.Abort();

                        Benutzer.Logout();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Fehler bei Beenden", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            
            Server.Stop();
        }
        #endregion // Events

        #region Methods
        /// <summary>
        /// Zeichnet die Kontaktliste
        /// </summary>
        private void DrawList()
        {
            // variablen setzen
            string name = "";

            // Ist derzeit ein Element ausgewählt?
            if (SelectedItem != null)
            {
                name = SelectedItem.Kontakt.Name;
            }

            // Panel leeren
            tableLayoutContacts.Controls.Clear();
            
            // Panel mit den ListElementen füllen
            for (int i = 0; i < Benutzer.Kontakte.Count; i++)
            {
                ControlContactListElement listElement = new ControlContactListElement(Benutzer.Kontakte[i], tableLayoutContacts.Controls);

                if (listElement.Kontakt.Name == name)
                {
                    listElement.Selected = true;
                }

                tableLayoutContacts.Controls.Add(listElement);
            }
        }
        
        /// <summary>
        /// Aktualisiert die Kontaktliste indem die Kontakte des Benutzers aktualisiert werden und die Kontakliste neu gezeichnet wird
        /// </summary>
        /// <param name="args"></param>
        private void UpdateList()
        {
            while (!stopThread)
            {
                Thread.Sleep(10000);

                MethodInvoker PanelUpdate = delegate
                {
                    try
                    {
                        Benutzer.UpdateKontakte();

                        DrawList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Fehler bei Kontakteakualisieren", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                };

                Invoke(PanelUpdate);
            }
        }
        #endregion // Methods

        #region Properties
        /// <summary>
        /// Gibt das derzeit Selektierte ControlContactListElement zurück
        /// </summary>
        private ControlContactListElement SelectedItem
        {
            get
            {
                foreach (ControlContactListElement element in tableLayoutContacts.Controls)
                {
                    if (element.Selected)
                    {
                        return element;
                    }
                }

                return null;
            }
        }
        #endregion // Properties
    }
}
