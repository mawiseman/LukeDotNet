using LukeDotNetWPF.Core.Service;
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
    public class LukeDotNetVM : INotifyPropertyChanged
    {
        public List<LukeIndex> RecentIndexes { get; set; }

        public ObservableCollection<ITabItemVM> Tabs { get; private set; }

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged("SelectedTabIndex");
            }
        }

        public LukeDotNetVM()
        {
            LoadRecentIndexes();
            InitialiseTabs();
        }
        
        private void InitialiseTabs()
        {
            Tabs = new ObservableCollection<ITabItemVM>();

            DoShowStartPage(this);

            SelectedTabIndex = 0;
        }

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

        private void OpenLuceneIndex(string indexPath)
        {
            // check if this index is already open

            var lukeIndexTab = Tabs.Where(
                    t => typeof(LuceneIndexVM) == t.GetType()
                        && ((LuceneIndexVM)t).LukeIndex.IndexPath == indexPath)
                .FirstOrDefault();

            // if its open, focus the tab

            if (lukeIndexTab != null)
            {
                SelectedTabIndex = Tabs.IndexOf(lukeIndexTab);
            }
            else
            {
                // open a new index

                var luceneIndex = new LuceneIndexVM(indexPath);
                luceneIndex.CloseCommand = this.CloseTabCommand(luceneIndex);

                Tabs.Add(luceneIndex);
                SelectedTabIndex = Tabs.Count - 1;
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }


        private ICommand CloseTabCommand(ITabItemVM tabitemVM)
        {
            return new SimpleCommand
            {
                CanExecuteDelegate = x => true,
                ExecuteDelegate = x => { this.DoCloseTabCommand(tabitemVM); }
            };
        }
        protected virtual void DoCloseTabCommand(ITabItemVM tabitemVM)
        {
            var tabToClose = Tabs.Where(t => t.TabName == tabitemVM.TabName).FirstOrDefault();

            if (tabToClose != null)
                Tabs.Remove(tabToClose);
        }
        

        private ICommand _openNewIndexCommand;
        public ICommand OpenNewIndexCommand
        {
            get
            {
                return this._openNewIndexCommand ?? (this._openNewIndexCommand = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = x => { this.DoOpenNewIndexCommand(x); }
                });
            }
        }
        protected virtual void DoOpenNewIndexCommand(object sender)
        {
            var fbd = new Ookii.Dialogs.VistaFolderBrowserDialog();
            var result = fbd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
                OpenLuceneIndex(fbd.SelectedPath);
        }


        private ICommand _openIndexCommand;
        public ICommand OpenIndexCommand
        {
            get
            {
                return this._openIndexCommand ?? (this._openIndexCommand = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = x => { this.DoOpenIndexCommand(x); }
                });
            }
        }
        protected virtual void DoOpenIndexCommand(object sender)
        {
            LukeIndex lukeIndex = (LukeIndex)sender;

            OpenLuceneIndex(lukeIndex.IndexPath);
        }
        

        private ICommand _showStartPage;
        public ICommand ShowStartPage
        {
            get
            {
                return this._showStartPage ?? (this._showStartPage = new SimpleCommand
                {
                    CanExecuteDelegate = x => !IsStartPageVisibile(),
                    ExecuteDelegate = x => { this.DoShowStartPage(x); }
                });
            }
        }
        protected virtual void DoShowStartPage(object sender)
        {
            if (IsStartPageVisibile())
                return;
            
            var startPage = new StartPageVM()
            {
                RecentIndexes = this.RecentIndexes,
                OpenNewIndexCommand = this.OpenNewIndexCommand,
                OpenIndexCommand = this.OpenIndexCommand
            };

            startPage.CloseCommand = this.CloseTabCommand(startPage);

            Tabs.Insert(0, startPage);
            SelectedTabIndex = 0;
        }
        private bool IsStartPageVisibile()
        {
            return Tabs.Where(t => typeof(StartPageVM) == t.GetType()).Any();
        }


        private ICommand _exitAppCommand;
        public ICommand ExitAppCommand
        {
            get
            {
                return this._exitAppCommand ?? (this._exitAppCommand = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = x => { this.DoExitAppCommand(x); }
                });
            }
        }
        protected virtual void DoExitAppCommand(object sender)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
