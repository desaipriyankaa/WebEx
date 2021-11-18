using System.Windows;
using System.Windows.Input;
using WebEx_ChatHistory_Viewer;
using System.Windows.Forms;
using Service.Library;
using WinForms = System.Windows;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Media;

namespace ChatHistory.Viewer
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        static Services _services = new Services(new JsonDataSource());
        Validation _validation = new Validation();
        LoginCredentialData _loginCredential = new LoginCredentialData();
        string _mainwindowBasePath;

        public string BrowseFullPath { get; set; }

        public ConfigurationWindow()
        {
            InitializeComponent();

            _loginCredential.ReadData();
            if (_loginCredential.EmailID != null)
            {
                inputEmail.Text = _loginCredential.EmailID;
                BrowseText.Text = _loginCredential.BrowsePath;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ConfigurationWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (inputEmail.Text == _loginCredential.EmailID)
            {
                _loginCredential.SaveData();
                MainWindow main = new MainWindow()
                {
                    BasePath = _mainwindowBasePath
                };
                main.myFolders.Items.Clear();
                main.DisplayData();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please enter correct information ...!!! ");
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == WinForms.Forms.DialogResult.OK)
            {
                    _mainwindowBasePath = folderBrowser.SelectedPath;
                    BrowseText.Text = _mainwindowBasePath;
                    BrowseFullPath = BrowseText.Text;
                    _loginCredential.BrowsePath = BrowseFullPath;
            }
        }

        private void InputEmail_LostFocus(object sender, RoutedEventArgs e)
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

        private void BrowseText_TextChanged(object sender, WinForms.Controls.TextChangedEventArgs e)
        {
            bool CheckValid = _validation.isValidBrowsePath(BrowseText.Text.ToString());
            if (CheckValid)
            {
                browseError.Foreground = Brushes.SkyBlue;
                browseError.Content = "Correct Browsepath";
                BrowseText.BorderBrush = Brushes.Black;
                _loginCredential.BrowsePath = BrowseText.Text;
            }
            else
            {
                MessageBox.Show($"Invalid input..... please enter correct Browsepath which contains {"WebexDump"} folder !!");
                BrowseText.BorderBrush = Brushes.Red;
                browseError.Foreground = Brushes.Red;
                browseError.Content = "**Browsepath is mandatory ..!";
                BrowseText.Text = string.Empty;
            }
        }
    }
}
