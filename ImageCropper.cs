using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace CameraApp_UWP
{
    public static class ImageCropper
    {
        public static async Task<byte[]> CropImage(byte[] imageData, int reqWidth, int reqHeight)
        {

            var memStream = new MemoryStream(imageData);

            IRandomAccessStream imageStream = memStream.AsRandomAccessStream();
            var decoder = await BitmapDecoder.CreateAsync(imageStream);
            if (decoder.PixelHeight > reqHeight || decoder.PixelWidth > reqWidth)
            {
                using (imageStream)
                {
                    var resizedStream = new InMemoryRandomAccessStream();

                    BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);
                    double widthRatio = (double)reqWidth / decoder.PixelWidth;
                    double heightRatio = (double)reqHeight / decoder.PixelHeight;

                    double scaleRatio = Math.Min(widthRatio, heightRatio);

                    if (reqWidth == 0)
                        scaleRatio = heightRatio;

                    if (reqHeight == 0)
                        scaleRatio = widthRatio;

                    uint aspectHeight = (uint)Math.Floor(decoder.PixelHeight * scaleRatio);
                    uint aspectWidth = (uint)Math.Floor(decoder.PixelWidth * scaleRatio);

                    encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;

                    encoder.BitmapTransform.ScaledHeight = aspectHeight;
                    encoder.BitmapTransform.ScaledWidth = aspectWidth;

                    await encoder.FlushAsync();
                    resizedStream.Seek(0);
                    var outBuffer = new byte[resizedStream.Size];
                    await resizedStream.ReadAsync(outBuffer.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);
                    return outBuffer;
                }
            }
            return imageData;
        }
    }
}
