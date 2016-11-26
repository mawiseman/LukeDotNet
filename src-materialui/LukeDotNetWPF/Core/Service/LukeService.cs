using LukeDotNetWPF.Core.Models;
using LukeDotNetWPF.Core.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LukeDotNetWPF.Core.Service
{
    public class LukeService
    {
        private PreferenceRepository _preferenceRepository;

        public LukeService()
        {
            _preferenceRepository = new PreferenceRepository();
        }


        #region Preferences
        
        public ArrayList GetRecentIndexes()
        {
            var preferences = _preferenceRepository.Load();
            return preferences.MruList;
        }

        #endregion
    }
}
