using LukeDotNetWPF.Core.Service;
using LukeDotNetWPF.Helper;
using LukeDotNetWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LukeDotNetWPF.ViewModels
{
    public class LukeDotNetVM
    {
        public ICommand OpenIndexCommand { get; set; }
        public ICommand OpenNewIndexCommand { get; set; }

        public LukeDotNetVM()
        {
            LoadRecentIndexes();
            OpenNewIndexCommand = new DelegateCommand(OpenNewIndex);
            OpenIndexCommand = new DelegateCommand(OpenIndex);
        }

        public ObservableCollection<LukeIndex> RecentIndexes { get; set; }

        private void LoadRecentIndexes()
        {
            LukeService lukeService = new LukeService();

            var recentIndexes = lukeService.GetRecentIndexes();

            recentIndexes.Add(@"C:\Data\Development\sitecore_master_index");
            recentIndexes.Add(@"C:\Data\Development\sitecore_web_index");

            RecentIndexes = new ObservableCollection<LukeIndex>();
            foreach (string recentIndex in recentIndexes)
            {
                RecentIndexes.Add(new LukeIndex(recentIndex));
            }
        }

        private void OpenIndex()
        {
            
        }

        private void OpenNewIndex()
        {
            var fbd = new Ookii.Dialogs.VistaFolderBrowserDialog();

            var result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                //string[] files = System.IO.Directory.GetFiles(fbd.SelectedPath);

                //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
            }
        }
    }
}
