using LukeDotNetWPF.Core.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LukeDotNetWPF.ViewModels
{
    public class StartPage : ObservableObject
    {
        private LukeService _lukeService = new LukeService();

        private readonly ObservableCollection<IndexLinkButton> _recent = new ObservableCollection<IndexLinkButton>();

        public IEnumerable<IndexLinkButton> Recent
        {
            get { return _recent; }
        }

        public StartPage()
        {
            LoadRecentIndexes();
        }

        public ICommand OpenIndexCommand
        {
            get { return new DelegateCommand(OpenIndex); }
        }

        private void OpenIndex()
        {
            var fbd = new Ookii.Dialogs.VistaFolderBrowserDialog();

            var result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                //string[] files = System.IO.Directory.GetFiles(fbd.SelectedPath);

                //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
            }
        }

        private void LoadRecentIndexes()
        {
            //spRecentIndexes.Children.Clear();

            var recentIndexes = _lukeService.GetRecentIndexes();

            recentIndexes.Add("test 1");
            recentIndexes.Add("test 2");
            
            foreach (string recentIndex in recentIndexes)
            {
                _recent.Add(new IndexLinkButton(recentIndex, @"c:\xxx\yyy\zzz"));
            }
        }
        
    }
}
