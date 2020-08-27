using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Diagnostics;
using App46.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace App46.ViewModel
{
    public class FileListView : Notify
    {
        public ViewText ViewText { get; set; }
        public Events Events { get; set; }
        public Selection Selection { get; set; }
        public FileCollection fileCollection { get; set; }

        private bool isEncryptionActive { get; set; } = false;
        public bool IsEncryptionActive { get { return isEncryptionActive; } set { isEncryptionActive = value; OnChange("IsEncryptionActive"); } }

        private bool cancelEncryptionProcess { get; set; } = false;
        public bool CancelEncryptionProcess
        {
            get { return cancelEncryptionProcess; }
            set { cancelEncryptionProcess = value; OnChange("CancelEncryptionProcess"); }
        }

        private Visibility listViewVisibility { get; set; } = Visibility.Collapsed;
        public Visibility ListViewVisibility { get { return listViewVisibility; } set { listViewVisibility = value;
                OnChange("ListViewVisibility"); } }

        private Visibility relativePanelVisibility { get; set; } = Visibility.Visible;
        public Visibility RelativePanelVisibility
        {
            get { return relativePanelVisibility; }
            set
            {
                relativePanelVisibility = value;
                OnChange("RelativePanelVisibility");
            }
        }

        private bool encryptAll { get; set; } = false;
        public bool EncryptAll
        {
            get { return encryptAll; }
            set
            {
                encryptAll = value;
                OnChange("EncryptAll");
            }
        }

        public string sName { get; set; }


        public FileStorageCollection fileStorageCollection { get; set; }

        private ListViewSelectionMode _listViewSelectionMode { get; set; } = ListViewSelectionMode.Extended;
        private Visibility isRemoveButtonVisible { get; set; } = Visibility.Collapsed;


        private bool _RemoveAll { get; set; }
        private bool _RemoveSelected { get; set; } 
        private bool _SelectMultiply { get; set; } 
        private bool _SelectAll { get; set; } 
        private bool _DeselectAll { get; set; } 

       
        




        public bool RemoveAll { get { return _RemoveAll; } set { _RemoveAll = value; OnChange("RemoveAll"); } }
        public bool RemoveSelected { get { return _RemoveSelected; } set { _RemoveSelected = value; OnChange("RemoveSelected"); } }
        public bool SelectMultiply { get { return _SelectMultiply; } set { _SelectMultiply = value; OnChange("SelectMultiply"); } }
        public bool SelectAll { get { return _SelectAll; } set { _SelectAll = value; OnChange("SelectAll"); } }
        public bool DeselectAll { get { return _DeselectAll; } set { _DeselectAll = value; OnChange("DeselectAll"); } }
        public ListViewSelectionMode ListViewSelectionMode { get { return _listViewSelectionMode; } set { _listViewSelectionMode = value;OnChange("ListViewSelectionMode"); } }
        public Visibility IsRemoveButtonVisible { get { return isRemoveButtonVisible; } set { isRemoveButtonVisible = value; OnChange("IsRemoveButtonVisible"); } }

        private int _SelectedNumber { get; set; } = 0;
        public int SelectedNumber
        {
            get { return _SelectedNumber; }
            set { _SelectedNumber = value; OnChange("SelectedNumber"); }
        }


        public FileListView()
        {
            fileStorageCollection = new FileStorageCollection();
            ViewText = new ViewText();
            Events = new Events(this, fileStorageCollection);
            Selection = new Selection();
            fileCollection = new FileCollection();
            fileStorageCollection.storageItems.CollectionChanged += Events.OnCollectionChanged;


            RemoveAll = Selection._RemoveAll;
            RemoveSelected = Selection._RemoveSelected;
            SelectMultiply = Selection._SelectMultiply;
            SelectAll = Selection._SelectAll;
            DeselectAll = Selection._DeselectAll;

           

        }

        public void DisableMenu()
        {
            RemoveAll = false;
            RemoveSelected = false;
            SelectMultiply = false;
            DeselectAll = false;
            SelectAll = false;

        }

        public void ShowMenu()
        {
            RemoveAll = true;
            RemoveSelected = false;
            SelectMultiply = true;
            DeselectAll = false;
            SelectAll = true;
        }

       



















    }
}
