using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Odbc;
using System.Net;
using System.Net.Sockets;
using System.Data;

namespace ChatProgramm
{
    [Serializable()]
    public static class Benutzer
    {
        #region vars
        private static List<Kontakt> kontakte;
        private static string name, passwort, ip;
        private static bool isResetingProperties;
        #endregion // vars

        #region Methods
        #region Public
        /// <summary>
        /// Prüft ob die Login-Daten für den Benutzer korrekt sind und setzt den Onlinestatus in der Datenbank auf 1
        /// </summary>
        /// <param name="_benutzername">Benutzername</param>
        /// <param name="_passwort">Passwort</param>
        public static void Login(string _benutzername, string _passwort)
        {
            // variablen setzen
            Name = _benutzername;
            Passwort = _passwort;

            // SQL-SELECT-Befehl ausführen
            string sqlupdate = $"UPDATE USERS SET Onlinestatus = '1' WHERE username = '{Name}'";
            
            if (!DBConnection.ExecuteNonQuery(sqlupdate))
            {
                // Kein Benutzer 
                throw new ApplicationException("Bei der Datenbankabfrage ist ein Fehler aufgetreten!");
            }
                
            // ID aus Datenbank holen
            ID = GetIdFromDB();

            // IP-Adresse des PC ermitteln
            IP = GetIPAdressFromPC();

            // Kontaktliste initilisieren
            InitKontaktList();
        }

        /// <summary>
        /// Loggt den Benutzer aus und setzt den Onlinestatus in der Datenbank auf 0
        /// </summary>
        public static void Logout()
        {
            // SQL-UPDATE-Befehl um den Onlinestatus in der Datenbank beim Ausloggen von 1 auf 0 zu setzen
            string sqlupdate = $"UPDATE USERS SET Onlinestatus = '0' WHERE username = '{Name}'";

            if (!DBConnection.ExecuteNonQuery(sqlupdate))
            {
                // Fehler beim updaten
                throw new ApplicationException("Logout ist fehlgeschlagen!");
            }

            // Properties zurücksetzen
            ResetProperties();
        }

        /// <summary>
        /// Registriert einen neuen Benutzer in der Datenbank
        /// </summary>
        /// <param name="_benutzername">Name des neuen Benutzers</param>
        /// <param name="_passwort">Passwort des neuen Benutzers</param>
        public static void Regist(string _benutzername, string _passwort)
        {
            // variablen setzen
            string benutzername = _benutzername;
            string passwort = _passwort;

            // Benutzername oder Passwort leer?
            if (benutzername == "" || passwort == "")
            {
                throw new FormatException("Keine leeren Eingaben erlaubt!");
            }

            List<Dictionary<string, string>> select;

            // SQL-SELECT-Befehl um zu prüfen ob der Benutzername noch frei ist
            string sqlselect = $"SELECT Username FROM Users " +
                               $"WHERE Username = '{benutzername}'";

            // SQL-SELECT-Befehl um den neuen Benutzer in der Datenbank abzuspeichern
            string sqlinsert = $"INSERT INTO USERS (Username, Password) " +
                               $"VALUES ('{benutzername}', '{passwort}')";

            select = DBConnection.ExecuteSELECT(sqlselect);

            if (select == null)
            {
                // Benutzer noch nicht vorhaden -> in Datenbank speichern
                if (!DBConnection.ExecuteNonQuery(sqlinsert))
                {
                    // Fehler beim einfügen
                    throw new ApplicationException("Bei der Datenbankabfrage ist ein Fehler aufgetreten!");
                }
            }
            else
            {
                // Benutzer bereits vorhanden
                throw new ApplicationException("Der Benutzer existiert bereits!");
            }
        }

        /// <summary>
        /// Fügt einen Benutzer zu den Kontakten hinzu
        /// </summary>
        /// <param name="_kontakt">Kontakt an den die Anfrage gesendet werden soll</param>
        public static void SendRequest(Kontakt _kontakt)
        {
            // variablen setzen
            Kontakt kontakt = _kontakt;

            // Kontakt bereits in Kontaktliste
            if (Kontakte.Contains(kontakt))
            {
                throw new ApplicationException("Der Kontakt ist bereits in ihrer Kontaktliste!");
            }

            // SQL-INSERT-Befehl um die Freundschaft in die Datenbank zu schreiben
            string sqlsinsert = $"INSERT INTO KONTAKTE (`User1id`, `User2id`, `freunde`) VALUES ({ID}, {kontakt.ID}, 1)";

            if (DBConnection.ExecuteNonQuery(sqlsinsert))
            {
                // erfolgreich -> zu Kontaktliste hinzufügen
                kontakte.Add(kontakt);
            }
            else
            {
                // Fehler beim einfügen
                throw new ApplicationException("Bei der Datenbankabfrage ist ein Fehler aufgetreten!");
            }
        }

