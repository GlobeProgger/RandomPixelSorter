using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RandomPixelSorter.Lib
{
    /// <summary>
    /// Contains helper methods
    /// </summary>
    public static class Herlpers
    {
        /// <summary>
        /// Creates an empty and initialized <paramref name="WriteableBitmap"/>
        /// </summary>
        /// <param name="width">The width of the <paramref name="WriteableBitmap"/></param>
        /// <param name="height">The height of the <paramref name="WriteableBitmap"/></param>
        /// <returns>The fully initialized <paramref name="WriteableBitmap"/></returns>
        public static WriteableBitmap CreateBitmapSource(double width, double height)
        {
            var bitmap = new Bitmap((int)width, (int)height);
            var image = new BitmapImage();
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                stream.Position = 0;

                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
            }

            return new WriteableBitmap(
                new FormatConvertedBitmap(
                    image, PixelFormats.Default, null, 0));
        }
    }
}
