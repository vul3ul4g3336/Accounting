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
namespace 記帳.Forms
{
    public partial class 記一筆 : Form
    {
        public 記一筆()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            記帳本 記帳本 = new 記帳本();
            記帳本.Show();

            this.Hide();
        }

        private void 記一筆_Load(object sender, EventArgs e)
        {

            comboBox1.DataSource = DataModel.Purpose;

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
                string pngName1_Path = Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date, $"{Guid.NewGuid().ToString()}.png");
                string pngName2_Path = Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date, $"{Guid.NewGuid().ToString()}.png");
                string pngName3_Path = Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date, $"{Guid.NewGuid().ToString()}.png");
                string pngName4_Path = Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date, $"{Guid.NewGuid().ToString()}.png");
                pictureBox1.Image.Save(pngName1_Path);
                pictureBox2.Image.Save(pngName2_Path);
                Bitmap image = new Bitmap(pictureBox1.Image, 30, 30);
                Bitmap image2 = new Bitmap(pictureBox2.Image, 30, 30);

                image.Save(pngName3_Path);
                image2.Save(pngName4_Path);
                recordModel.PictureAddress1 = pngName1_Path;
                recordModel.PictureAddress2 = pngName2_Path;
                recordModel.resizedPictureAddress1 = pngName3_Path;
                recordModel.resizedPictureAddress2 = pngName4_Path;

                CSV.Write<RecordModel>(Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date, "記帳.csv"), recordModel);
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
                System.Drawing.Image compressed = ImageCompressionUtility.CompressAndSaveImage(pictureAddress, 10L);
                System.Drawing.Image compressed2 = ImageCompressionUtility.CompressAndSaveImage(pictureAddress, 10L);
                //compressed.Save("C:\\Users\\TUF\\source\\repos\\test123.jpg");

                //compressed2.Save("C:\\Users\\TUF\\source\\repos\\test123.jpg");

                pictureBox1.Image = compressed;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.Image = compressed2;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                //compressed?.Dispose();
                //compressed = null;
                //compressed2?.Dispose();
                //compressed2 = null;

                //pictureBox1.Image.Save("test.jpg");
            }

            openFileDialog.Dispose();
            openFileDialog = null;
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();
            //Console.WriteLine($"Memory usage: {GC.GetTotalMemory(false)} bytes");
            //Console.WriteLine($"Total memory used by process: {Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024} MB");
        }

    }
}
