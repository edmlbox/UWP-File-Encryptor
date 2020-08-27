using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace App46
{
    public class FileCollection:ViewModel.Notify
    {
        private bool _isSelected { get; set; }
        public IStorageItem storageItem { get; set; } = null;
        private BitmapImage bitmap { get; set; }
        public BitmapImage Bitmap { get { return bitmap; } set { bitmap = value; OnChange("Bitmap"); } }
        public string fileSize { get; set; }
        private string fileName { get; set; }
        public string FileName { get { return fileName; } set { fileName = value;OnChange("FileName"); } }
        private Visibility isButtonVisible { get; set; } = Visibility.Collapsed;
        private int progressBarValue { get; set; } = 0;
      

        private Visibility progressBarVisibility { get; set; } = Visibility.Collapsed;
        public Visibility ProgressBarVisibility { get { return progressBarVisibility; } set { progressBarVisibility = value; OnChange("ProgressBarVisibility"); } }
        public int ProgressBarValue { get { return progressBarValue; }set { progressBarValue = value;
                if(ProgressBarVisibility == Visibility.Collapsed) { ProgressBarVisibility = Visibility.Visible; }
                OnChange("ProgressBarValue"); } }


        async public Task CoreDis(int number)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
          {
              ProgressBarValue = number;

          });
        }

        async public Task CoreDisImg(StorageFile storageFile)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {

                BitmapImage lockImage = new BitmapImage();
                Uri uri = new Uri("ms-appx:///Assets/lock.png");
                lockImage.UriSource = uri;
                lockImage.DecodePixelHeight = 50;
                lockImage.DecodePixelWidth = 50;
                Bitmap = lockImage;

                string encName = storageFile.DisplayName + ".enc";
                FileName = encName;

            });
        }
        

        static readonly string[] SizeSuffixes = 
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public bool IsSelected { get { return this._isSelected; } set { _isSelected = value;OnChange("IsSelected"); } }
        public Visibility IsButtonVisible { get { return isButtonVisible; } set { isButtonVisible = value; OnChange("IsButtonVisible"); } }

        //Gets ThumbnailIcon
        async public static Task<BitmapImage> GetFileIcon(IStorageItem storageItem)
        {
            StorageFile storageFile = (StorageFile)storageItem;
            StorageItemThumbnail thumbnail = await storageFile.GetThumbnailAsync(ThumbnailMode.SingleItem, 50);
            BitmapImage image = new BitmapImage();
            image.SetSource(thumbnail);
            return image;
        }

       
        async public static Task<string> GetFileSize(IStorageItem storageItem)
        {
            StorageFile storageFile = (StorageFile)storageItem;
            BasicProperties basicProperties = await storageFile.GetBasicPropertiesAsync();
            ulong fileSizeinBytes = basicProperties.Size;

            string res = SizeSuffix((long)fileSizeinBytes, 1);

            return res;

        }


        static string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }
           
            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));
            
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }


        public void ListViewItem_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            IsButtonVisible = Visibility.Visible;

        }

        public void ListViewItem_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            IsButtonVisible = Visibility.Collapsed;



        }

       



    }
}
