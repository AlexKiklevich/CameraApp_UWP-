using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;

namespace CameraApp_UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private static int count = 3;
        int[] NewA, OldA;
        WriteableBitmap[] _cutIV;
        WriteableBitmap[] Buf_cutIV;
        private int height;
        private int width;
        public GamePage()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFile content = e.Parameter as StorageFile;
            WriteableBitmap corefile = await ApplicationService.StorageFileToBitmapImage((StorageFile)content);
            startBtn.Click += ClickStart;
            _cutIV = new WriteableBitmap[count * count];
            Buf_cutIV = new WriteableBitmap[count * count];
            NewA = new int[count * count];
            OldA = new int[count * count];

            var bitmaps = ApplicationService.splitBitmap(content,corefile, count, count);

            //for (int i = 0; i < bitmaps.Length; i++)
            //{
            //    _cutIV[i] = new WriteableBitmap(100,100);
            //    _cutIV[i] = bitmaps[i];
            //}
            //for (int i = 0; i < _cutIV.Count(); i++)
            //{
            //    OldA[i] = i;
            //}

            //NewA = ApplicationService.MixImages(OldA);
            //for (int i = 0; i < _cutIV.Count(); i++)
            //{
            //    Buf_cutIV[i] = _cutIV[NewA[i]];
            //}
        }

        private void ClickStart(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
