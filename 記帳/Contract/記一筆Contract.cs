using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳.Models;

namespace 記帳.Contract
{
    internal class 記一筆Contract
    {
        public interface 記一筆IView
        {

        }
        public interface 記一筆IPresenter
        {
            void saveData(RecordModel recordModel, Image image1, Image image2);
        }
    }
}
