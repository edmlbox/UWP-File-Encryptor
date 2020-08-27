using App46.Encryption;
using App46.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace App46.Cryptography
{
    public class ChunkReader
    {
        public FileListView fileListView { get; set; }
        public ChunkReader(FileListView fileListView)
        {
            this.fileListView = fileListView;
        }

        async public Task ChunkRWAsync(FileCollection fileCollection, AppFolders folder, bool encrypt)
        {

             Cryptography cryptography = new Cryptography();
             StorageFile storageFile = (StorageFile)fileCollection.storageItem;
             bool encryptOperation = encrypt; 
             BasicProperties basicProperties = await storageFile.GetBasicPropertiesAsync();
             ulong fileSize = basicProperties.Size;
             string fileType = storageFile.FileType;
             uint chunkSize = 10000000;
             uint moduloReminder = (uint)fileSize % chunkSize;
             string fileName = storageFile.DisplayName + ".enc";
            
             ulong maxFileSize = basicProperties.Size;
             byte[] fileBytes = null;
             int count = 0;
             

           
            StorageFile fileToWriteChunks = await folder.filesFolder.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

            using (IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read))
            {
              ulong streamSize = stream.Size;

                using (var inputStream = stream.GetInputStreamAt(0))
                {
                    using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
                    {
                       
                        while (fileSize > 0)
                        {
                            if (fileListView.CancelEncryptionProcess) { stopEncryption(); break; }
                            await fileCollection.CoreDis((int)calcProgreeBarValue(fileSize, maxFileSize));
                            if (moduloReminder == 0)
                            {
                               count++;
                               fileBytes = new byte[chunkSize];
                               await dataReader.LoadAsync(chunkSize);
                               dataReader.ReadBytes(fileBytes);
                             

                               if (encryptOperation)
                                {
                                    await cryptography.Encryption(fileBytes, folder, fileName, fileType, fileToWriteChunks);
                                }
                                else
                                {
                                    cryptography.Decryption(fileBytes);
                                }
                                fileSize -= chunkSize;
                            }

                            else if (fileSize > chunkSize)
                            {
                                count++;
                              
                                  fileBytes = new byte[chunkSize];
                                  await dataReader.LoadAsync(chunkSize);
                                  dataReader.ReadBytes(fileBytes);
                                if (encryptOperation)
                                  {
                                      await cryptography.Encryption(fileBytes, folder, fileName, fileType, fileToWriteChunks);
                                  }
                                  else
                                  {
                                      cryptography.Decryption(fileBytes);
                                  }
                                fileSize -= chunkSize;
                            }

                            else if (fileSize <= chunkSize)
                            {
                                count++;
                                fileBytes = new byte[(uint)fileSize];
                                await dataReader.LoadAsync((uint)fileSize);
                                dataReader.ReadBytes(fileBytes);

                                if (encryptOperation)
                                {
                                    await cryptography.Encryption(fileBytes, folder, fileName, fileType, fileToWriteChunks);
                                }
                                else {
                                    cryptography.Decryption(fileBytes); }
                                    fileSize = 0;
                            }

                        }
                        if (!fileListView.CancelEncryptionProcess) { await fileCollection.CoreDisImg(storageFile); }

                      
                          


                    }
                }

            }

            double calcProgreeBarValue(ulong currentSize, ulong maxSize)
            {
                double progress = ((double)currentSize / (double)maxSize) * 100;
                double convertToProgressValue = 100 - progress;
                if (fileSize <= chunkSize) { convertToProgressValue = 100; }
                return convertToProgressValue;
            }
            async void stopEncryption()
            {
              await fileToWriteChunks.DeleteAsync();
            }


        }
        
       
        
    }
}
