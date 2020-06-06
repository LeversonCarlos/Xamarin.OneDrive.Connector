using System;
using System.IO;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService
   {

      public async Task<Stream> Download(string fileID)
      {
         try
         {
            if (!await this.CheckConnectionAsync()) { return null; }
            var fileStream = System.IO.File.OpenRead(fileID);
            var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            await memoryStream.FlushAsync();
            memoryStream.Position = 0;
            return memoryStream;
         }
         catch (Exception ex) { throw new Exception($"Error while downloading file [{fileID}] with localDrive service", ex); }
      }

   }
}