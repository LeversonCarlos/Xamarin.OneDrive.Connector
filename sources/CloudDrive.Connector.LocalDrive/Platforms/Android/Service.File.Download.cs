using System;
using System.IO;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService
   {

      public async Task<byte[]> Download(string fileID)
      {
         try
         {
            if (!await this.CheckConnectionAsync()) { return null; }
            using (var fileStream = new FileStream(fileID, FileMode.Open, FileAccess.Read))
            {
               var fileLength = (int)fileStream.Length;
               var fileBytes = new byte[fileLength];
               await fileStream.ReadAsync(fileBytes, 0, fileLength);
               return fileBytes;
            }
         }
         catch (Exception) { throw; }
      }

   }
}