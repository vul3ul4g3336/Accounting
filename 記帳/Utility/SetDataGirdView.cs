using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Attributes;
using 記帳.Models;

namespace 記帳.Utility
{
    public class SetDataGirdView
    {

        public DataGridView finalDataGridView;
        public static DataGridView SetColumns(DataGridView dataGridView1, List<RecordModel> list)
        {

            var dataProp = typeof(DataModel).GetFields(BindingFlags.Static | BindingFlags.Public).ToList();
            var props = typeof(RecordModel).GetProperties().ToList();
            for (int i = 0; i < props.Count; i++)
            {
                var attrs = props[i].GetCustomAttributes().ToList();

                if (attrs.Any(x => x.GetType() == typeof(ComboBoxColumnAttribute)))
                {
                    var column = dataGridView1.Columns[props[i].Name];
                    column.Visible = false;
                    DataGridViewComboBoxColumn purposeColumn = new DataGridViewComboBoxColumn();
                    purposeColumn.Name = props[i].Name + "_combo";
                    purposeColumn.HeaderText = column.HeaderText;
                    var prop = dataProp.FirstOrDefault(x => x.Name == props[i].Name);
                    if (prop != null)
                    {
                        purposeColumn.DataSource = prop.GetValue(null);
                    }

                    dataGridView1.Columns.Insert(i, purposeColumn);

                }
                if (attrs.Any(x => x.GetType() == typeof(HideAttribute)))
                {
                    dataGridView1.Columns[props[i].Name].Visible = false;
                }
                if (attrs.Any(x => x.GetType() == typeof(ImageColumnAttribute)))
                {
                    dataGridView1.Columns[props[i].Name].Visible = false;
                    var imageColumn = new DataGridViewImageColumn
                    {
                        Name = $"{props[i].Name}_image", //縮圖
                        HeaderText = "縮圖",
                        ImageLayout = DataGridViewImageCellLayout.Stretch
                    };
                    dataGridView1.Columns.Add(imageColumn);
                }

            }
            var imageColumn3 = new DataGridViewImageColumn
            {
                Name = "GarbageCan",
                HeaderText = "刪除",
                ImageLayout = DataGridViewImageCellLayout.Stretch
            };
            dataGridView1.Columns.Add(imageColumn3);

            for (int i = 0; i < list.Count; i++)
            {
                var data = list[i];

                foreach (var prop in props)
                {
                    var attrs = prop.GetCustomAttributes();
                    if (prop.GetCustomAttributes().Any(x => x.GetType() == typeof(ImageColumnAttribute)))
                    {
                        string imageURL = prop.GetValue(data).ToString();
                        dataGridView1.Rows[i].Cells[$"{prop.Name}_image"].Value = new Bitmap(imageURL);
                    }

                    if (prop.GetCustomAttributes().Any(x => x.GetType() == typeof(ComboBoxColumnAttribute)))
                    {
                        var comboBoxColumnAttribute = attrs.OfType<ComboBoxColumnAttribute>().First();
                        if (comboBoxColumnAttribute._source == "")
                        {
                            dataGridView1.Rows[i].Cells[prop.Name + "_combo"].Value = prop.GetValue(data).ToString();
                            continue;
                        }
                        string keyWord = comboBoxColumnAttribute._source.ToString();
                        var keyValue = dataGridView1.Rows[i].Cells[keyWord].Value.ToString();
                        var propName = prop.Name + "_combo";
                        var column = (DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[propName];
                        column.DataSource = DataModel.dictionary[keyValue];
                        dataGridView1.Rows[i].Cells[propName].Value = dataGridView1.Rows[i].Cells[prop.Name].Value;
                    }
                }
                String trashPath = Path.Combine(ConfigurationManager.AppSettings["TrashImgPath"], "Delete.jpg");
                dataGridView1.Rows[i].Cells["GarbageCan"].Value = new Bitmap(trashPath);
            }
            return dataGridView1;
        }

    }
}
