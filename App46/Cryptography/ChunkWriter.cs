using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace App46.Cryptography
{
    public class ChunkWriter
    {

        async public Task writeChunksToFile(IBuffer tempArray, StorageFile fileToWriteChunks)
        {

            byte[] writeToDisk = tempArray.ToArray();
            
            using (Stream x = await fileToWriteChunks.OpenStreamForWriteAsync())
            {
                x.Seek(0, SeekOrigin.End);
                await x.WriteAsync(writeToDisk, 0, writeToDisk.Length);

            }


        }

       async public Task<bool> WriteConfigToFile(StorageFile fileToWriteConfig, string data)
        {
             await FileIO.WriteTextAsync(fileToWriteConfig, data);

            return true;

        }



    }
}
