using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LukeDotNetWPF.Helper;
using System.ComponentModel;

namespace LukeDotNetWPF.Models
{
    public class LukeIndex : INotifyPropertyChanged
    {
        private string path;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public string IndexName
        {
            get
            {
                return "get the name from the path";
            }
        }

        public string IndexPath
        {
            get { return path; }
            set
            {
                if (path != value)
                {
                    path = value;
                    RaisePropertyChanged("Path");
                }
            }
        }
        
        
        public LukeIndex(string path)
        {
            IndexPath = path;
        }
    }
}
