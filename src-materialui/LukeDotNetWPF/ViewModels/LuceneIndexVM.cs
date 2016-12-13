using LukeDotNetWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LukeDotNetWPF.ViewModels
{
    public class LuceneIndexVM : ITabItemVM
    {
        public string TabName { get; set; }

        public ICommand CloseCommand { get; set; }

        public LukeIndex LukeIndex { get; set; }

        public LuceneIndexVM(string indexPath)
        {
            this.LukeIndex = new LukeIndex(indexPath);

            TabName = LukeIndex.IndexName;
        }
    }
}
