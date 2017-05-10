using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace CameraApp_UWP
{
    public class ImageClass
    {
        public int id;
        BitmapImage image;

        public ImageClass(BitmapImage b, int _id)
        {
            image = b;
            id = _id;
        }
    }
}
