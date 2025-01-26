using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 記帳.Forms
{
    public partial class PictureDialog : Form
    {

        public PictureDialog(string path)
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(path);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void PictureDialog_FormClosing(object sender, FormClosingEventArgs e)
        {

            pictureBox1.Image.Dispose();
        }
    }
}
