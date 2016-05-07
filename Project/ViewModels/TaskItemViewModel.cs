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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
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

            // 从数据库中加载数据
            getItemFromDB();
            //
        }

        public async void getItemFromDB()
        {
            this.allItems.Clear();
            try
            {
                var dp = App.conn;
                using (var statement = dp.Prepare(@"SELECT * FROM TaskItem"))
                {
                    while (SQLiteResult.ROW == statement.Step())
                    {
                        //do with pic
                        BitmapImage bimage = new BitmapImage();
                        StorageFile file = await StorageFile.GetFileFromPathAsync((string)statement[4]);
                        IRandomAccessStream instream = await file.OpenAsync(FileAccessMode.Read);
                        string boot = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList.Add(file);
                        await bimage.SetSourceAsync(instream);
                        // 处理时间
                        DateTime dt;
                        DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                        dtFormat.ShortDatePattern = "yyyy-MM-dd";
                        dt = Convert.ToDateTime((string)statement[3], dtFormat);
                        this.allItems.Add(new Models.TaskItem(statement[0].ToString(), statement[1].ToString(), (string)statement[2], bimage, dt, (string)statement[4]));
                    }
                }
            }
            catch
            {
                return;
            }


        }

        public void AddTaskItem(string title, string description, DateTime datetime, string filepath, string username)
        {
            if (filepath == null) filepath = path;
            var dp = App.conn;
            try
            {
                using (var Todolist = dp.Prepare("INSERT INTO TaskItem (Title, Detail, Datetime, Filepath, Username) VALUES (?, ?, ?, ?, ?)"))
                {
                    Todolist.Bind(1, title);
                    Todolist.Bind(2, description);
                    Todolist.Bind(3, datetime.Date.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo));
                    Todolist.Bind(4, filepath);
                    Todolist.Bind(5, username);
                    Todolist.Step();
                    getItemFromDB();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public void RemoveTaskItem(string id)
        {
            this.allItems.Remove(this.selectedItem);
            using (var statement = App.conn.Prepare("DELETE FROM TaskItem WHERE Id = ?"))
            {
                statement.Bind(1, id);
                statement.Step();
            }
            // set selectedItem to null after remove
            this.selectedItem = null;
        }

        //public void updatetaskitem(string id, string title, string description, datetime datetime, bitmapimage bitmapimage, string filepath)
        //{
        //    if (this.selecteditem != null)
        //    {
        //        using (var todolist = app.conn.prepare("update taskitem set title = ?, detail = ?, datetime = ?, filepath = ? where id = ?"))
        //        {
        //            todolist.bind(1, title);
        //            todolist.bind(2, description);
        //            todolist.bind(3, datetime.date.tostring("yyyy-mm-dd", datetimeformatinfo.invariantinfo));
        //            todolist.bind(4, filepath);
        //            todolist.bind(5, id);
        //            todolist.step();
        //        }
        //    }
        //    getitemfromdb();
        //    this.selecteditem = null;
        //}

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
