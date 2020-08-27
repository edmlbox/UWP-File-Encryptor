using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace App46.ViewModel
{
    public class UIControlsView
    {


        public void ShowHideEnableDisableUIControls(
            ObservableCollection<FileCollection> fileCollections1, FileListView FileListView)
        {
            if (fileCollections1.Count > 0)
            {
                FileListView.EncryptAll = true;
                FileListView.RelativePanelVisibility = Visibility.Collapsed;
                FileListView.ListViewVisibility = Visibility.Visible;
                FileListView.SelectMultiply = true;
                FileListView.RemoveAll = true;
                FileListView.SelectAll = true;
                FileListView.RemoveSelected = false;
            }
            else if (fileCollections1.Count == 0)
            {
                FileListView.EncryptAll = false; ;
                FileListView.RelativePanelVisibility = Visibility.Visible;
                FileListView.ListViewVisibility = Visibility.Collapsed;
                FileListView.SelectMultiply = false;
                FileListView.SelectAll = false;
                FileListView.RemoveAll = false;
            }
        }
        
    }
}
