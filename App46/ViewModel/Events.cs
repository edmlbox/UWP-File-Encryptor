using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using App46.Cryptography;

namespace App46.ViewModel
{
    public class Events:Notify
    {
        private RetrieveAllFilesFromAllFOlders createListOfFiles { get; set; }
        private FileListView FileListView;
        private ObservableCollection<FileCollection> fileCollections;
        private IList<object> selectedItems;
        public FileStorageCollection fileStorageCollection { get; set; }
        public FileCollection FileCollection { get; set; }
        private Visibility _visibility { get; set; } = Visibility.Collapsed;
        public Visibility visibility { get { return _visibility; } set { _visibility = value; OnChange("visibility"); } }
       


        public Events(FileListView FileListView, FileStorageCollection fileStorageCollection)
        {
            this.FileListView = FileListView;
            this.fileStorageCollection = fileStorageCollection;
            this.fileCollections = fileStorageCollection.storageItems;
            this.createListOfFiles = new RetrieveAllFilesFromAllFOlders();
            FileCollection = FileListView.fileCollection;
        }
      
      


        public void OnCollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<FileCollection> fileCollections1 = new ObservableCollection<FileCollection>();
            fileCollections1 = (ObservableCollection<FileCollection>)sender;

            UIControlsView uIControlsView = new UIControlsView();
            uIControlsView.ShowHideEnableDisableUIControls(fileCollections1, FileListView);


        }

        async public void ClickSingleFolderEvent(object sender, RoutedEventArgs e)
        {
            await GetSingleFolderAsync();
        }



        async public void ClickMultiplyFilesEvent(object sender, RoutedEventArgs e)
        {
            fileStorageCollection.isStoped = false;
            IReadOnlyList<StorageFile> multiple_files = await GetMultiplyFilesASync();
            visibility = Visibility.Visible;
            await fileStorageCollection.AddElementsToSharedCollection(multiple_files);
            visibility = Visibility.Collapsed;
        }



        public void ClickCloseEvent(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }



        public void ClearListClickEvent(object sender, RoutedEventArgs e)
        {
            fileStorageCollection.storageItems.Clear();
        }

        
        //Drag And Drop
        public void Grid_DragOverEvent(object sender, DragEventArgs e)
        {
            //DragUI Settings
            e.DragUIOverride.IsCaptionVisible = false;
            e.DragUIOverride.IsGlyphVisible = false;
            e.DragUIOverride.IsContentVisible = true;
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Link;
        }

       
        //Drag And Drop
        async public void Grid_DropEvent(object sender, DragEventArgs e)
        {
            createListOfFiles.isStoped = false;
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                IReadOnlyList<IStorageItem> items = await e.DataView.GetStorageItemsAsync();
                createListOfFiles = new RetrieveAllFilesFromAllFOlders();
                visibility = Visibility.Visible;
                await createListOfFiles.GetAllFilesFromAllSubfolders(items, fileStorageCollection);
                visibility = Visibility.Collapsed;
            }
        }



        public void OpenFolderEvent(object sender, RoutedEventArgs e)
        {
            AppBarButton appBarButton = (AppBarButton)sender;
            FileCollection fileCollectionItem = (FileCollection)appBarButton.DataContext;
            IStorageItem storage = fileCollectionItem.storageItem;
            ShowFileInFolder.Show(storage);
        }



        public void RemoveFromListEvent(object sender, RoutedEventArgs e)
        {
            AppBarButton appBarButton = (AppBarButton)sender;
            FileCollection fileCollectionItem = (FileCollection)appBarButton.DataContext;
            fileStorageCollection.storageItems.Remove(fileCollectionItem);
        }



        public void ListFiles_SelectionChangedEvent(object sender, SelectionChangedEventArgs e)
        {

            IList<FileCollection> selectedObjects = e.AddedItems.Cast<FileCollection>().ToList();


            foreach(FileCollection file in fileCollections)
            {
                string relativeID1 = ((StorageFile)file.storageItem).FolderRelativeId;


                foreach(FileCollection file1 in selectedObjects)
                {
                    string relativeID2 = ((StorageFile)file1.storageItem).FolderRelativeId;
                    if(relativeID2 == relativeID1) { file.IsSelected = true; }
                }


            }


            ListView listViewItem = (ListView)sender;
            selectedItems = listViewItem.SelectedItems;
            int selectedNumber =  selectedItems.Count;
            FileListView.SelectedNumber = selectedNumber;



            if (selectedNumber > 0) {
                FileListView.RemoveSelected = true;
                FileListView.DeselectAll = true; }
            else if (selectedNumber == 0) {
                FileListView.RemoveSelected = false;
                FileListView.DeselectAll = false; }



        }



        public void RemoveSelectedItemsEvent(object sender, RoutedEventArgs e)
        {
            foreach (object i in selectedItems.ToList())
            {
                FileCollection name = (FileCollection)i;
                fileCollections.Remove(name);
            }

        }



        public void SelectAllItems(object sender, RoutedEventArgs e)
        {

            foreach(FileCollection file in fileCollections)
            {
                file.IsSelected = true;
            }


        }



       public void DeSelectAll(object sender, RoutedEventArgs e)
       {
            foreach (FileCollection file in fileCollections)
            {
                if (file.IsSelected) { file.IsSelected = false; }
            }
        }


        async public Task GetSingleFolderAsync()
        {
            createListOfFiles.isStoped = false;
            IReadOnlyList<IStorageItem> listofFiles = null;
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            folderPicker.FileTypeFilter.Add("*");
            StorageFolder storageFolder = await folderPicker.PickSingleFolderAsync();

            if (storageFolder != null)
            {
                listofFiles = await storageFolder.GetItemsAsync();
                //Get all files from all Subfolders

                visibility = Visibility.Visible;
                await createListOfFiles.GetAllFilesFromAllSubfolders(listofFiles, fileStorageCollection);
                visibility = Visibility.Collapsed;
            }


        }

        async public static Task<IReadOnlyList<StorageFile>> GetMultiplyFilesASync()
        {
            FileOpenPicker filePicker = new FileOpenPicker();

            filePicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            filePicker.FileTypeFilter.Add("*");
            
            IReadOnlyList<StorageFile> listofFiles = await filePicker.PickMultipleFilesAsync();
            
            return listofFiles;

        }


       

        public void StopAddingItemsToCollection(object sender, RoutedEventArgs e)
        {
            createListOfFiles.isStoped = true;
            fileStorageCollection.isStoped = true;
        }

        public void MenuSelectMultiplyItems_Click(object sender, RoutedEventArgs e)
        {
            FileListView.ListViewSelectionMode = (FileListView.ListViewSelectionMode == ListViewSelectionMode.Multiple) ? 
                ListViewSelectionMode.Extended : ListViewSelectionMode.Multiple;

        
        }

        async public void MenuFlyoutItemEncryptAll(object sender, RoutedEventArgs e)
        {
          
            StartEncryption startEncryption = new StartEncryption(FileListView);
            FileListView.IsEncryptionActive = true;
            FileListView.EncryptAll = false;
            FileListView.DisableMenu();
            await startEncryption.BeginEncryption(fileCollections);
            FileListView.IsEncryptionActive = false;
            FileListView.EncryptAll = true;


        }

        public void MenuFlyoutItemStopEncryption(object sender, RoutedEventArgs e)
        {
            FileListView.CancelEncryptionProcess = true;
            FileListView.ShowMenu();
        }


        public void MenuFlyoutItemSettingPage(object sender, RoutedEventArgs e)
        {

            SettingsView settingsView = new SettingsView();
            settingsView.settingsPageInit();


        }

    }
}
