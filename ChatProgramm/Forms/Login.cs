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

namespace ChatProgramm
{
    public partial class Login : Form
    {
        bool allowDisplay;

        #region Constructor
        public Login(bool _signedOut)
        {
            allowDisplay = true;

            InitializeComponent();

            if (!_signedOut)
            {
                allowDisplay = false;

                string benutzername = AppSettings.getAppSetting("LoginDataName");
                string passwort = AppSettings.getAppSetting("LoginDataPW");

                PerformLogin(benutzername, passwort);
            }
        }

        public Login(): this(true)
        {
            AppSettings.setAppSetting("SigndUp", "no");
            AppSettings.setAppSetting("LoginDataName", "");
            AppSettings.setAppSetting("LoginDataPW", "");
        }
        #endregion Constructor

        #region Events
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string benutzername = txtUsername.Text;
            string passwort = txtPassword.Text;
            
            if (ckBxSigndUp.Checked)
            {
                AppSettings.setAppSetting("SigndUp", "yes");
                AppSettings.setAppSetting("LoginDataName", benutzername);
                AppSettings.setAppSetting("LoginDataPW", passwort);
            }
            else
            {
                AppSettings.setAppSetting("SigndUp", "no");
            }

            PerformLogin(benutzername, passwort);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Registrieren regist = new Registrieren();

            regist.ShowDialog();

            if (regist.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("Sie haben sich erfolgreich registriert!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion // Events

        #region Methods
        /// <summary>
        /// Führt die Benutzer.Login Methode aus, ruft entsprechende Messageboxen bei Fehlermeldungen auf und öffnet bei Erfolg die Übersicht-Form
        /// </summary>
        /// <param name="_benutzername">eingegebener Benutzername</param>
        /// <param name="_passwort">eingegebenes Passwort</param>
        private void PerformLogin(string _benutzername, string _passwort)
        {
            try
            {
                Benutzer.Login(_benutzername, _passwort);

                MessageBox.Show("Erfolgreich eingeloggt!", "Login Erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Uebersicht uebersicht = new Uebersicht();
                uebersicht.Show();

                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler bei Login!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }
        #endregion // Methods

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowDisplay ? value : allowDisplay);
        }
    }
}
