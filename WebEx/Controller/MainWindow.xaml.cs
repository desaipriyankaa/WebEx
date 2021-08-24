using System;
using System.Collections.Generic;
using System.IO;
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

//--<using>--
using Microsoft.Win32;
using System.Windows.Forms;
using WinForms = System.Windows;
using System.Diagnostics;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Path = System.IO.Path;
//--</using>--


namespace WebEx
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == WinForms.Forms.DialogResult.OK)
            {
                myFolders.Items.Clear();
                string[] files = Directory.GetFiles(FBD.SelectedPath);
                string[] dirs = Directory.GetDirectories(FBD.SelectedPath);

                foreach (string file in files)
                {
                    myFolders.Items.Add(Path.GetFileName(file));
                }

                foreach (string dir in dirs)
                {
                    myFolders.Items.Add(Path.GetFileName(dir));
                }
            }
        }      
    }
}
