using CSV_Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Contract;
using 記帳.Models;
using 記帳.Presenter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static 記帳.Contract.AccountAnalysisContract;
using static 記帳.Contract.BookKeepingContract;

namespace 記帳.Forms
{
    public partial class 帳本分析 : Form, AccountAnalysisIView
    {
        List<string> typeFilter = new List<string>();
        List<string> targetFilter = new List<string>();
        List<string> payMethodFilter = new List<string>();
        private AccountAnalysisIPresenter _accountAnalysisPresenter;
        public 帳本分析()
        {
            InitializeComponent();
            _accountAnalysisPresenter = new AccountAnalysisPresenter(this);
        }

        public void search_Response(DataGridView dataGridView)
        {
            dataGridView1 = dataGridView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;

            this.Debounce(() =>
            {
                _accountAnalysisPresenter.search_Events(date1, date2, typeFilter, targetFilter, payMethodFilter);



                // 動態資料群組 => 建立匿名類別

            });

        }

        private void 帳本分析_Load(object sender, EventArgs e)
        {
            var props = typeof(DataModel).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                                    .Where(x => x.FieldType == typeof(List<string>)) /*& (!x.Name.Contains("Purpose")))*/
                                    .ToList();
            foreach (var prop in props)
            {
                var itemList = (List<string>)prop.GetValue(null);
                CheckBox totalCheck = new CheckBox
                {
                    Text = "全選",
                    AutoSize = true
                };
                totalCheck.CheckedChanged += TotalCheckChanged;
                FlowLayoutPanel panel = new FlowLayoutPanel
                {
                    Height = flowLayoutPanel1.Height / 8,
                    Width = flowLayoutPanel1.Width,
                    FlowDirection = FlowDirection.LeftToRight,

                };
                panel.Controls.Add(totalCheck);

                foreach (var property in itemList)
                {
                    CheckBox checkBox = new CheckBox
                    {
                        Text = property,
                        AutoSize = true,
                        Tag = prop.Name.ToString()

                    };
                    panel.Controls.Add(checkBox);
                    checkBox.CheckedChanged += CheckChanged;

                }
                flowLayoutPanel1.Controls.Add(panel);
            }
        }


        private void CheckChanged(object sender, EventArgs e)
        {

            CheckBox checkbox = (CheckBox)sender;
            Console.WriteLine(checkbox.Text);
            if (checkbox.Checked)
            {
                switch (checkbox.Tag)
                {
                    case "Type":
                        typeFilter.Add(checkbox.Text);
                        break;
                    case "Object":
                        targetFilter.Add(checkbox.Text);
                        break;
                    case "PaymentMethod":
                        payMethodFilter.Add(checkbox.Text);
                        break;
                }
            }
            if (!checkbox.Checked)
            {
                switch (checkbox.Tag)
                {
                    case "Type":
                        typeFilter.Remove(checkbox.Text);
                        break;
                    case "Object":
                        targetFilter.Remove(checkbox.Text);
                        break;
                    case "PaymentMethod":
                        payMethodFilter.Remove(checkbox.Text);
                        break;
                }
            }


            //String key = "";
            //foreach (var item in DataModel.dictionary)
            //{
            //    if (item.Value.Contains(checkbox.Text))
            //    {
            //        key = item.Key;
            //        break;
            //    }
            //}
            var key = DataModel.dictionary.FirstOrDefault(x => x.Value.Contains(checkbox.Text)).Key;


            var groupCheckbox = flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>()
                                            .SelectMany(x => x.Controls.OfType<CheckBox>())
                                            .FirstOrDefault(x => x.Text?.ToString() == key);
            if (groupCheckbox != null)
                groupCheckbox.Checked = true;
            Console.WriteLine(key);
        }

        private void TotalCheckChanged(object sender, EventArgs e)
        {
            CheckBox totalCheck = (CheckBox)sender;
            FlowLayoutPanel panel = totalCheck.Parent as FlowLayoutPanel;
            var checkBoxes = panel.Controls.OfType<CheckBox>().Where(x => x != totalCheck).ToList();
            foreach (var checkBox in checkBoxes)
            {
                checkBox.Checked = !checkBox.Checked;

            }
        }

        public void search_Response(List<AnalysisModel> list)
        {
            dataGridView1.DataSource = list;
        }



    }
}
