using LukeDotNetWPF.Core.Service;
using LukeDotNetWPF.Helper;
using LukeDotNetWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LukeDotNetWPF.ViewModels
{
    public class LukeDotNetVM
    {
        public LukeDotNetVM()
        {
            LoadRecentIndexes();
        }

        public List<LukeIndex> RecentIndexes { get; set; }

        private void LoadRecentIndexes()
        {
            LukeService lukeService = new LukeService();

            var recentIndexes = lukeService.GetRecentIndexes();

            recentIndexes.Add(@"C:\Data\Development\sitecore_master_index");
            recentIndexes.Add(@"C:\Data\Development\sitecore_web_index");

            RecentIndexes = new List<LukeIndex>();
            foreach (string recentIndex in recentIndexes)
            {
                RecentIndexes.Add(new LukeIndex(recentIndex));
            }
        }
        
        
        private ICommand _openNewIndex;
        public ICommand OpenNewIndex
        {
            get
            {
                return this._openNewIndex ?? (this._openNewIndex = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = x => { this.DoOpenNewIndex(x); }
                });
            }
        }
        protected virtual void DoOpenNewIndex(object sender)
        {
            var fbd = new Ookii.Dialogs.VistaFolderBrowserDialog();
            var result = fbd.ShowDialog();
        }


        private ICommand _openIndex;
        public ICommand OpenIndex
        {
            get
            {
                return this._openIndex ?? (this._openIndex = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = x => { this.DoOpenIndex(x); }
                });
            }
        }
        protected virtual void DoOpenIndex(object sender)
        {
            //open the index
            string s = "";
        }
        

        private ICommand _showStartPage;
        public ICommand ShowStartPage
        {
            get
            {
                return this._showStartPage ?? (this._showStartPage = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = x => { this.DoShowStartPage(x); }
                });
            }
        }
        protected virtual void DoShowStartPage(object sender)
        {
            //open the index
            string s = "";
        }


        private ICommand _exitApp;
        public ICommand ExitApp
        {
            get
            {
                return this._exitApp ?? (this._exitApp = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = x => { this.DoExitApp(x); }
                });
            }
        }
        protected virtual void DoExitApp(object sender)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
