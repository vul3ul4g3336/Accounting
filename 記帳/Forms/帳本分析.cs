using CSV_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Models;

namespace 記帳.Forms
{
    public partial class 帳本分析 : Form
    {
        public 帳本分析()
        {
            InitializeComponent();
        }

        List<RecordModel> list;
        private void button1_Click(object sender, EventArgs e)
        {
            list = CSV.Read<RecordModel>(@"C:\Users\TUF\source\repos\記帳\記帳資料\2025-02-09\記帳.csv");
            dataGridView1.DataSource = list;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
