using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SevenSegmentVideoToGraph
{
    public class ConversionHelper
    {
        /// Reference https://stackoverflow.com/questions/26260654/wpf-converting-bitmap-to-imagesource
        /// <summary>
        /// Takes a bitmap and converts it to an image that can be handled by WPF ImageBrush
        /// </summary>
        /// <param name="source">A bitmap image</param>
        /// <returns>The image as a BitmapImage for WPF</returns>
        public static BitmapImage ConvertToBitmapImage(Bitmap source)
        {
            MemoryStream memoryStream = new MemoryStream();
            source.Save(memoryStream, ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            memoryStream.Seek(0, SeekOrigin.Begin);
            image.StreamSource = memoryStream;
            image.EndInit();
            return image;
        }

        /// <summary>
        /// Takes a BitmapSource and converts it to a Bitmap
        /// </summary>
        /// <param name="bitmapSource">A bitmap source image</param>
        /// <returns>The image as a standard Bitmap</returns>
        public static Bitmap ConvertToBitmap(BitmapSource bitmapSource)
        {
            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;

            // The stride is the width of a single row of pixels (a scan line), rounded up to a four - byte boundary.
            // If the stride is positive, the bitmap is top - down. If the stride is negative, the bitmap is bottom - up.
            // https://docs.microsoft.com/en-us/dotnet/api/system.drawing.imaging.bitmapdata.stride
            int stride = width * ((bitmapSource.Format.BitsPerPixel + 7) / 8);

            IntPtr memoryBlockPointer = Marshal.AllocHGlobal(height * stride);
            bitmapSource.CopyPixels(new Int32Rect(0, 0, width, height), memoryBlockPointer, height * stride, stride);
            return new Bitmap(width, height, stride, PixelFormat.Format32bppPArgb, memoryBlockPointer);
        }
    }
}