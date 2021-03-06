using ChatHistory.Viewer;
using Service.Library;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Button = System.Windows.Controls.Button;
using Path = System.IO.Path;
using TextBlock = System.Windows.Controls.TextBox;
using WinForms = System.Windows;

namespace WebEx_ChatHistory_Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Services _services = new Services(new JsonDataSource());
        LoginCredentialData _loginCredential = new LoginCredentialData();
        JsonDataSource _dataSource = new JsonDataSource();


        public string BasePath { get; set; }
        object selectChat;

        public MainWindow()
        {
            InitializeComponent();
            _loginCredential.ReadData();
            DisplayData();
        }

        /// <summary>
        /// if BrowsePath exists then displays all data in myFolders(ListView) 
        /// </summary>
        public void DisplayData()
        {
            _loginCredential.ReadData();
            if (_loginCredential.BrowsePath != null)
            {
                myFolders.Items.Clear();
                string[] dirs = _services.ReadUserName(_loginCredential.BrowsePath);

                foreach (string dir in dirs)
                {
                    myFolders.Margin = new Thickness(10);
                    string[] folder = Directory.GetDirectories(dir);
                    foreach (var item1 in folder)
                    {
                        myFolders.Items.Add(Path.GetFileName(item1));
                    }
                }
            }
            else
            {
                this.Hide();
                ConfigurationWindow window = new ConfigurationWindow();
                window.Show();
            }
        }

        /// <summary>
        /// mouse movement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //WindowState = x.Border_MouseDown(sender, e);
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
            WinForms.Application.Current.Shutdown();
        }

        /// <summary>
        /// click on chat folder and can read chat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ChatItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var ChildStack = CreateNewStackPanelDynamically();
            ParentStack.Children.Add(ChildStack);
        }

        /// <summary>
        /// Create StackPanel dynamically
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>

        private StackPanel CreateNewStackPanelDynamically()
        {
            ParentStack.Children.Clear();
            string data = "";
            selectChat = myFolders.SelectedItem;
            StackPanel stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(200, 0, 200, 0);

            if (selectChat != null)
            {
                string[] dirs = _services.ReadUserName(_loginCredential.BrowsePath);
                int dirNumber = selectChat.ToString().Contains(',') ? 1 : 0;
                string filename = Path.Join(dirs[dirNumber], selectChat.ToString(), "messages.json");
                List<Messages> msg = _services.ReadUserChatData(filename);
                List<string> localImagesPath = GetImagesPath(Path.Join(dirs[dirNumber], selectChat.ToString()));

                foreach (var item in msg)
                {
                    JsonDataSource jsonData = new JsonDataSource();
                    string loginUserEmail = jsonData.SplitEmail(_loginCredential.EmailID);

                    if (item.PersonEmail != loginUserEmail)  //For Others chat
                    {
                        StackPanel stackPanel1 = GetStackPanel(Brushes.LightCyan, WinForms.HorizontalAlignment.Left);
                        data = AddingDataToStackPanel(data, stackPanel, localImagesPath, item, stackPanel1);
                    }
                    else  //for "SuccessfullLoggedUser"
                    {
                        StackPanel stackPanel2 = GetStackPanel(Brushes.LightBlue, WinForms.HorizontalAlignment.Right);
                        data = AddingDataToStackPanel(data, stackPanel, localImagesPath, item, stackPanel2);
                    }
                }
            }
            return stackPanel;
        }

        private string AddingDataToStackPanel(string data, StackPanel stackPanel, List<string> localImagesPath, Messages item, StackPanel stack)
        {
            if (item.Text != null)
            {
                data = item.PersonEmail + "     " + item.Created + " \n\n " + item.Text + " \n ";
            }
            else
            {
                data = item.PersonEmail + "     " + item.Created + " \n ";
            }

            TextBlock textBlock = GetTextBlock(data, HorizontalAlignment.Left);
            stack.Children.Add(textBlock);

            if (item.Files != null)  //Check for Image is present or not
            {
                foreach (string item1 in item.Files)
                {
                    string imgNameFromJSON = SplitFilesToFindImageName(item1);
                    List<string> imageFullName = localImagesPath.FindAll(i => i.Contains(imgNameFromJSON));
                    foreach (var singleImageName in imageFullName) //Add Image
                    {
                        Image image = GetImage(singleImageName);
                        Button button = new Button();
                        button.Content = image;
                        button.Background = Brushes.Transparent;
                        button.Height = 200;
                        stack.Children.Add(button);
                        button.Click += ZoomImage_Click;
                    }
                }
            }
            else
            {
                data = item.PersonEmail + "     " + item.Created + " \n\n " + item.Text + " \n ";
            }
            stackPanel.Children.Add(stack);
            return data;
        }

        private void ZoomImage_Click(object sender, RoutedEventArgs e)
        {
            ImgStack.Visibility = Visibility.Visible;
            Button srcButton = e.Source as Button;
            Image a = srcButton.Content as Image;
            ImageSource p = a.Source;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new System.Uri(p.ToString());
            bitmapImage.EndInit();
            Image image = new Image();
            image.Height = 500;
            image.Width = 800;
            image.Source = bitmapImage;
            StackPanel btnStack = CreateZoomImage();

            StackPanel stackPanel = new StackPanel();
            stackPanel.Width = 1200;
            stackPanel.Background = Brushes.LightGray;
            stackPanel.Orientation = WinForms.Controls.Orientation.Vertical;

            stackPanel.Children.Add(btnStack);
            stackPanel.Children.Add(image);
            frame.Navigate(stackPanel);

        }

        private StackPanel CreateZoomImage()
        {
            StackPanel btnStack = new StackPanel();
            btnStack.Orientation = WinForms.Controls.Orientation.Horizontal;
            Button button = new Button();
            button.HorizontalAlignment = WinForms.HorizontalAlignment.Right;
            button.Content = new MaterialDesignThemes.Wpf.PackIcon { Kind = MaterialDesignThemes.Wpf.PackIconKind.Close };


            button.FontSize = 20;
            button.Width = 50;
            button.Height = 50;
            button.Margin = new Thickness(1050, 10, 10, 10);
            button.Click += ZoomImageCloseButton_Click;
            btnStack.Children.Add(button);
            return btnStack;
        }

        private void ZoomImageCloseButton_Click(object sender, RoutedEventArgs e)
        {
            ImgStack.Visibility = Visibility.Hidden;
        }

        private static StackPanel GetStackPanel(Brush brush, WinForms.HorizontalAlignment horizontalAlignment)
        {
            StackPanel stackPanel1 = new StackPanel();
            stackPanel1.Background = brush;
            stackPanel1.Margin = new Thickness(5);
            stackPanel1.HorizontalAlignment = horizontalAlignment;
            return stackPanel1;
        }

        private static Image GetImage(string ImageName)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new System.Uri(ImageName);
            bitmapImage.EndInit();
            Image image = new Image();
            image.Height = 200;
            image.Width = 400;
            image.Source = bitmapImage;
            return image;
        }


        private static TextBlock GetTextBlock(string text, WinForms.HorizontalAlignment horizontalAlignment)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 16;
            textBlock.Background = Brushes.Transparent;
            textBlock.Width = 400;
            textBlock.Padding = new Thickness(10, 5, 10, 0);
            textBlock.Margin = new Thickness(5);
            textBlock.HorizontalAlignment = horizontalAlignment;
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.IsReadOnly = true;
            textBlock.Text = text;
            return textBlock;
        }


        private List<string> GetImagesPath(string folderName)
        {
            DirectoryInfo Folder = new DirectoryInfo(folderName);
            FileInfo[] Images = Folder.GetFiles();
            List<string> imagesList = new List<string>();

            for (int i = 0; i < Images.Length; i++)
            {
                imagesList.Add(string.Format(@"{0}/{1}", folderName, Images[i].Name));
            }

            return imagesList;
        }

        public string SplitFilesToFindImageName(string files)
        {
            string[] parts = files.Split("contents/");
            string url = parts[0];
            string imagePath = parts[1];

            return imagePath;
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationWindow loginWindow = new ConfigurationWindow();
            loginWindow.Show();
        }
    }
}

