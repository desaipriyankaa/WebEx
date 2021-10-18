using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Service.Library;
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
            var ChildStack = CreateNewStackPanelDynamicallyForOtherUsersChat();
            ParentStack.Children.Add(ChildStack);
        }

        /// <summary>
        /// Create StackPanel dynamically
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>

        private StackPanel CreateNewStackPanelDynamicallyForOtherUsersChat()
        {
            ParentStack.Children.Clear();
            string data = "";
            object selectChat = myFolders.SelectedItem;
            StackPanel stackPanel = new StackPanel();

            if (selectChat != null)
            {
                string filename = Path.Join(BasePath, selectChat.ToString(), "messages.json");
                List<Messages> msg = _services.ReadUserChatData(filename);

                foreach (var item in msg)
                {
                    //For Others chat
                    if (item.PersonEmail != "Sanket.Naik")
                    {

                        //Add stackpanel dynamically
                        StackPanel stackPanel1 = new StackPanel();
                        stackPanel1.Background = Brushes.Lavender;
                        stackPanel1.Margin = new Thickness(5);
                        stackPanel1.HorizontalAlignment = WinForms.HorizontalAlignment.Left;

                        if (item.Files != null)
                        {
                            foreach (var item1 in item.Files)
                            {
                                string imgName = SplitFilesToFindImageName(item1) ;
                                string imgFullPath = Path.Join(BasePath, selectChat.ToString(), imgName);

                                //Add Image
                                BitmapImage bitmapImage = new BitmapImage();
                                bitmapImage.BeginInit();
                                bitmapImage.UriSource = new System.Uri(imgFullPath);
                                bitmapImage.EndInit();
                                Image image = new Image();
                                image.Height = 200;
                                image.Source = bitmapImage;

                                stackPanel1.Children.Add(image);

                                data = item.PersonEmail + "     " + item.Created + " \n\n " + item.Text + " \n ";

                                if (item.Text == null)
                                {
                                    data = item.PersonEmail + "     " + item.Created + " \n ";
                                }
                            }
                        }
                        else
                        {
                            data = item.PersonEmail + "     " + item.Created + " \n\n " + item.Text + " \n ";
                        }

                        //Add textblock dynamically
                        TextBlock textBlock = new TextBlock();
                        textBlock.Background = Brushes.Transparent;
                        textBlock.Width = 400;
                        textBlock.Padding = new Thickness(10, 5, 10, 0);
                        textBlock.Margin = new Thickness(5);
                        textBlock.HorizontalAlignment = WinForms.HorizontalAlignment.Left;
                        textBlock.TextWrapping = TextWrapping.Wrap;
                        textBlock.IsReadOnly = true;
                        textBlock.Text = data;

                        stackPanel1.Children.Add(textBlock);
                        stackPanel.Children.Add(stackPanel1);
                    }


                    //for "Sanket.Naik"
                    if (item.PersonEmail == "Sanket.Naik")
                    {
                        //Add stackpanel dynamically
                        StackPanel stackPanel2 = new StackPanel();
                        stackPanel2.Background = Brushes.LightGreen;
                        stackPanel2.Margin = new Thickness(5);
                        stackPanel2.HorizontalAlignment = WinForms.HorizontalAlignment.Right;

                        if (item.Files != null)
                        {
                            foreach (var item1 in item.Files)
                            {
                                string imgName = SplitFilesToFindImageName(item1) + ".91f312cf-0562-42d3-a9c5-02aab5a5f0cc.PNG";
                                string imgFullPath = Path.Join(BasePath, selectChat.ToString(), imgName);

                                //Add Image
                                BitmapImage bitmapImage = new BitmapImage();
                                bitmapImage.BeginInit();
                                bitmapImage.UriSource = new System.Uri(imgFullPath);
                                bitmapImage.EndInit();
                                Image image = new Image();
                                image.Height = 200;
                                image.Source = bitmapImage;

                                stackPanel2.Children.Add(image);

                                data = item.PersonEmail + "     " + item.Created + " \n\n " + item.Text + " \n ";

                                if (item.Text == null)
                                {
                                    data = item.PersonEmail + "     " + item.Created + " \n ";
                                }
                            }
                        }
                        else
                        {
                            data = item.PersonEmail + "     " + item.Created + " \n\n " + item.Text + " \n ";
                        }

                        //Add textblock dynamically
                        TextBlock textBlock = new TextBlock();
                        textBlock.Background = Brushes.Transparent;
                        textBlock.Width = 400;
                        textBlock.Padding = new Thickness(10, 5, 10, 0);
                        textBlock.Margin = new Thickness(5);
                        textBlock.HorizontalAlignment = WinForms.HorizontalAlignment.Right;
                        textBlock.TextWrapping = TextWrapping.Wrap;
                        textBlock.IsReadOnly = true;
                        textBlock.Text = data;

                        stackPanel2.Children.Add(textBlock);
                        stackPanel.Children.Add(stackPanel2);
                    }
                }
            }
            return stackPanel;
        }

        
        public string SplitFilesToFindImageName(string files)
        {
            string[] parts = files.Split("contents/");
            string url = parts[0];
            string imagePath = parts[1];

            return imagePath;
        }

        //private void Media_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //OpenFileDialog ofd = new OpenFileDialog();
        //    //ofd.InitialDirectory = @"F:\PProject\WPF\WebexDump\DirectChats";
        //    //var a = ofd.InitialDirectory;
        //    //ofd.Filter = "Image files (*.PNG)|*.PNG|All Files(*.*)|*.*";

        //    //ofd.RestoreDirectory = true;

        //    //if (ofd.ShowDialog() == WinForms.Forms.DialogResult.OK)
        //    //{
        //    //    string selectedFilename = ofd.FileName;
        //    //    BitmapImage bitmap = new BitmapImage();
        //    //    bitmap.BeginInit();
        //    //    bitmap.UriSource = new System.Uri(selectedFilename);
        //    //    bitmap.EndInit();
        //    //    ImageViewer1.Source = bitmap;
        //    //}

        //    var selectChat = myFolders.SelectedItem;
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    ofd.Filter = "Image files (*.PNG)|*.PNG|All Files(*.*)|*.*";
                      
        //    if (selectChat != null)
        //    {
        //        string filename = Path.Join(BasePath, selectChat.ToString());

        //        if (ofd.ShowDialog() == WinForms.Forms.DialogResult.OK)
        //        {
        //            string selectedFilename = ofd.FileName;
        //            BitmapImage bitmap = new BitmapImage();
        //            bitmap.BeginInit();
        //            bitmap.UriSource = new System.Uri(selectedFilename);
        //            bitmap.EndInit();
        //            ImageViewer1.Source = bitmap;
        //        }
        //    }
        //}
    }
}

