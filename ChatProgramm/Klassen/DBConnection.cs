using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Net.NetworkInformation;

namespace ChatProgramm
{
    static class DBConnection
    {
        #region Vars
        // Connection-String für Datenbank mit öffentlicher IP von außerhalb des Schulnetzes
        static string connStrServer1 = "Driver={MySQL ODBC 3.51 Driver};" +
                                        "Server=;Database=;" +
                                        "User=;Password=;Option=3;";

        // Connection-String für Datenbank mit öffentlicher IP von innerhalb des Schulnetzes
        static string connStrServer2 = "Driver={MySQL ODBC 3.51 Driver};" +
                                        "Server=;Database=;" +
                                        "User=;Password=;Option=3;";
        #endregion // Vars

        #region Constructor
        /// <summary>
        /// Erzeugt die statische DBConnection-Klasse
        /// </summary>
        static DBConnection()
        {
            SetServerConnection();
        }
        #endregion //Constructor

        #region Methods
        /// <summary>
        /// Setzt die Serververbindung für die IP innerhalb/außerhalb des Schulnetzes
        /// </summary>
        private static void SetServerConnection()
        {
            // Ping ausführen um zu prüfen über welche IP-Adresse der Datenbank-Server erreicht werden kann
            Ping sender = new Ping();
            PingReply result = sender.Send("193.170.68.49");

            // Ping-Ergebniss abfragen
            if (result.Status == IPStatus.Success)
            {
                // Ping auf externe IP-Adresse des Servers erfolgreich -> Connection-String 1 verwenden
                ConnServer = new OdbcConnection(connStrServer1);
            }
            else
            {
                // Ping auf externe IP-Adresse des Servers fehlgeschlagen -> interne IP-Adresse versuchen
                result = sender.Send("10.174.27.3");

                if (result.Status == IPStatus.Success)
                {
                    // Erfolgreicher Ping -> Connection-String 2 verwenden
                    ConnServer = new OdbcConnection(connStrServer2);
                }
            }
        }

        /// <summary>
        /// Führt einen SQL-SELECT-Befehl an der Datenbank aus
        /// </summary>
        /// <param name="_statement">SQL-SELECT-Befehl der ausgeführt werden soll</param>
        /// <returns>Liste mit den Zeilen aus der Datenbank</returns>
        public static List<Dictionary<string, string>> ExecuteSELECT(string _statement)
        {
            // variablen setzen
            string statement = _statement;

            List<Dictionary<string, string>> output = new List<Dictionary<string, string>>(); // Liste mit Assoziativen Arrays für die Spaltennamen

            OdbcCommand cmd = new OdbcCommand(statement, ConnServer);
            OdbcDataReader dr;
            
            try
            {
                // Verbindung öffnen
                if(OpenConn(cmd.Connection))
                {
                    // Select-Befehl ausführen
                    dr = cmd.ExecuteReader();

                    // Prüfen ob ein Ergebniss zurückgekommen ist
                    if (!dr.HasRows)
                    {
                        return null;
                    }

                    // List<Array> mit Ergebniss befüllen
                    while (dr.Read())
                    {
                        Dictionary<string, string> fields = new Dictionary<string, string>();

                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            fields.Add(dr.GetName(i), dr[i].ToString());
                        }

                        output.Add(fields);
                    }
                }
                else
                {
                    throw new ApplicationException("Konnte keine Verbindung zur Datenbank aufbauen!");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // Verbindung am Ende schließen
                CloseConn(cmd.Connection);
            }

            return output;
        }

        /// <summary>
        /// Führt einen Nicht-SQL-SELECT-Befehl an der Datenbank aus
        /// </summary>
        /// <param name="_statement">Nicht-SQL-SELECT-Befehl der ausgeführt werden soll</param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string _statement)
        {
            // variablen setzen
            string statement = _statement;

            OdbcCommand cmd = new OdbcCommand(statement, ConnServer);
            
            try
            {
                // Verbindung öffnen
                if(OpenConn(cmd.Connection))
                {
                    // Nicht-SELECT-Befehl ausführen
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // Verbindung schließen
                CloseConn(cmd.Connection);
            }

            return true;
        }

        #region ConnectionMethods
        /// <summary>
        /// Öffnet die Verbindung zur Datenbank
        /// </summary>
        /// <param name="_conn">Odbc-Verbindung zur Datenbank</param>
        /// <returns>TRUE wenn erfolgreich verbunden, FALSE wenn bereits eine Verbindung zu dieser Datenbank geöffnet ist</returns>
        private static bool OpenConn(OdbcConnection _conn)
        {
            // Warten, falls eine Verbindung geöffnet ist
            while (_conn.State != System.Data.ConnectionState.Closed)
            {
                System.Threading.Thread.Sleep(100);
            }

            // Prüfen ob Verbindung geschlossen ist
            if (_conn.State == System.Data.ConnectionState.Closed)
            {
                // Verbindung öffnen
                _conn.Open();
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Schließt die Verbindung zur Datenbank
        /// </summary>
        /// <param name="_conn">Odbc-Verbindung zur Datenbank</param>
        /// <returns>TRUE wenn erfolgreich die Verbindung geschlossen wurde, FALSE wenn keine Verbindung vorhanden ist</returns>
        private static bool CloseConn(OdbcConnection _conn)
        {
            // Prüfen ob Verbindung einen anderen Zustand als geschlossen hat
            if (_conn.State != System.Data.ConnectionState.Closed)
            {
                // Verbindung schließen
                _conn.Close();
                return true;
            }

            return false;
        }
        #endregion //ConnectionMethods
        #endregion // Methods

        #region Properties
        /// <summary>
        /// Setzt/Gibt die Odbc-Verbindung für die Server Datenbank-Verbindung zurück
        /// </summary>
        private static OdbcConnection ConnServer
        {
            get;
            set;
        }
        #endregion // Properties
    }
}
