using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace App46.Encryption
{
    public class AppFolders
    {

        private StorageFolder folder = null;
        private string errorMSG;

        public StorageFolder encryptionAppFolder { get; private set; }
        public StorageFolder encryptedFilesFolder { get; private set; }
        public StorageFolder decryptedFilesFolder { get; private set; }
        public StorageFolder filesFolder { get; private set; }
        public StorageFolder privateKeysFolder { get; private set; }



        public StorageFolder getLocalFolder()
        {
         
            try { folder = Windows.Storage.ApplicationData.Current.LocalFolder; }
            catch (Exception e)
            {
                errorMSG = e.Message;
            }
           
            return folder;
        }


        async public Task<StorageFolder> createEncryptionAppFolder(StorageFolder localFolder)
        {
           
            try
            {
                encryptionAppFolder = await localFolder.CreateFolderAsync("EncryptionApp", CreationCollisionOption.FailIfExists);
            }
            catch (Exception e)
            {
                errorMSG = e.Message;
            }

            return encryptionAppFolder;


        }


        async public Task<StorageFolder> createEncryptedFilesFolder(StorageFolder EncryptionApp)
        {
            try
            {
                encryptedFilesFolder = await EncryptionApp.CreateFolderAsync("EncryptedFiles", CreationCollisionOption.FailIfExists);
            }
            catch (Exception e)
            {
                errorMSG = e.Message;
            }
            return encryptedFilesFolder;


        }


        async public Task<StorageFolder> createDecryptedFilesFolder(StorageFolder EncryptionAppFolder)
        {
            try
            {

                decryptedFilesFolder = await EncryptionAppFolder.CreateFolderAsync("DecryptedFiles", CreationCollisionOption.FailIfExists);
            }
            catch (Exception e)
            {
                errorMSG = e.Message;
            }

            return decryptedFilesFolder;

        }


        async public Task<StorageFolder> getEncryptedFolder(StorageFolder EncryptionAppFolder)
        {
            
            try
            {
                encryptedFilesFolder = await EncryptionAppFolder.GetFolderAsync("EncryptedFolder");
            }
            catch (Exception e)
            {
                errorMSG = e.Message;
            }

            return encryptedFilesFolder;
        }


        async public Task<StorageFolder> getDecryptedFolder(StorageFolder EncryptionApp)
        {
         
            try
            {
                decryptedFilesFolder = await EncryptionApp.GetFolderAsync("EncryptedFolder");
            }

            catch (Exception e)
            {
                errorMSG = e.Message;
            }

            return decryptedFilesFolder;
        }


        async public Task<bool> isFolderExists(StorageFolder folderToCheck,  string folderName)
        {
            try
            {
                folder = await folderToCheck.GetFolderAsync(folderName);
            }
            catch (Exception e) { errorMSG = e.Message; folder = null; }

            if(folder!= null) { return true; } else { return false; }
        }


        async public Task<StorageFolder> getEncryptionAppFolder(StorageFolder localFolder)
        {
            try
            {
                encryptionAppFolder = await localFolder.GetFolderAsync("EncryptionApp");
            }
            catch (Exception e)
            {
                errorMSG = e.Message;
            }
            return encryptionAppFolder;
        }


        async public Task initFolderStructure()
        {

            StorageFolder localFolder = getLocalFolder();
           

            bool folderExists = await isFolderExists(localFolder, "EncryptionApp");

            if (folderExists)
            {

                StorageFolder EncryptionAppFolder = await getEncryptionAppFolder(localFolder);

                bool isfolderEncryptedFilesExists = await isFolderExists(EncryptionAppFolder, "EncryptedFiles");
                bool isfolderDecryptedFilesExists = await isFolderExists(EncryptionAppFolder, "DecryptedFiles");




                if (isfolderEncryptedFilesExists == false)
                {
                    encryptedFilesFolder = await createEncryptedFilesFolder(EncryptionAppFolder);

                    filesFolder = await createFilesFolder(encryptedFilesFolder);
                    privateKeysFolder = await createPrivateKeyFolder(encryptedFilesFolder);

                }
                else
                {
                    encryptedFilesFolder = await EncryptionAppFolder.GetFolderAsync("EncryptedFiles");

                    bool isFilesFolderExists = await isFolderExists(encryptedFilesFolder, "Files");
                    bool isPrivateKeysFolderExists = await isFolderExists(encryptedFilesFolder, "PrivateKeys");

                    if (isFilesFolderExists == false) { filesFolder = await createFilesFolder(encryptedFilesFolder); }
                    else { filesFolder = await encryptedFilesFolder.GetFolderAsync("Files"); }
                    if (isPrivateKeysFolderExists == false) { privateKeysFolder = await createPrivateKeyFolder(encryptedFilesFolder); }
                    else { privateKeysFolder = await encryptedFilesFolder.GetFolderAsync("PrivateKeys"); }

                   
                   


                }


                if (isfolderDecryptedFilesExists == false)
                {
                    decryptedFilesFolder = await createDecryptedFilesFolder(EncryptionAppFolder);
                    
                }
                else
                {
                    decryptedFilesFolder = await EncryptionAppFolder.GetFolderAsync("DecryptedFiles");
                }

            }
            else
            {
                StorageFolder EncryptionAppFolder = await createEncryptionAppFolder(localFolder);

                encryptedFilesFolder =  await createEncryptedFilesFolder(EncryptionAppFolder);
                decryptedFilesFolder = await createDecryptedFilesFolder(EncryptionAppFolder);

                filesFolder = await createFilesFolder(encryptedFilesFolder);
                privateKeysFolder =  await createPrivateKeyFolder(encryptedFilesFolder);

            }
          

        }


        async public Task<StorageFolder> createFilesFolder(StorageFolder localFolder)
        {
            try
            {
                filesFolder = await localFolder.CreateFolderAsync("Files", CreationCollisionOption.FailIfExists);
            }
            catch (Exception e)
            {
                errorMSG = e.Message;
            }

            return filesFolder;
        }


        async public Task<StorageFolder> createPrivateKeyFolder(StorageFolder localFolder)
        {
            try
            {
                privateKeysFolder = await localFolder.CreateFolderAsync("PrivateKeys", CreationCollisionOption.FailIfExists);
            }
            catch (Exception e)
            {
                errorMSG = e.Message;
            }

            return privateKeysFolder;
        }


    }
}
