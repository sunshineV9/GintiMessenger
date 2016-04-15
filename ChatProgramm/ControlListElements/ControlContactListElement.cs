using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProgramm
{
    public partial class ControlContactListElement : UserControl
    {
        #region Vars
        Kontakt kontakt;

        bool selected;

        Color selectedFond = Color.White;

        TableLayoutControlCollection elementList;
        #endregion // Vars

        #region Constructor
        public ControlContactListElement(Kontakt _kontakt, TableLayoutControlCollection _elementList)
        {
            kontakt = _kontakt;
            elementList = _elementList;

            Selected = false;

            InitializeComponent();

            lblName.Text = $"{elementList.Count + 1}: {kontakt.Name}";

            if (kontakt.Onlinestatus)
            {
                panelStatus.BackColor = Color.Green;
            }
            else
            {
                panelStatus.BackColor = Color.Red;
            }
        }
        #endregion // Constructor

        #region Events
        private void ControlContactListElement_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = selectedFond;
        }

        private void panelStatus_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = selectedFond;
        }

        private void lblName_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = selectedFond;
        }

        private void ControlContactListElement_MouseLeave(object sender, EventArgs e)
        {
            if (Selected)
            {
                return;
            }

            this.BackColor = Uebersicht.DefaultBackColor;
        }

        private void panelStatus_MouseLeave(object sender, EventArgs e)
        {
            if (Selected)
            {
                return;
            }

            this.BackColor = Uebersicht.DefaultBackColor;
        }

        private void lblName_MouseLeave(object sender, EventArgs e)
        {
            if (Selected)
            {
                return;
            }

            this.BackColor = Uebersicht.DefaultBackColor;
        }

        private void ControlContactListElement_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (ControlContactListElement element in elementList)
            {
                if (element.Selected)
                {
                    element.Selected = false;
                }
            }

            if (Selected)
            {
                Selected = false;
                return;
            }

            Selected = true;
        }

        private void panelStatus_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (ControlContactListElement element in elementList)
            {
                if (element.Selected)
                {
                    element.Selected = false;
                }
            }

            if (Selected)
            {
                Selected = false;
                return;
            }

            Selected = true;
        }

        private void lblName_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (ControlContactListElement element in elementList)
            {
                if (element.Selected)
                {
                    element.Selected = false;
                }
            }

            if (Selected)
            {
                Selected = false;
                return;
            }

            Selected = true;
        }
        #endregion //Events

        #region Propertys
        /// <summary>
        /// Setzt/Gibt das Selektierte Element zurück
        /// </summary>
        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                if (value == true)
                {
                    this.BackColor = selectedFond;
                }
                else
                {
                    this.BackColor = Uebersicht.DefaultBackColor;
                }

                selected = value;
            }
        }

        /// <summary>
        /// Gibt den Kontaktnamen des ListElements zurück
        /// </summary>
        public Kontakt Kontakt
        {
            get
            {
                return kontakt;
            }
        }
        #endregion // Propertys
    }
}
