using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;

namespace ChatProgramm
{
    public partial class Registrieren : Form
    {
        public Registrieren()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (txtConfPassword.TextLength == 0 || txtPassword.TextLength == 0 || txtUsername.TextLength == 0)
            {
                MessageBox.Show("Es darf keine leeren Felder geben!");
                return;
            }

            if (txtPassword.Text != txtConfPassword.Text)
            {
                MessageBox.Show("Passwörter stimmen nicht überein!");
                return;
            }

            try
            {
                Benutzer.Regist(txtUsername.Text, txtPassword.Text);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler bei Registrierung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
