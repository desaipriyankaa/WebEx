using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Service.Library;
using Path = System.IO.Path;
using WinForms = System.Windows;

namespace WebEx_ChatHistory_Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Services _services = new Services(new JsonDataSource());

        private string BasePath { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// mouse movement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Screen Minimize event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Screen maximize and if double click then resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }


        /// <summary>
        /// Screen close event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WinForms.Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// click on browse button and folder dialog will open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Browse_button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == WinForms.Forms.DialogResult.OK)
            {
                myFolders.Items.Clear();
                BasePath = folderBrowser.SelectedPath;

                string[] dirs = _services.ReadUserName(BasePath);
                
                foreach (string dir in dirs)
                {
                    myFolders.Items.Add(Path.GetFileName(dir));
                }
            }
        }

        /// <summary>
        /// click on chat folder and can read chat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ChatItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectChat = myFolders.SelectedItem;
            if (selectChat != null)
            {
                string filename = Path.Join(BasePath, selectChat.ToString(), "messages.json");
                chatData.Text = _services.ReadUserChatData(filename);
            }
        }

        private void Data_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
            var selectChat = myFolders.SelectedItem;
            if (selectChat != null)
            {
                string filename = Path.Join(BasePath, selectChat.ToString(), "messages.json");
                chatData.Text = _services.ReadUserChatData(filename);
            }
        }

        private void Media_Button_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.InitialDirectory = @"F:\PProject\WPF\WebexDump\DirectChats";
            //var a = ofd.InitialDirectory;
            //ofd.Filter = "Image files (*.PNG)|*.PNG|All Files(*.*)|*.*";

            //ofd.RestoreDirectory = true;

            //if (ofd.ShowDialog() == WinForms.Forms.DialogResult.OK)
            //{
            //    string selectedFilename = ofd.FileName;
            //    BitmapImage bitmap = new BitmapImage();
            //    bitmap.BeginInit();
            //    bitmap.UriSource = new System.Uri(selectedFilename);
            //    bitmap.EndInit();
            //    ImageViewer1.Source = bitmap;
            //}

            var selectChat = myFolders.SelectedItem;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.PNG)|*.PNG|All Files(*.*)|*.*";
                      
            if (selectChat != null)
            {
                string filename = Path.Join(BasePath, selectChat.ToString());

                if (ofd.ShowDialog() == WinForms.Forms.DialogResult.OK)
                {
                    string selectedFilename = ofd.FileName;
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new System.Uri(selectedFilename);
                    bitmap.EndInit();
                    ImageViewer1.Source = bitmap;
                }
            }
        }

        private void chatData_Scroll(object sender, WinForms.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}

