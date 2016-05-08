using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Project
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AddTaskPage : Page
    {
        string path;
        string currentUserName;
        public AddTaskPage()
        {
            this.InitializeComponent();
            //aaaaa
            this.ViewModel = new ViewModels.TaskItemViewModel();
            FileInfo pfile = new FileInfo("Project.exe");
            this.path = pfile.DirectoryName + "\\Assets\\SplashScreen.scale-200.png";
            this.currentUserName = App.login_user.Username;
        }
        ViewModels.TaskItemViewModel ViewModel { get; set; }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            string imgPath;
            imgPath = this.path;
            bool valid = checkValid();
            if (valid == true)
            {
                ViewModel.AddTaskItem(title.Text.ToString(), details.Text.ToString(), time.Date.DateTime, imgPath, currentUserName, "");
            }
            Frame.Navigate(typeof(HomePage), "");
        }
        //get file path

        private bool checkValid()
        {
            if (title.Text.ToString() == "" || details.Text.ToString() == "")
            {
                var i = new MessageDialog("something empty").ShowAsync();
                return false;
            } else if (time.Date.DateTime < DateTime.Today) {
                var i = new MessageDialog("time wrong!").ShowAsync();
            }
            return true;
        }

        private async void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();  //允许用户打开和选择文件的UI
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();  //storageFile:提供有关文件及其内容以及操作的方式
            if (file != null)
            {
                path = file.Path;
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap 
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.DecodePixelWidth = 600; //match the target Image.Width, not shown
                    await bitmapImage.SetSourceAsync(fileStream);
                    img.Source = bitmapImage;
                }
            }
            else
            {
                var i = new MessageDialog("error with picture").ShowAsync();
            }
        }
    }
}
