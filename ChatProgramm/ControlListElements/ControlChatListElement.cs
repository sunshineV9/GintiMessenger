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
    public partial class ControlChatListElement : UserControl
    {
        const int UMBRUCH = 40;

        public ControlChatListElement(string _text, string _timeStamp)
        {
            InitializeComponent();

            int length = _text.Length;

            // Bei gewisser länge der Nachricht einen Zeilenumbruch einfügen
            while (length > UMBRUCH)
            {
                if (_text.Contains(" "))
                {
                    // Wörter nicht in der Mitte trennen, sondern immer nur an leeren stellen
                    if (_text[length - 1] == ' ')
                    {
                        _text = _text.Insert(length, Environment.NewLine);
                        length = length - UMBRUCH;
                    }
                    else
                    {
                        length--;
                    }
                }
                else
                {
                    _text = _text.Insert(length, Environment.NewLine);
                    length -= UMBRUCH;
                }
            }

            txtChat.Text = _text;
            txtChat.Text += Environment.NewLine;
            txtChat.Text += _timeStamp; // Zeitstempel in Textbox anhängen
        }

        #region Autosize
        private void txtChat_TextChanged(object sender, EventArgs e)
        {
            // Nötige größe der Textbox anhand des Textinhaltes herausfinden
            Size size = TextRenderer.MeasureText(txtChat.Text, txtChat.Font);

            Resize(size);
        }

        /// <summary>
        /// Setzt die größe für das Element
        /// </summary>
        /// <param name="_size">Größe die verwendet werden soll</param>
        private new void Resize(Size _size)
        {
            // Zu der Größe ein offset hinzufügen, damit der Text nicht abgeschnitten wird
            // und das ControllElement größer ist als die Textbox
            txtChat.Width = _size.Width + 10;
            txtChat.Height = _size.Height + 10;
            Width = _size.Width + 15;
            Height = _size.Height + 15;
        }
        #endregion // Autosize
    }
}
