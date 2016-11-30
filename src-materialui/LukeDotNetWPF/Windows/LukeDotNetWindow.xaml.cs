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
        }

        #region Events

        public void OpenIndex_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new Ookii.Dialogs.VistaFolderBrowserDialog();

            var result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                //string[] files = System.IO.Directory.GetFiles(fbd.SelectedPath);

                //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
            }
        }

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
