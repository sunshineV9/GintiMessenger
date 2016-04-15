using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ChatProgramm
{
    class Client
    {
        #region Constructor
        /// <summary>
        /// Erstellt ein neues Client-Objekt anhand einer Nachricht
        /// </summary>
        /// <param name="_nachricht">Nachricht die gesendet werden soll</param>
        public Client(Nachricht _nachricht)
        {
            Nachricht = _nachricht;
        }
        #endregion // Constructor

        #region Methods
        /// <summary>
        /// Startet den Hintergrund-Thread für den Client
        /// </summary>
        public void Start(int _port)
        {
            // Port setzen
            Port = _port;

            // Hintergrund-Thread mit Methode Sending starten
            ClientThread = new Thread(new ThreadStart(Sending));
            ClientThread.Start();
        }

        /// <summary>
        /// Sendet das Nachricht-Objekt an die angegebene Verbindung
        /// </summary>
        private void Sending()
        {
            try
            {
                // Mit Empfänger verbinden und Nachricht-Objekt senden
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Nachricht.Receiver.IP), Port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Mit Empfänger verbinden
                socket.Connect(ep);

                if (socket.Connected)
                {
                    // Nachricht-Objekt serialisieren
                    MemoryStream stream = new MemoryStream();

                    BinaryFormatter formatter = new BinaryFormatter();

                    formatter.Serialize(stream, Nachricht);

                    //Senden
                    socket.Send(stream.ToArray(), stream.ToArray().Length, SocketFlags.None);

                    // Verbindung trennen
                    socket.Close();
                    stream.Close();
                }
                else
                {
                    throw new ApplicationException("Konnte nicht mit Empfänger verbinden!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler bei Senden", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion // Methods

        #region Properties
        /// <summary>
        /// Setzt/Gibt die zu sendende Nachricht zurück
        /// </summary>
        private Nachricht Nachricht
        {
            get;
            set;
        }

        /// <summary>
        /// Setzt/Gibt den Hintergrund-Thread zurück
        /// </summary>
        private Thread ClientThread
        {
            get;
            set;
        }

        /// <summary>
        /// Setzt/Gibt den Port zurück an dem der Client senden soll
        /// </summary>
        private int Port
        {
            get;
            set;
        }
        #endregion // Properties
    }
}
