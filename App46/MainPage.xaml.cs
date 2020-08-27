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
using Windows.Storage.Pickers;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.ApplicationModel.Core;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI;
using App46.ViewModel;




namespace App46
{

    public sealed partial class MainPage : Page
    {
        FileListView FileListView;

        public MainPage()
        {

          this.InitializeComponent();
          FileListView = new FileListView();

        }

        public void Button_Click_Remove_El_Event(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            FileCollection fileCollection = (FileCollection)button.DataContext;
            FileListView.fileStorageCollection.storageItems.Remove(fileCollection);

        }

      
    }
}
