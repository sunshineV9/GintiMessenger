using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Odbc;
using System.Data;

namespace ChatProgramm
{
    [Serializable()]
    public class Kontakt
    {
        #region Constructor
        /// <summary>
        /// Erstellt anhand der ID ein neues Kontakt-Objekt, dass die Properties aus der Datenbank lädt
        /// </summary>
        /// <param name="_id">ID des Kontakts</param>
        public Kontakt(string _id)
        {
            // variablen setzen
            ID = _id;
            
            InitKontakt();
        }
        #endregion // Constructor
        
        #region Methods
        /// <summary>
        /// Holt anhand der ID des Kontaktobjekts alle relevanten Daten aus der Datenbank
        /// </summary>
        private void InitKontakt()
        {
            // variablen setzen
            List<Dictionary<string, string>> select;

            // SELECT-Befehl um die Daten des Kontakts aus der Datenbank zu holen
            string sqlselect = $"SELECT * FROM USERS WHERE ID = {ID}";

            select = DBConnection.ExecuteSELECT(sqlselect);

            if (select != null)
            {
                // Daten in Properties speichern
                Name = select[0]["Username"];
                Onlinestatus = Convert.ToBoolean(Convert.ToInt32(select[0]["Onlinestatus"]));
                IP = select[0]["IP - Adresse"];
            }
            else
            {
                // Kontakt nicht gefunden
                throw new ApplicationException("Kontakt existiert nicht!");
            }
        }

        /// <summary>
        /// Selektiert anhand des Kontaktnamen einen Kontakt aus der Datenbank
        /// </summary>
        /// <param name="_kontaktName">Name des Kontaktes der selektiert werden soll</param>
        /// <returns>Kontakt-Objekt des Kontaktes</returns>
        public static Kontakt SelectKontaktFromDB(string _kontaktname)
        {
            // variablen setzen
            string kontaktname = _kontaktname;

            List<Dictionary<string, string>> select;

            // SELECT-Befehl um die ID eines Users aus der Datenbank zu holen
            string sqlselect = $"SELECT ID FROM USERS WHERE Username = '{kontaktname}'";

            select = DBConnection.ExecuteSELECT(sqlselect);

            if (select != null)
            {
                // User gefunden -> ID zurückgeben
                return new Kontakt(select[0]["ID"]);
            }
            else
            {
                // User nicht gefunden
                throw new ApplicationException("Der gesuchte Kontakt existiert nicht!");
            }
        }

        /// <summary>
        /// Überschriebene Equals-Methode die auf die Gleichheit von 2 Kontakt-Objekte prüft
        /// </summary>
        /// <param name="_obj">Objekt das geprüft werden soll</param>
        /// <returns>TRUE wenn die Objekte gleich sind, ansonsten FALSE</returns>
        public override bool Equals(object _obj)
        {
            object obj = _obj;

            if (obj.GetType() != typeof(Kontakt))
            {
                return false;
            }

            if (obj == null)
            {
                return false;
            }
            
            if (ID != ((Kontakt)obj).ID)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Überschreibt die GetHashCode-Methode um Compiler-Warnungen zu beheben durch die Überschreibung der Equals-Methode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion // Methods

        #region Properties
        /// <summary>
        /// Setzt/Gibt die ID des Kontaktes zurück
        /// </summary>
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// Setzt/Gibt den Namen des Kontaktes zurück
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Setzt/Gibt den Onlinestatus des Kontaktes zurück
        /// </summary>
        public bool Onlinestatus
        {
            get;
            set;
        }

        /// <summary>
        /// Setzt/Gibt die IP-Adresse des Kontakts zurück
        /// </summary>
        public string IP
        {
            get;
            set;
        }
        #endregion // Properties
    }
}
