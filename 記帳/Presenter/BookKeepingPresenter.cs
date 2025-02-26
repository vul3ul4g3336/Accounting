using CSV_Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Attributes;
using 記帳.ExtensionFile;
using 記帳.Forms;
using 記帳.Models;
using 記帳.Repositary;
using 記帳.Utility;
using static 記帳.Contract.BookKeepingContract;

namespace 記帳.Presenter
{
    internal class BookKeepingPresenter : BookKeepingIPresenter
    {
        private BookKeepingIView _view;
        private DateTime date1;
        private DateTime date2;
        IRepository repository = new CSV_Repository();
        public BookKeepingPresenter(BookKeepingIView view)
        {
            _view = view;
        }
        List<RecordModel> list = new List<RecordModel>();
        public void search_Events(DataGridView dataGridView, DateTime dateTime1, DateTime dateTime2)
        {
            list.Clear();
            dataGridView.ImageDispose(); // 治標不治本方式
            date1 = dateTime1;
            date2 = dateTime2;

            list = repository.GetRawData(dateTime1, dateTime2);
            dataGridView.DataSource = list;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView = SetDataGirdView.SetColumns(dataGridView, list);
            // 遍歷每一行，將圖片加載到最後兩個欄位
            _view.search_Response(dataGridView);



        }

        public void EditData(DataGridView dataGridView1, int columnIndex, int rowIndex)
        {
            var propName = dataGridView1.Columns[columnIndex].Name;
            if (propName == "Type_combo")
            {
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dataGridView1.Rows[rowIndex].Cells["Purpose_combo"];

                var itemlist = DataModel.dictionary[dataGridView1.Rows[rowIndex].Cells["Type_combo"].Value.ToString()];
                comboBoxCell.DataSource = itemlist;
                comboBoxCell.Value = itemlist[0];
                list[rowIndex].Purpose = itemlist[0];
            }
            if (propName.Contains("combo"))
            {
                propName = propName.Split('_')[0];
                var targetProp = typeof(RecordModel).GetProperties().First(x => x.Name == (propName));
                targetProp.SetValue(list[rowIndex], dataGridView1.Rows[rowIndex].Cells[propName + "_combo"].Value.ToString());
            }
            string rootFile = ConfigurationManager.AppSettings["DirectoryPath"];
            string filePath = Path.Combine(rootFile, list[rowIndex].Date,
                "記帳.csv");
            string date = list[rowIndex].Date;
            var nowadays = list.Where(x => x.Date == date).ToList();
            repository.SaveRecords(list[rowIndex].Date, nowadays);

        }





        public void CellDoubleClick(DataGridView dataGridView1, int columnIndex, int rowIndex)
        {

            string columnType = dataGridView1.Columns[columnIndex].GetType().Name;
            if (columnType != "DataGridViewImageColumn")
                return;
            string imagePropName = dataGridView1.Columns[columnIndex].Name;
            if (imagePropName == "GarbageCan")
            {
                dataGridView1.Rows[rowIndex].Cells.OfType<DataGridViewImageCell>()
                    .ToList().ForEach(x => (x.Value as IDisposable)?.Dispose());
                string rootFile = ConfigurationManager.AppSettings["DirectoryPath"];
                string filePath = Path.Combine(rootFile, list[rowIndex].Date,
                    "記帳.csv");
                var prop = typeof(RecordModel).GetProperties();
                var photoAddress = prop.Where(x => x.Name.Contains("Address"));
                foreach (var address in photoAddress)
                {
                    var addressValue = address.GetValue(list[rowIndex])?.ToString();
                    string adress = Path.Combine(rootFile, list[rowIndex].Date, addressValue);
                    repository.DeleteData(adress);
                }
                string date = list[rowIndex].Date;

                //刪照片
                list.RemoveAt(rowIndex);  // 刪除的資料 A


                var nowadays = list.Where(x => x.Date == date).ToList();
                switch (nowadays.Count)
                {
                    case 0:
                        Directory.Delete(Path.Combine(rootFile, date), true);
                        break;
                    default:
                        repository.DeleteData(filePath);
                        repository.SaveRecords(filePath, nowadays);
                        break;
                }
                search_Events(dataGridView1, date1, date2);
                return;
            }

            string targetPropName = dataGridView1.Columns[columnIndex].Name.Split('_')[0].Replace("resized", "");
            var targetProp = typeof(RecordModel).GetProperties().First(x => x.Name == targetPropName);
            string imgURL = targetProp.GetValue(list[rowIndex]).ToString();
            _view.showPicTure(imgURL);




        }
    }
}
