using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LukeDotNetWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LukeDotNetWindow : MetroWindow
    {
        public LukeDotNetWindow()
        {
            InitializeComponent();

            //this is th eonly way i could get binding to work correctly
            this.DataContext = new LukeDotNetWPF.ViewModels.LukeDotNetVM();
        }

        #region Events
        
        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
