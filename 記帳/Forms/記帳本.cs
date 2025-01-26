using CSV_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Attributes;
using 記帳.Models;


namespace 記帳.Forms
{
    public partial class 記帳本 : Form
    {
        public 記帳本()
        {
            InitializeComponent();
        }
        List<RecordModel> list = new List<RecordModel>();
        private void Search_Click(object sender, EventArgs e)
        {
            Search_Events();
        }
        public void Search_Events()
        {
            this.Debounce(() =>
            {
                list.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = null;
                GC.Collect(); // 治標不治本方式
                string rootFile = ConfigurationManager.AppSettings["DirectoryPath"];

                TimeSpan timeSpan = dateTimePicker2.Value - dateTimePicker1.Value;
                for (int i = 0; i <= timeSpan.Days; i++)
                {
                    string date = dateTimePicker1.Value.AddDays(i).ToString("yyyy-MM-dd");
                    if (!Directory.Exists(Path.Combine(rootFile, date)))
                    {
                        continue;
                    }
                    var data = CSV.Read<RecordModel>(Path.Combine(rootFile, date, "記帳.csv"));
                    list.AddRange(data);
                }

                dataGridView1.DataSource = list;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                SetColumns();


                // 遍歷每一行，將圖片加載到最後兩個欄位
                for (int i = 0; i < list.Count; i++)
                {
                    var data = list[i];
                    var prop = data.GetType().GetProperties();
                    dataGridView1.Rows[i].Cells["類型_combo"].Value = data.Type;

                    DataGridViewComboBoxCell comboBoxCell =
                    (DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells["目的_combo"];
                    comboBoxCell.DataSource = DataModel.dictionary[data.Type];
                    comboBoxCell.Value = data.Purpose;

                    Bitmap originalimage = new Bitmap(list[i].resizedPictureAddress1);
                    Bitmap originalimage2 = new Bitmap(list[i].resizedPictureAddress2);
                    //"C:\Users\TUF\source\repos\記帳\Delete.png"
                    String A = Path.Combine(ConfigurationManager.AppSettings["TrashImgPath"], "Delete.jpg");
                    Bitmap image3 = new Bitmap(A);
                    int length = dataGridView1.ColumnCount;
                    dataGridView1.Rows[i].Cells[length - 3].Value = originalimage; // 倒數第二欄
                    dataGridView1.Rows[i].Cells[length - 2].Value = originalimage2; // 倒數第一欄
                    dataGridView1.Rows[i].Cells[length - 1].Value = image3; // 倒數第一欄
                }


            });
        }
        private void SetColumns()
        {
            int index = 0;
            var props = typeof(RecordModel).GetProperties().ToList();
            for (int i = 0; i < props.Count; i++)
            {
                var attrs = props[i].GetCustomAttributes();
                if (attrs.Any(x => x.GetType().Name == "ComboBoxColumnAttribute"))
                {
                    dataGridView1.Columns[index].Visible = false;
                    DataGridViewComboBoxColumn purposeColumn = new DataGridViewComboBoxColumn();
                    purposeColumn.DataSource = DataModel.Purpose;
                    purposeColumn.Name = dataGridView1.Columns[index].HeaderText + "_combo";
                    purposeColumn.HeaderText = dataGridView1.Columns[index].HeaderText;
                    dataGridView1.Columns.Insert(index, purposeColumn);
                    index++;
                }
                if (attrs.Any(x => x.GetType().Name == "HideAttribute"))
                {
                    dataGridView1.Columns[index].Visible = false;
                }
                if (attrs.Any(x => x.GetType().Name == "ImageColumnAttribute"))
                {
                    dataGridView1.Columns[index].Visible = false;
                    var imageColumn = new DataGridViewImageColumn
                    {
                        Name = $"ImageColumn{i}",
                        HeaderText = "縮圖",
                        ImageLayout = DataGridViewImageCellLayout.Stretch
                    };
                    dataGridView1.Columns.Add(imageColumn);
                }
                index++;
            }
            var imageColumn3 = new DataGridViewImageColumn
            {
                Name = "ImageColumn3",
                HeaderText = "刪除",
                ImageLayout = DataGridViewImageCellLayout.Stretch
            };
            dataGridView1.Columns.Add(imageColumn3);

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            switch (e.ColumnIndex)
            {
                case 12:
                case 13:
                    PictureDialog pictureDialog = new PictureDialog(list[e.RowIndex].PictureAddress1);
                    pictureDialog.ShowDialog();

                    break;
                case 14: // 1. 找到日期資料夾// 2.找出list裡該日的資料  //3. 遍歷複寫
                    (dataGridView1.Rows[e.RowIndex].Cells[12].Value as IDisposable)?.Dispose();
                    (dataGridView1.Rows[e.RowIndex].Cells[13].Value as IDisposable)?.Dispose();
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    string rootFile = ConfigurationManager.AppSettings["DirectoryPath"];
                    string filePath = Path.Combine(rootFile, list[e.RowIndex].Date,
                        "記帳.csv");

                    string date = list[e.RowIndex].Date;
                    File.Delete(filePath);
                    //刪照片
                    list.RemoveAt(e.RowIndex);  // 刪除的資料 A
                    var nowadays = list.Where(x => x.Date == date).ToList();
                    switch (nowadays.Count)
                    {
                        case 0:

                            Directory.Delete(Path.Combine(rootFile, date), true);
                            break;
                        default:
                            CSV.Write(filePath, nowadays);
                            break;
                    }


                    Search_Events();
                    break;
                default:
                    return;

            }

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                DataGridView dataGridView = (DataGridView)sender;
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dataGridView1.Rows[e.RowIndex].Cells[4];

                var itemlist = DataModel.dictionary[dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()];
                comboBoxCell.DataSource = itemlist;
                comboBoxCell.Value = itemlist[0];
                list[e.RowIndex].Purpose = itemlist[0];
            }
            var data = list[e.RowIndex];
            var prop = data.GetType().GetProperties()[e.ColumnIndex];
            prop.SetValue(data, dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            string rootFile = ConfigurationManager.AppSettings["DirectoryPath"];
            string filePath = Path.Combine(rootFile, list[e.RowIndex].Date,
                "記帳.csv");
            string date = list[e.RowIndex].Date;
            var nowadays = list.Where(x => x.Date == date).ToList();
            File.Delete(filePath);
            CSV.Write(filePath, nowadays);

        }
    }
}
