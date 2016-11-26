using LukeDotNetWPF.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LukeDotNetWPF.Core.Repositories
{
    public class PreferenceRepository
    {
        private const string LUKE_PREFS_FILE = ".luke";

        private string PreferencesPath
        {
            get
            {
                FileInfo file = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string HOME_DIR = file.DirectoryName;
                if (!HOME_DIR.EndsWith("\\"))
                    HOME_DIR += "\\";

                return HOME_DIR + "/" + LUKE_PREFS_FILE;
            }
        }

        public Preferences Load()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Preferences));

                var pd = (Preferences)serializer.Deserialize(new StreamReader(this.PreferencesPath));

                if (pd.MruList == null)
                    pd.MruList = new ArrayList();

                if (pd.MruList.Count > pd.MruMaxSize)
                    pd.MruList.RemoveRange(pd.MruMaxSize, pd.MruList.Count - pd.MruMaxSize);

                return pd;
            }
            catch (Exception)
            {
                // not found or corrupted, keep defaults
                return new Preferences();
            }
        }

        public void Save(Preferences preferences)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Preferences));
            serializer.Serialize(new StreamWriter(this.PreferencesPath), preferences);
        }
    }

    
}
