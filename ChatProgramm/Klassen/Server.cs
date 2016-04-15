using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ChatProgramm
{
    static class Server
    {
        #region Methods
        /// <summary>
        /// Startet den Hintergrund-Thread vom Server
        /// </summary>
        /// <param name="_port">Port an dem Empfangen werden soll</param>
        public static void Start(int _port)
        {
            // variablen setzen
            ShouldStop = false;
            Port = _port;

            // Hintergrund-Thread starten
            ServerThread = new Thread(new ThreadStart(Receiving));
            ServerThread.Start();
        }

        /// <summary>
        /// Stoppt den Hintergrund-Thread vom Server
        /// </summary>
        public static void Stop()
        {
            ShouldStop = true; // Stopp-Variable für Thread auf true setzen
        }

        /// <summary>
        /// Empfängt Nachrichten von Clients auf dem gewählten Port
        /// </summary>
        private static void Receiving()
        {
            // variablen setzen
            bool contains = false;

            TcpListener Server = new TcpListener(IPAddress.Parse("0.0.0.0"), Port);

            try
            {
                // Sever starten
                Server.Start();

                // Solange der die Thread-Stop-Variable nicht auf true ist soll der Server auf dem angegebenen Port auf Verbindungen warten
                while (!ShouldStop)
                {
                    try
                    {
                        if (Server.Pending())
                        {
                            // Daten empfangen
                            Socket socket = Server.AcceptSocket();

                            byte[] data = new byte[socket.ReceiveBufferSize];

                            socket.Receive(data, socket.ReceiveBufferSize, SocketFlags.None);

                            // Empfangene Daten umwandeln in ein Nachricht-Objekt
                            MemoryStream stream = new MemoryStream(data);

                            BinaryFormatter formatter = new BinaryFormatter();

                            Nachricht nachricht = (Nachricht)formatter.Deserialize(stream);

                            // Verbindung schließen
                            stream.Close();
                            socket.Close();

                            // Nachricht leer
                            if (nachricht == null)
                            {
                                // Client beendet
                                break;
                            }

                            // Nachricht abspeichern und anzeigen
                            nachricht.SaveToDB();

                            // Alle geöffneten Forms durchgehen um zu sehen ob das Chatfenster mit dem Kontakt bereits geöffnet ist
                            foreach (Form form in Application.OpenForms)
                            {
                                if (form.Name == $"Chat mit {nachricht.Sender.Name}")
                                {
                                    Chat chat = (Chat)form;

                                    chat.AddToChat(nachricht, 0);
                                    contains = true;
                                }
                            }

                            // Chatfenster noch nicht geöffnet -> öffnen
                            if (!contains)
                            {
                                Thread ChatThread = new Thread(() => FormShowDialog(new Chat(nachricht.Sender)));
                                ChatThread.Start();
                            }
                        }
                        else
                        {
                            // Warten um Prozessauslastung zu senken
                            Thread.Sleep(350);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Fehler bei Empfangen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler bei Empfangen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                // wenn die Schleife irgendwann beendet wird, Thread und Server beenden
                Server.Stop();
                ServerThread.Abort();
            }
        }

        private static void FormShowDialog(Chat _chat)
        {
            _chat.ShowDialog();
        }
        #endregion // Methods

        #region Properties
        /// <summary>
        /// Setzt/Gibt die Anweisung zum Stoppen des Server zurück
        /// </summary>
        private static bool ShouldStop
        {
            get;
            set;
        }

        /// <summary>
        /// Setzt/Gibt den Hintergrund-Thread vom Server zurück
        /// </summary>
        private static Thread ServerThread
        {
            get;
            set;
        }

        /// <summary>
        /// Setzt/Gibt den Port auf dem der Server empfängt zurück
        /// </summary>
        private static int Port
        {
            get;
            set;
        }
        #endregion // Properties
    }
}
