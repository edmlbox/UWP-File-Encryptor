using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Core;
// Iterate over all subfolders and create a list of all files and return a list;
namespace App46
{
    class RetrieveAllFilesFromAllFOlders
    {
        public bool isStoped { get; set; } = false;


        async public Task GetAllFilesFromAllSubfolders(
            IReadOnlyList<IStorageItem> items, 
            FileStorageCollection fileStorageCollection)
        {
             
                foreach (IStorageItem item in items)
                {
                if (isStoped) { break; }
              

                if (item.IsOfType(StorageItemTypes.File))
                    {

                    bool isInCollection = fileStorageCollection.isCollectionContainsStorageFile((StorageFile)item);
                    if (isInCollection) { continue; }

                    bool skipTheFile = await doSkipTheFile(item);

                    if (skipTheFile) { continue; }


                    fileStorageCollection.storageItems.Add(new FileCollection()
                    {
                        storageItem = item,
                        Bitmap = await FileCollection.GetFileIcon(item),
                        fileSize = await FileCollection.GetFileSize(item),
                        FileName = item.Name
                    }); ;
                    }

                    else if (item.IsOfType(StorageItemTypes.Folder))
                    {
                        StorageFolder storageFolder = (StorageFolder)item;
                        IReadOnlyList<IStorageItem> list = await storageFolder.GetItemsAsync();
                        await GetAllFilesFromAllSubfolders(list, fileStorageCollection);
                    }

                }
              

        }


        async public Task<bool> doSkipTheFile(IStorageItem file)
        {

            BasicProperties props = await file.GetBasicPropertiesAsync();
            ulong FileSize = props.Size;

            if (FileSize == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
