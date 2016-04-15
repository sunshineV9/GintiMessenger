using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace ChatProgramm
{
    public partial class Optionen : Form
    {
        public Optionen()
        {
            InitializeComponent();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClearChatHistory_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Möchten Sie wirkliche den gesamten Chatverlauf löschen?", "Chatverlauf löschen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                ClearChatHistory();
            }
        }

        /// <summary>
        /// Löscht den gesamten Chatverlauf des Benutzers und gibt entsprechene Rückmeldung mittels Messagebox
        /// </summary>
        private void ClearChatHistory()
        {
            try
            {
                string sqldelete = $"DELETE FROM CHATS WHERE OwnerID = '{Benutzer.ID}'";

                if (DBConnection.ExecuteNonQuery(sqldelete))
                {
                    MessageBox.Show("Chatverlauf erfolgreich gelöscht!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Beim löschen des Chatverlaufes ist ein Fehler aufgetreten!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler bei Chatlöschen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
