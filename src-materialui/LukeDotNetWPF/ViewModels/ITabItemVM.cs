using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LukeDotNetWPF.ViewModels
{
    public interface ITabItemVM
    {
        string TabName { get; set; }

        ICommand CloseCommand { get; set; }
    }
}