        /// <summary>
        /// Löscht einen Kontakt aus der Liste
        /// </summary>
        /// <param name="_kontakt"></param>
        public static void DeleteKontakt(Kontakt _kontakt)
        {
            // variablen setzen
            Kontakt kontakt = _kontakt;

            //SQL-DELETE-Befehl um die ausgewählte Freundschaft aus der Datenbank zu löschen
            string sqldelete = $"DELETE FROM KONTAKTE WHERE User1id = {ID} AND User2id = {kontakt.ID}";

            if (DBConnection.ExecuteNonQuery(sqldelete))
            {
                // erfolgreich gelöscht
                kontakte.Remove(kontakt);
            }
            else
            {
                // Fehler beim löschen
                throw new ApplicationException("Bei der Datenbankabfrage ist ein Fehler aufgetreten!");
            }
        }

        /// <summary>
        /// Aktualisiert die Kontakt-Liste des Benutzers
        /// </summary>
        public static void UpdateKontakte()
        {
            // variablen setzen
            kontakte = new List<Kontakt>();
            List<Dictionary<string, string>> select;

            // SQL-SELECT-Befehl um die ID aller Kontakte zu finden die mit dem Benutzer befreundet sind
            string sqlselect = $"SELECT User2id FROM KONTAKTE WHERE User1id = {ID} AND freunde = 1";

            select = DBConnection.ExecuteSELECT(sqlselect);

            if (select != null)
            {
                // Kontakte vorhanden
                for (int i = 0; i < select.Count; i++)
                {
                    Kontakt kontakt = new Kontakt(select[i]["User2id"]);

                    // Prüfen ob sich bei den Kontakten geändert hat und diese Änderungen in die Kontaktliste übernehmen
                    if (!kontakte.Contains(kontakt))
                    {
                        kontakte.Add(kontakt);
                    }
                    else
                    {
                        for (int j = 0; j < kontakte.Count; j++)
                        {
                            if (kontakte[j].ID == kontakt.ID && (kontakte[j].Onlinestatus != kontakt.Onlinestatus || kontakte[j].IP != kontakt.IP))
                            {
                                kontakte.RemoveAt(j);
                                kontakte.Insert(j, kontakt);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gibt den Chatverlauf des Benutzers mit dem gewählten Kontakt zurück
        /// </summary>
        /// <param name="_kontakt">Kontakt-Objekt des gewünschten Kontakts</param>
        /// <returns>Liste-Objekt vom Typ Nachricht in dem die Nachrichten enthalten sind</returns>
        public static List<Nachricht> GetChatHistory(Kontakt _kontakt)
        {
            // variablen setzen
            Kontakt kontakt = _kontakt;

            List<Nachricht> chats = new List<Nachricht>();
            List<Dictionary<string, string>> select;

            Nachricht msg;

            // SQL-SELECT-Befehl um alle Nachrichten die der Benutzer und der Kontakt ausgetauscht haben zu finden
            string sqlselect = $"SELECT * FROM Chats WHERE (`From` = '{Name}' AND `To` = '{kontakt.Name}') OR (`To` = '{Name}' AND `From` = '{kontakt.Name}') ORDER BY `TimeStamp` ASC";

            select = DBConnection.ExecuteSELECT(sqlselect);

            if (select == null)
            {
                // Keine Nachrichten vorhanden
                return null;
            }

            for (int i = 0; i < select.Count; i++)
            {
                // Nachrichten vorhanden -> in Liste einfügen und zurückgeben
                if (select[i]["OwnerID"] == ID)
                {
                    if (select[i]["From"] == Name)
                    {
                        msg = new Nachricht(select[i]["Content"], new Kontakt(ID), kontakt, select[i]["TimeStamp"]);

                        chats.Add(msg);
                    }
                    else
                    {
                        msg = new Nachricht(select[i]["Content"], kontakt, new Kontakt(ID), select[i]["TimeStamp"]);

                        chats.Add(msg);
                    }
                }
            }

            return chats;
        }
        #endregion // Public
        #region Private
        /// <summary>
        /// Holt die ID des Benutzers anhand des Benutzernamens aus der Datenbank
        /// </summary>
        /// <returns>ID des Benutzers</returns>
        private static string GetIdFromDB()
        {
            // variablen setzen
            List<Dictionary<string, string>> select;

            //SQL-SELECT-Befehl um die ID des Benutzers anhand des Namens aus der Datenbank zu holen
            string sqlselect = $"SELECT ID FROM USERS WHERE USERNAME = '{Name}'";

            select = DBConnection.ExecuteSELECT(sqlselect);

            if (select == null)
            {
                // ID nicht gefunden
                throw new ApplicationException("Der Benutzer existiert nicht!");
            }

            // ID gefunden
            return select[0]["ID"];
        }

        /// <summary>
        /// Ermittelt die IP-Adresse des PCs
        /// </summary>
        /// <returns>IP-Adresse des PC's</returns>
        private static string GetIPAdressFromPC()
        {
            // Anhand des Hosts die IP-Adressen des Pcs herausfinden
            IPAddress[] ip = Dns.GetHostAddresses(Dns.GetHostName());
            
            for (int i = 0; i < ip.Length; i++)
            {
                // Prüfen ob die IP-Adresse eine IPv4-Adresse ist, damit kann man davon ausgehen das dies die Richtige ist
                if (ip[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip[i].ToString();
                }
            }

            // Kein Netzwerkadapter gefunden bzw. keine IPv4-Adresse vorhanden
            throw new ApplicationException("IP-Adesse konnte nicht ermittelt werden!");
        }

        /// <summary>
        /// Initialisiert die Kontakliste des Benutzers, wenn kein Kontakt vorhanden ist bleibt die Kontaktliste null
        /// </summary>
        private static void InitKontaktList()
        {
            // variablen setzen
            kontakte = new List<Kontakt>();
            List<Dictionary<string, string>> select;

            // SQL-SELECT-Befehl um die ID aller Kontakte zu finden die mit dem Benutzer befreundet sind
            string sqlselect = $"SELECT User2id FROM KONTAKTE WHERE User1id = {ID} AND freunde = 1";

            select = DBConnection.ExecuteSELECT(sqlselect);

            if (select != null)
            {
                // Kontakte vorhanden -> in kontakte einfügen
                for (int i = 0; i < select.Count; i++)
                {
                    Kontakt kontakt = new Kontakt(select[i]["User2id"]);

                    kontakte.Add(kontakt);
                }
            }
        }

        /// <summary>
        /// Setzt die Properties des Benutzers auf null
        /// </summary>
        private static void ResetProperties()
        {
            isResetingProperties = true;

            //Alle Properties und Kontaktliste auf null setzen
            name = null;
            kontakte = new List<Kontakt>();
            ip = null;
            ID = null;
            Passwort = null;
        }
        #endregion // Private
        #endregion // Methods

        #region Properties
        /// <summary>
        /// Setzt/Gibt den Benutzernamen zurück
        /// </summary>
        public static string Name
        {
            get
            {
                return name;
            }

            set
            {
                // Prüfen ob die Properties zurückgesetzt werden
                if (isResetingProperties)
                {
                    name = value;
                }

                // Name darf nicht leer sein
                if (value == "")
                {
                    throw new FormatException("Benutzername darf nicht leer sein!");
                }

                // SQL-SELECT-Befehl zum prüfen ob der Benutzer existiert
                List<Dictionary<string, string>> select;
                
                string sqlselect = $"SELECT Username FROM USERS WHERE Username = '{value}'";

                select = DBConnection.ExecuteSELECT(sqlselect);
                
                if (select != null)
                {
                    // Benutzer existiert
                    name = value;
                }
                else
                {
                    // Benutzer existiert nicht
                    throw new ApplicationException("Passwort/Benutzername falsch!");
                }
            }
        }
        
        /// <summary>
        /// Gibt ein Kontakte-Array zurück, dass alle Kontakte des Benutzers enthält
        /// </summary>
        public static List<Kontakt> Kontakte
        {
            get
            {
                // Kontaktliste nach Namen sortieren
                kontakte.Sort((x, y) => string.Compare(x.Name, y.Name));

                return kontakte;
            }
        }
        
        /// <summary>
        /// Setzt/Gibt die IP-Adresse des Benutzers zurück
        /// </summary>
        public static string IP
        {
            get
            {
                return ip;
            }

            set
            {
                // Prüfen ob die Properties zurückgesetzt werden
                if (isResetingProperties)
                {
                    ip = value;
                    return;
                }

                // SQL-INSERT-Befehl um IP-Adresse des PCs in die Datenbank zu speichern
                string sqlinsert = $"UPDATE USERS SET `IP - Adresse`='{value}' WHERE Username='{Name}'";

                if (DBConnection.ExecuteNonQuery(sqlinsert))
                {
                    // IP-Adresse erfolgreich in Datenbank gespeichert
                    ip = value;
                }
                else
                {
                    // Fehler beim speichern der IP-Adresse
                    throw new ApplicationException("Bei der Datenbankabfrage ist ein Fehler aufgetreten!");
                }
            }
        }

        /// <summary>
        /// Setzt/Gibt die ID des Benutzers zurück
        /// </summary>
        public static string ID
        {
            get;
            set;
        }

        /// <summary>
        /// Setzt/Gibt das Passwort des Benutzers zurück
        /// </summary>
        private static string Passwort
        {
            get
            {
                return passwort;
            }

            set
            {
                // Prüfen ob die Properties zurückgesetzt werden
                if (isResetingProperties)
                {
                    passwort = value;
                    return;
                }

                // SQL-SELECT-Befehl zum prüfen ob das Passwort stimmt
                string sqlselect = $"SELECT Password FROM USERS WHERE username = '{Name}'";

                List<Dictionary<string, string>> select = DBConnection.ExecuteSELECT(sqlselect);

                if (select[0]["Password"] == value)
                {
                    // Passwort stimmt
                    passwort = value;
                }
                else
                {
                    // Passwort falsch
                    throw new ApplicationException("Passwort/Benutzername falsch!");
                }
            }
        }
        #endregion // Properties
    }
}