using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace ChatProgramm
{
    static class AppSettings
    {
        /// <summary>
        /// Gibt die Value des angegebenen App.config Eintrags zurück
        /// </summary>
        /// <param name="_key">Keyname des App.config Eintrags</param>
        /// <returns>Value die zu dem Key gehört</returns>
        public static string getAppSetting(string _key)
        {
            string key = _key;

            //Laden der AppSettings
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);

            //Zurückgeben der dem Key zugehörigen Value
            return config.AppSettings.Settings[key].Value;
        }

        /// <summary>
        /// Setzt die Value des angegebenen App.config Eintrags oder Erstellt einen neuen Eintrag und speichert diesen
        /// </summary>
        /// <param name="key">Keyname des App.config Eintrags</param>
        /// <param name="value">Value des Eintrags</param>
        public static void setAppSetting(string _key, string _value)
        {
            string key = _key;
            string value = _value;

            //Laden der AppSettings
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);

            //Überprüfen ob Key existiert
            if (config.AppSettings.Settings[key] != null)
            {
                //Key existiert. Löschen des Keys zum "überschreiben"
                config.AppSettings.Settings.Remove(key);
            }
            //Anlegen eines neuen KeyValue-Paars
            config.AppSettings.Settings.Add(key, value);
            //Speichern der aktualisierten AppSettings
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
