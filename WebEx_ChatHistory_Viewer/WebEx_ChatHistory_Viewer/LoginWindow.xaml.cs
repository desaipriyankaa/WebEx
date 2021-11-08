using System.Windows;
using System.Windows.Input;
using WebEx_ChatHistory_Viewer;
using System.Windows.Forms;
using Service.Library;
using Path = System.IO.Path;
using WinForms = System.Windows;

namespace ChatHistory.Viewer
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        static Services _services = new Services(new JsonDataSource());
        
        MainWindow mainWindow = new MainWindow();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Hide();   
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == WinForms.Forms.DialogResult.OK)
            {
                mainWindow.myFolders.Items.Clear();
                mainWindow.BasePath = folderBrowser.SelectedPath;
                BrowseText.Text = mainWindow.BasePath;

                string[] dirs = _services.ReadUserName(mainWindow.BasePath);

                foreach (string dir in dirs)
                {
                    mainWindow.myFolders.Items.Add(Path.GetFileName(dir));
                }
            }
        }
    }
}
