using CSV_Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Models;

namespace 記帳.ExtensionFile
{
    internal static class DataGridViewExtension
    {
        public static void ImageDispose(this DataGridView dataGridView)
        {
            dataGridView.Columns.Clear();
            dataGridView.DataSource = null;
            GC.Collect();
        }

    }
}
