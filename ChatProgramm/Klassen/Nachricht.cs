using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Data.Odbc;
using System.Data;

namespace ChatProgramm
{
    [Serializable()]
    public class Nachricht
    {
        #region vars
        string text;
        #endregion // vars

        #region Constructor
        /// <summary>
        /// Erstellt ein neues Nachricht-Objekt
        /// </summary>
        /// <param name="_text">Text der Nachricht</param>
        /// <param name="_sender">Sender der Nachricht</param>
        /// <param name="_receiver">Empfänger der Nachricht</param>
        /// <param name="_timeStamp">Zeitstempel bei der Erstellung der Nachricht</param>
        public Nachricht(string _text, Kontakt _sender, Kontakt _receiver, string _timeStamp)
        {
            Text = _text;
            Sender = _sender;
            Receiver = _receiver;
            TimeStamp = _timeStamp;
        }
        #endregion // Constructor

        #region Methods
        /// <summary>
        /// Sendet die Nachricht an den Empfänger indem ein Hintergrund-Thread gestartet wird
        /// </summary>
        /// <param name="_port">Port an dem gesendet werden soll</param>
        public void Send(int _port)
        {
            // neuen Client erstellen, der die Nachricht an den Empfänger sendet
            Client client = new Client(this);
            client.Start(_port); // Client-Thread starten
        }

        /// <summary>
        /// Speichert die Nachricht in der Datenbank ab
        /// </summary>
        public void SaveToDB()
        {
            // INSERT-Befehl um die Nachricht in der Datenbank abzusichern
            string sqlinster = $"INSERT INTO Chats (`From`, `To`, `Content`, `OwnerID`) VALUES ('{Sender.Name}', '{Receiver.Name}', '{Text}', {Benutzer.ID})";

            if (!DBConnection.ExecuteNonQuery(sqlinster))
            {
                // Fehler bei Datenbankabfrage
                throw new ApplicationException("Bei der Datenbankabfrage ist ein Fehler aufgetreten!");
            }
        }
        #endregion // Methods

        #region Properties
        /// <summary>
        /// Setzt/Gibt den Text der Nachrcht zurück
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                // Text darf nicht leer sein
                if (value == "")
                {
                    throw new FormatException("Nachricht darf nicht leer sein");
                }
                else
                {
                    text = value;
                }
            }
        }

        /// <summary>
        /// Setzt/Gibt den Sender der Nachricht zurück
        /// </summary>
        public Kontakt Sender
        {
            get;
            set;
        }

        /// <summary>
        /// Setzt/Gibt den Empfänger der Nachricht zurück
        /// </summary>
        public Kontakt Receiver
        {
            get;
            set;
        }

        /// <summary>
        /// Setzt/Gibt die Zeit bei der Erstellung der Nachricht zurück
        /// </summary>
        public string TimeStamp
        {
            get;
            set;
        }
        #endregion // Properties
    }
}
