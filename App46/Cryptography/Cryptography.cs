using App46.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;


namespace App46.Cryptography
{
    public class Cryptography
    {

        String strAlgName = SymmetricAlgorithmNames.TripleDesEcbPkcs7;
        UInt32 keyLength = 128;
        CryptographicKey key; 
        ChunkWriter chunkWriter = new ChunkWriter();
        SymmetricKeyAlgorithmProvider objAlg;
        IBuffer keyMaterial;
        private bool isKeySaved = false;
        bool isFileConfigDataAdded = false;


        public Cryptography()
        {
            this.objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(strAlgName);
            this.keyMaterial = CryptographicBuffer.GenerateRandom(keyLength);
            key = objAlg.CreateSymmetricKey(keyMaterial);

        }


        async public Task Encryption(
            byte[] byteToBufferEncrypt, AppFolders folder, string fileName, String fileType, StorageFile fileToWriteChunks)
        {

            IBuffer buffertoEncrypt = byteToBufferEncrypt.AsBuffer();

            if (isKeySaved == false) {
                await SaveKey(keyMaterial, folder.privateKeysFolder, fileName);
                isKeySaved = true;
            }
           
            IBuffer buffEncrypt = CryptographicEngine.Encrypt(key, buffertoEncrypt, null);

           

            if(isFileConfigDataAdded == false)
            {
                string fileConfigData = FileConfigData(fileType, strAlgName);
                bool isWritten = await chunkWriter.WriteConfigToFile(fileToWriteChunks, fileConfigData);
                if (isWritten) { isFileConfigDataAdded = true; }


            }
            await chunkWriter.writeChunksToFile(buffEncrypt, fileToWriteChunks);


        }


         public void Decryption(
           byte[] byteToBufferDecrypt
          )
        {
            SymmetricKeyAlgorithmProvider objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(strAlgName);
            IBuffer buffertoDecrypt = byteToBufferDecrypt.AsBuffer();
            IBuffer buffDecrypted = CryptographicEngine.Decrypt(key, buffertoDecrypt, null);

        }

        //Save private key to file
        async public Task SaveKey(IBuffer x, StorageFolder folder, string fileName)
        {
            StorageFile storageFile = null; ;
            string content = CryptographicBuffer.EncodeToBase64String(x);

            string fileNameTXT = StringManipulation.subString(fileName,'.');
            fileNameTXT = fileNameTXT + "_PrivateKey.txt";
            storageFile = await folder.CreateFileAsync(fileNameTXT, CreationCollisionOption.GenerateUniqueName);
            if (storageFile != null) { await FileIO.WriteTextAsync(storageFile, content); }
             

        }

         public string FileConfigData(string fileType, String strAlgName)
        {
            string combine = "FileType*" + fileType + "*" + "AlgUsed*" + strAlgName + "*";
            
            return combine;
        }
    }
}
