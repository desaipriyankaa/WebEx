using System.Windows;
using System.Windows.Input;
using WebEx_ChatHistory_Viewer;
using System.Windows.Forms;
using Service.Library;
using Path = System.IO.Path;
using WinForms = System.Windows;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Media;

namespace ChatHistory.Viewer
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        static Services _services = new Services(new JsonDataSource());
        Validation _validation = new Validation();
        MainWindow _mainWindow = new MainWindow();
        LoginCredentialData _loginCredential = new LoginCredentialData();

        public string BrowseFullPath { get; set; }

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

        private void LoginClose_Click(object sender, RoutedEventArgs e)
        {
            WinForms.Application.Current.Shutdown();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //if (inputEmail.Text.Length == 0 || BrowseText.Text.Length == 0)
            //{
            //    MessageBox.Show("Enter correct Credentials ...!!! ");
            //}
            //else
            //{
            //    _loginCredential.SaveData();
            //    _mainWindow.Show();
            //    this.Hide();
            //}
            if (inputEmail.Text == "Sanket.Naik@klingelnberg.com")
            {
                _loginCredential.SaveData();
                _mainWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Enter correct Credentials ...!!! ");
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == WinForms.Forms.DialogResult.OK)
            {
               
                _mainWindow.myFolders.Items.Clear();
                _mainWindow.BasePath = folderBrowser.SelectedPath;
                BrowseText.Text = _mainWindow.BasePath;
                BrowseFullPath = BrowseText.Text;
                _mainWindow.DisplayData();
             
                _loginCredential.BrowsePath = BrowseFullPath;
            }
        }

        private void inputEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            bool CheckValid = _validation.isValidEmail(inputEmail.Text.ToString());

            if (CheckValid)
            {
                lblError.Foreground = Brushes.SkyBlue;
                lblError.Content = "Correct Email";
                inputEmail.BorderBrush = Brushes.Black;
                _loginCredential.EmailID = inputEmail.Text;
            }
            else
            {
                MessageBox.Show("Invalid input..... please enter correct Email !!");
                inputEmail.BorderBrush = Brushes.Red;
                lblError.Foreground = Brushes.Red;
                lblError.Content = "**Email is mandatory ..!";
                inputEmail.Text = string.Empty;
            }
        }

    }
}
