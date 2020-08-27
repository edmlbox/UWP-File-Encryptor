using App46.Encryption;
using App46.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace App46.Cryptography
{
    public class StartEncryption
    {

        private ChunkReader chunkReader;
        private StorageFile storageFile;
        private FileListView fileListView;






        public StartEncryption(ViewModel.FileListView fileListView)
        {
            this.fileListView = fileListView;
            this.chunkReader = new ChunkReader(fileListView);
            this.storageFile = null;
            
        }



       async public Task BeginEncryption(ObservableCollection<FileCollection> fileCollections)
        {

            AppFolders appFolders = new AppFolders();
            await appFolders.initFolderStructure();

            await Task.Run(async () =>
            {
                foreach (FileCollection file in fileCollections)
                {
                    if (fileListView.CancelEncryptionProcess) { return; }
                    await chunkReader.ChunkRWAsync(file, appFolders, true);
                    
                };
                
            });

            fileListView.CancelEncryptionProcess = false;


















        }

       






    }
}
