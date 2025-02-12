using CSV_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using static 記帳.Contract.記一筆Contract;
using 記帳.Presenter;
using static 記帳.Contract.UploadImageContract;
namespace 記帳.Forms
{
    public partial class 記一筆 : Form, 記一筆IView, UploadImageIView
    {
        private 記一筆IPresenter _presenter;
        private UploadImageIPresenter _uploadPresenter;
        public 記一筆()
        {
            InitializeComponent();
            _presenter = new 記一筆Presenter(this);
            _uploadPresenter = new UploadImagePresenter(this);
        }
        private void 記一筆_Load(object sender, EventArgs e)
        {

            comboBox1.DataSource = DataModel.Type;
            comboBox3.DataSource = DataModel.Object;
            comboBox4.DataSource = DataModel.PaymentMethod;

            pictureBox1.Image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["DefaultUploadPath"]);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["DefaultUploadPath"]);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = comboBox1.SelectedValue.ToString();
            comboBox2.DataSource = DataModel.dictionary[item];

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Debounce(() =>
            {

                RecordModel recordModel = new RecordModel();
                recordModel.Date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                recordModel.Price = textBox1.Text;
                recordModel.Type = comboBox1.SelectedValue.ToString();
                recordModel.Purpose = comboBox2.SelectedValue.ToString();
                recordModel.Target = comboBox3.SelectedValue.ToString();
                recordModel.PayMethod = comboBox4.SelectedValue.ToString();
                if (!Directory.Exists(Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date))) //確認是否有此位址
                {
                    Directory.CreateDirectory(Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date));
                }
                _presenter.saveData(recordModel, pictureBox1.Image, pictureBox2.Image);

                Reset();

            });


        }
        public void Reset()
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            textBox1.Text = string.Empty;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox1.Image.Dispose();
            pictureBox2.Image.Dispose();
            pictureBox1.Image = null;
            pictureBox2.Image = null;

            GC.Collect();
            pictureBox1.Image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["DefaultUploadPath"]);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["DefaultUploadPath"]);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }




        private void UploadImage_Click(object sender, EventArgs e)
        {
            //Console.WriteLine($"Memory usage: {GC.GetTotalMemory(false)} bytes");
            //Console.WriteLine($"Total memory used by process: {Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024} MB");
            pictureBox1.Image?.Dispose();
            pictureBox2.Image?.Dispose();
            //pictureBox1.Image = null;
            //pictureBox2.Image = null;
            string pictureAddress = String.Empty;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "圖片檔|*.jpg;*.png";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureAddress = openFileDialog.FileName;
                _uploadPresenter.CompressImageRequest(pictureAddress);

            }

            openFileDialog.Dispose();
            openFileDialog = null;
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();
            //Console.WriteLine($"Memory usage: {GC.GetTotalMemory(false)} bytes");
            //Console.WriteLine($"Total memory used by process: {Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024} MB");
        }



        public void CompressResponse(Image compressImage)
        {
            pictureBox1.Image = compressImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = compressImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
