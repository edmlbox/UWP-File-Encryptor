using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;

namespace App46
{
   public static class ShowFileInFolder
    {
        //Show/locate file in file explorer
        async public static void Show(IStorageItem item)
        {
           string path = item.Path;
           string finished_path = StringManipulation.subString(path, '\\');
           StorageFile storageFile = (StorageFile)item;
           await Launcher.LaunchFolderPathAsync(finished_path);

        }

    }
}
