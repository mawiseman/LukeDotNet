using LukeDotNetWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LukeDotNetWPF.ViewModels
{
    public class StartPageVM : ITabItemVM
    {
         public string TabName { get; set; }
        public List<LukeIndex> RecentIndexes { get; set; }

        public ICommand CloseCommand { get; set; }
        public ICommand OpenNewIndexCommand { get; set; }
        public ICommand OpenIndexCommand { get; set; }

        public StartPageVM()
        {
            //TabId = Guid.NewGuid();
        }
    }
}
