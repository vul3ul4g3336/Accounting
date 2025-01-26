using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 記帳
{
    public static class ImageCompressionUtility
    {
        public static System.Drawing.Image CompressAndSaveImage(string filepath, long quality)
        {
            using (MemoryStream inputMs = new MemoryStream())
            {
                // 確保檔案讀取不會鎖定
                using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    fs.CopyTo(inputMs);
                }

                inputMs.Seek(0, SeekOrigin.Begin);
                using (Image image = Image.FromStream(inputMs)) // 確保 MemoryStream 是非鎖定的
                {
                    using (MemoryStream outputMs = new MemoryStream())
                    {
                        // 設定 JPEG 壓縮編碼器
                        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                        if (jpgEncoder == null)
                        {
                            throw new InvalidOperationException("無法找到 JPEG 編碼器。");
                        }

                        using (EncoderParameters encoderParams = new EncoderParameters(1))
                        {
                            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                            // 將圖片直接壓縮並存入 MemoryStream
                            image.Save(outputMs, jpgEncoder, encoderParams);
                        }

                        // 從 MemoryStream 重新載入壓縮後的圖片
                        outputMs.Seek(0, SeekOrigin.Begin);
                        // 返回深拷貝的圖片物件
                        using (Image tempImage = Image.FromStream(outputMs))
                        {
                            return new Bitmap(tempImage);
                        }
                    }
                }
            }
        }


        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
