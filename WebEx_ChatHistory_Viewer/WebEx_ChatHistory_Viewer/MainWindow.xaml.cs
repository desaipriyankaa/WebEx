using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChatHistory.Viewer;
using Service.Library;
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
        
        public string BasePath { get; set; }

        object selectChat;
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

            if (selectChat != null)
            {
                string filename = Path.Join(BasePath, selectChat.ToString(), "messages.json");
                List<Messages> msg = _services.ReadUserChatData(filename);
                List<string> localImagesPath = GetImagesPath(Path.Join(BasePath, selectChat.ToString()));

                foreach (var item in msg)
                {
                    //For Others chat
                    if (item.PersonEmail != "Sanket.Naik")
                    {
                        StackPanel stackPanel1 = GetStackPanel(Brushes.Lavender, WinForms.HorizontalAlignment.Left);

                        // Check for Text is present or not
                        if (item.Text != null)
                        {
                            data = item.PersonEmail + "     " + item.Created + " \n\n " + item.Text + " \n ";
                        }
                        else if (item.Text == null)
                        {
                            data = item.PersonEmail + "     " + item.Created + " \n ";
                        }

                        TextBlock textBlock = GetTextBlock(data, WinForms.HorizontalAlignment.Left);
                        stackPanel1.Children.Add(textBlock);

                        //Check for Image is present or not
                        if (item.Files != null)
                        {
                            foreach (string item1 in item.Files)
                            {
                                string imgNameFromJSON = SplitFilesToFindImageName(item1);
                                List<string> imageFullName = localImagesPath.FindAll(i => i.Contains(imgNameFromJSON));
                                foreach (var singleImageName in imageFullName)
                                {
                                    //Add Image
                                    Image image = GetImage(singleImageName);
                                    Button button = new Button();
                                    button.Content = image;
                                    button.Background = Brushes.Transparent;
                                    stackPanel1.Children.Add(button);

                                    button.Click += ZoomImage_Click;
                                }
                            }
                        }
                        else
                        {
                            data = item.PersonEmail + "     " + item.Created + " \n\n " + item.Text + " \n ";
                        }
                        stackPanel.Children.Add(stackPanel1);
                    }


                    //for "Sanket.Naik"
                    if (item.PersonEmail == "Sanket.Naik")
                    {
                        StackPanel stackPanel2 = GetStackPanel(Brushes.LightGreen, WinForms.HorizontalAlignment.Right);

                        // Check for Text is present or not
                        if (item.Text != null)
                        {
                            data = item.PersonEmail + "     " + item.Created + " \n\n " + item.Text + " \n ";
                        }
                        if (item.Text == null)
                        {
                            data = item.PersonEmail + "     " + item.Created + " \n ";
                        }

                        TextBlock textBlock = GetTextBlock(data, WinForms.HorizontalAlignment.Right);
                        stackPanel2.Children.Add(textBlock);

                        //Check for Image is present or not
                        if (item.Files != null)
                        {
                            foreach (string item1 in item.Files)
                            {
                                string imgNameFromJSON = SplitFilesToFindImageName(item1);
                                List<string> imageFullName = localImagesPath.FindAll(i => i.Contains(imgNameFromJSON));
                                foreach (string singleImageName in imageFullName)
                                {
                                    Image image = GetImage(singleImageName);
                                    Button button = new Button();
                                    button.Content = image;
                                    button.Background = Brushes.Transparent;
                                    stackPanel2.Children.Add(button);

                                    button.Click += ZoomImage_Click;
                                }
                            }
                        }
                        else
                        {
                            data = item.PersonEmail + "     " + item.Created + " \n\n " + item.Text + " \n ";
                        }
                        stackPanel.Children.Add(stackPanel2);
                    }
                }
            }
            return stackPanel;
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
            image.Height = 800;
            image.Width = 1000;
            image.Source = bitmapImage;
            StackPanel btnStack = CloseButtonStack();

            StackPanel stackPanel = new StackPanel();
            stackPanel.Background = Brushes.LightGray;
            stackPanel.Orientation = WinForms.Controls.Orientation.Vertical;

            stackPanel.Children.Add(btnStack);
            stackPanel.Children.Add(image);
            frame.Navigate(stackPanel);

        }

        private StackPanel CloseButtonStack()
        {
            StackPanel btnStack = new StackPanel();
            btnStack.Orientation = WinForms.Controls.Orientation.Horizontal;
            Button button = new Button();
            button.HorizontalAlignment = WinForms.HorizontalAlignment.Right;
            button.Content = "Close";
            button.FontSize = 20;
            button.Width = 100;
            button.Height = 50;
            button.Margin = new Thickness(1500, 10, 10, 10);
            button.Click += BackButton_Click;
            btnStack.Children.Add(button);
            return btnStack;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
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
            
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            //this.Close();
            WinForms.Application.Current.MainWindow.Close();
        }
    }
}
 
