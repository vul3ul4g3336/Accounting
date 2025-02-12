using CSV_Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Models;
using static 記帳.Contract.記一筆Contract;

namespace 記帳.Presenter
{
    internal class 記一筆Presenter : 記一筆IPresenter
    {
        private 記一筆IView _view;
        public 記一筆Presenter(記一筆IView view)
        {
            _view = view;
        }

        public void saveData(RecordModel recordModel, Image originImage1, Image originImage2)
        {
            string pngName1_Path = Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date, $"{Guid.NewGuid().ToString()}.png");
            string pngName2_Path = Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date, $"{Guid.NewGuid().ToString()}.png");
            string pngName3_Path = Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date, $"{Guid.NewGuid().ToString()}.png");
            string pngName4_Path = Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date, $"{Guid.NewGuid().ToString()}.png");
            originImage1.Save(pngName1_Path);
            originImage2.Save(pngName2_Path);

            Bitmap image = new Bitmap(originImage1, 30, 30);
            Bitmap image2 = new Bitmap(originImage2, 30, 30);

            image.Save(pngName3_Path);
            image2.Save(pngName4_Path);
            recordModel.PictureAddress1 = pngName1_Path;
            recordModel.PictureAddress2 = pngName2_Path;
            recordModel.resizedPictureAddress1 = pngName3_Path;
            recordModel.resizedPictureAddress2 = pngName4_Path;

            CSV.Write<RecordModel>(Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], recordModel.Date, "記帳.csv"), recordModel);
        }
    }

}
