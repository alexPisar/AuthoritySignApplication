using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace AuthoritySignClient.Utils
{
    public static class ByteImageUtil
    {
        public static BitmapImage ConvertBytesToBitmap(byte[] bytes, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            var imageConverter = new ImageConverter();
            Bitmap bm = (Bitmap)imageConverter.ConvertFrom(bytes);

            MemoryStream ms = new MemoryStream();
            bm.Save(ms, imageFormat);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
