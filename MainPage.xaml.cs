using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Graphics;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace CameraApp_UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static StorageFile file;
        WriteableBitmap bitmap;
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private async void selectBtn_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".png");
            file = await openPicker.PickSingleFileAsync();
            bitmap = await ApplicationService.StorageFileToBitmapImage(file);
            imageview.Source = bitmap;
        }
        
        

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage), file);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task<BitmapImage> task = ApplicationService.ImageFromBytes(ImageCropper.CropImage(ApplicationService.ConvertWriteableBitmapToByteArray(bitmap).Result, 50, 50).Result);
            imageview.Source = task.Result;
        }
    }
}
