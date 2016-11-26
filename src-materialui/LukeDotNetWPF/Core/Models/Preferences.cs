using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LukeDotNetWPF.Core.Models
{
    public class Preferences
    {
        public string LastPwd;

        public ArrayList MruList;

        [DefaultValue(10)]
        public int MruMaxSize;

        [DefaultValue(true)]
        public bool UseCompound;

        public Preferences()
        {
            MruMaxSize = 10;
            UseCompound = true;
            MruList = new ArrayList();
        }
    }
}
