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
using 記帳.Presenter;
using 記帳.Utility;
using static 記帳.Contract.BookKeepingContract;


namespace 記帳.Forms
{
    public partial class 記帳本 : Form, BookKeepingIView
    {
        private BookKeepingIPresenter _bookKeepingPresenter;
        public 記帳本()
        {
            InitializeComponent();
            _bookKeepingPresenter = new BookKeepingPresenter(this);
        }
        List<RecordModel> list = new List<RecordModel>();
        private void Search_Click(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;

            this.Debounce(() =>
            {
                _bookKeepingPresenter.search_Events(dataGridView1, date1, date2);
            });


        }


        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _bookKeepingPresenter.CellDoubleClick(dataGridView1, e.ColumnIndex, e.RowIndex);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _bookKeepingPresenter.EditData(dataGridView1, e.ColumnIndex, e.RowIndex);
        }

        public void search_Response(DataGridView dataGridView)
        {
            dataGridView1 = dataGridView;
        }

        public void showPicTure(string URL)
        {
            PictureDialog pictureDialog = new PictureDialog(URL);
            pictureDialog.ShowDialog();
        }
    }
}
