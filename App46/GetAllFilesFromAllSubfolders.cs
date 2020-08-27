using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.Storage.FileProperties;

namespace App46
{
    public class FileStorageCollection
    {
        public bool isStoped { get; set; } = false;
        public  ObservableCollection<FileCollection> storageItems = new ObservableCollection<FileCollection>();
        
        async public Task AddElementsToSharedCollection(IReadOnlyList<StorageFile> multiple_files)
        {
           
            
            foreach (StorageFile file in multiple_files)
            {
                if (isStoped) { break; }
                bool skipTheFile = await doSkipTheFile(file);
               
                if (skipTheFile) { continue; }

                bool isInCollection = isCollectionContainsStorageFile(file);
                if (isInCollection) { continue; }

                storageItems.Add(new FileCollection() {
                        storageItem = file,
                        Bitmap = await FileCollection.GetFileIcon(file),
                        fileSize = await FileCollection.GetFileSize(file),
                        FileName = file.Name
                });
            }
           
        }
      

        public  bool isCollectionContainsStorageFile(StorageFile file)
        {
            if(storageItems.Count < 1) { return false; };
            foreach (FileCollection x in storageItems)
            {
                if(((StorageFile)x.storageItem).FolderRelativeId == file.FolderRelativeId)
                {
                    return true;
                }
            }
            return false;
        }

        async public Task<bool> doSkipTheFile(StorageFile file)
        {

            BasicProperties props = await file.GetBasicPropertiesAsync();
            ulong FileSize = props.Size;

            if(FileSize == 0) {
                return true; }
            else {
                return false; }
        }

    }
}
