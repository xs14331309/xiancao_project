using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Project.ViewModels
{
    class TaskItemViewModel
    {
        private ObservableCollection<Models.TaskItem> allItems = new ObservableCollection<Models.TaskItem>();
        public ObservableCollection<Models.TaskItem> AllItems { get { return this.allItems; } }

        private Models.TaskItem selectedItem = default(Models.TaskItem);
        public Models.TaskItem SelectedItem { get { return selectedItem; } set { this.selectedItem = value; } }
        public string path;        // 默认图片路径

        public TaskItemViewModel()
        {
            // 获取当前应用程序所在位置的路径
            FileInfo pfile = new FileInfo("Todos.exe");
            // 图片绝对的路径 = 绝对路径+相对路径
            path = pfile.DirectoryName + "\\Assets\\set.jpg";

            // 从数据库中加载数据
            //getItemFromDB();
            //
        }

        //public async void getItemFromDB()
        //{
        //    //this.allItems.Clear();
        //    //var dp = App.conn;
        //    //using (var statement = dp.Prepare(@"SELECT * FROM TaskItem"))
        //    //{
        //    //    while (SQLiteResult.ROW == statement.Step())
        //    //    {
        //    //        // 处理图片
        //    //        StorageFile file = await StorageFile.GetFileFromPathAsync((string)statement[4]);
        //    //        IRandomAccessStream instream = await file.OpenAsync(FileAccessMode.Read);
        //    //        string boot = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList.Add(file);
        //    //        BitmapImage bimage = new BitmapImage();
        //    //        await bimage.SetSourceAsync(instream);

        //    //        // 处理时间
        //    //        DateTime dt;
        //    //        DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
        //    //        dtFormat.ShortDatePattern = "yyyy-MM-dd";
        //    //        dt = Convert.ToDateTime((string)statement[3], dtFormat);
        //    //        this.allItems.Add(new Models.TaskItem(statement[0].ToString(), statement[1].ToString(), (string)statement[2], dt, bimage, (string)statement[4]));
        //    //    }
        //    //}
        //}

        public void AddTaskItem(string title, string description, DateTime datetime, BitmapImage bitmapImage, string filepath)
        {
            //if (filepath == null) filepath = path;
            //var dp = App.conn;
            //try
            //{
            //    using (var Todolist = dp.Prepare("INSERT INTO TaskItem (Title, Detail, Datetime, Filepath) VALUES (?, ?, ?, ?)"))
            //    {
            //        Todolist.Bind(1, title);
            //        Todolist.Bind(2, description);
            //        Todolist.Bind(3, datetime.Date.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo));
            //        Todolist.Bind(4, filepath);
            //        Todolist.Step();
            //        getItemFromDB();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}

        }

        public void RemoveTaskItem(string id)
        {
            //this.allItems.Remove(this.selectedItem);
            //using (var statement = App.conn.Prepare("DELETE FROM TaskItem WHERE Id = ?"))
            //{
            //    statement.Bind(1, id);
            //    statement.Step();
            //}
            //// set selectedItem to null after remove
            //this.selectedItem = null;
        }

        public void UpdateTaskItem(string id, string title, string description, DateTime datetime, BitmapImage bitmapImage, string filepath)
        {
            //if (this.selectedItem != null)
            //{
            //    using (var Todolist = App.conn.Prepare("UPDATE TaskItem SET Title = ?, Detail = ?, Datetime = ?, Filepath = ? WHERE Id = ?"))
            //    {
            //        Todolist.Bind(1, title);
            //        Todolist.Bind(2, description);
            //        Todolist.Bind(3, datetime.Date.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo));
            //        Todolist.Bind(4, filepath);
            //        Todolist.Bind(5, id);
            //        Todolist.Step();
            //    }
            //}
            //getItemFromDB();
            //this.selectedItem = null;
        }

        public Models.TaskItem GetNewestItem()
        {
            return this.allItems.Last<Models.TaskItem>();
        }

        public static implicit operator TaskItemViewModel(RoutedEventArgs v)
        {
            throw new NotImplementedException();
        }

    }

}
