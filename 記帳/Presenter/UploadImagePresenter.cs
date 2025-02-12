using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static 記帳.Contract.UploadImageContract;
using static 記帳.Contract.記一筆Contract;

namespace 記帳.Presenter
{
    internal class UploadImagePresenter : UploadImageIPresenter
    {
        private UploadImageIView _view;
        public UploadImagePresenter(UploadImageIView view)
        {
            _view = view;
        }
        public void CompressImageRequest(string imagePath)
        {
            System.Drawing.Image compressed = ImageCompressionUtility.CompressAndSaveImage(imagePath, 10L);
            System.Drawing.Image compressed2 = ImageCompressionUtility.CompressAndSaveImage(imagePath, 10L);
            //compressed.Save("C:\\Users\\TUF\\source\\repos\\test123.jpg");
            _view.CompressResponse(compressed);
            //compressed2.Save("C:\\Users\\TUF\\source\\repos\\test123.jpg");


            //compressed?.Dispose();
            //compressed = null;
            //compressed2?.Dispose();
            //compressed2 = null;

            //pictureBox1.Image.Save("test.jpg");
        }
    }
}
