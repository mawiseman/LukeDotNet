using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.IO;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace LukeDotNetWPF.Models
{
    public class LukeIndex
    {
        public string IndexPath { get; private set; }
        
        public string IndexName
        {
            get
            {
                return IndexPath.Split('\\').Last();
            }
        }

        public LukeIndex(string path)
        {
            IndexPath = path;

            
        }
    }
}
