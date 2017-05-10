using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace CameraApp_UWP
{
    public static class ApplicationService
    {
        public static async Task<WriteableBitmap> StorageFileToBitmapImage(StorageFile savedStorageFile)
        {
            using (IRandomAccessStream fileStream = await savedStorageFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
            {
                WriteableBitmap bitmapImage = new WriteableBitmap(100, 100);
                await bitmapImage.SetSourceAsync(fileStream);
                return bitmapImage;
            }
        }
        public static int[] MixImages(int[] indexes)
        {
            var random = new Random();
            int[] buf = new int[indexes.Count()];
            buf = indexes.OrderBy(x => random.Next()).ToArray();
            return buf;
        }
        public static bool WinAnalize(int[] i1)
        {
            for (int i = 0; i < i1.Count(); i++)
            {
                if (i1[i] != i)
                {
                    return false;
                }
            }
            return true;
        }
        //public static void SwapPictures(ImageView iv1, ImageView iv2)
        //{
        //    var getX = iv1.GetX();
        //    var getY = iv1.GetY();

        //    iv2.Layout((int)getX, (int)getY, (int)getX + iv2.Width, (int)getY + iv2.Height);
        //    iv1.Layout((int)iv2.GetX(), (int)iv2.GetY(), (int)iv2.GetX() + iv1.Width, (int)iv2.GetY() + iv1.Height);
        //}
        public static WriteableBitmap[] splitBitmap(StorageFile originalfile ,WriteableBitmap bitmap, int xCount, int yCount)
        {

            WriteableBitmap[] bitmaps = new WriteableBitmap[xCount * yCount];
            int width, height;
            
            width = bitmap.PixelWidth / xCount;
            height = bitmap.PixelHeight / yCount;
            int i = 0;
            Task<byte[]> convertTobyte = ConvertWriteableBitmapToByteArray(bitmap);
            for (int y = 0; y < xCount; ++y)
            {
                for (int x = 0; x < yCount; ++x)
                {
                    
                    //bitmaps[i] = task.Result;
                    //i++;
                }
            }

            return bitmaps;
        }
        public static async Task<byte[]> ConvertWriteableBitmapToByteArray(WriteableBitmap bitmap)
        {
            byte[] pixels;
            using (Stream stream = bitmap.PixelBuffer.AsStream())
            {
                pixels = new byte[(uint)stream.Length];
                await stream.ReadAsync(pixels, 0, pixels.Length);
            }

            return pixels;
        }
        public async static Task<BitmapImage> ImageFromBytes(Byte[] bytes)
        {
            BitmapImage image = new BitmapImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(bytes.AsBuffer());
                stream.Seek(0);
                await image.SetSourceAsync(stream);
            }
            return image;
        }
    }
}
