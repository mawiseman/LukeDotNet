using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LukeDotNetWPF.ViewModels
{
    public class IndexLinkButton
    {
        public string Content { get; private set; }

        public string ToolTip { get; private set; }

        public IndexLinkButton(string content, string tooltip)
        {
            Content = content;
            ToolTip = tooltip;
        }

        public ICommand OpenIndexCommand
        {
            get { return new DelegateCommand(OpenIndex); }
        }

        private void OpenIndex()
        {
            //open Content
        }
    }
}
