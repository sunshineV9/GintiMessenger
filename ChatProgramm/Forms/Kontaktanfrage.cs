using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ChatProgramm
{
    public partial class Kontaktanfrage : Form
    {
        public Kontaktanfrage()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length == 0)
            {
                MessageBox.Show("Bitte geben Sie zuerst einen Namen ein!", "Kein Name vorhanden!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Benutzer.SendRequest(Kontakt.SelectKontaktFromDB(txtUsername.Text));

                MessageBox.Show("Kontaktanfrage erfolgreich gesendet!", "Kontaktanfrage gesendet!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler bei Kontaktanfrage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
