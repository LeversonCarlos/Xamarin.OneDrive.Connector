using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService
   {

      public async Task<FileVM> Upload(string fileID, byte[] fileContent)
      {
         try
         {
            if (!await this.ConnectAsync()) { return null; }

            if (File.Exists(fileID)) { File.Delete(fileID); }
            await Task.Run(() => File.WriteAllBytes(fileID, fileContent));

            if (!File.Exists(fileID)) { return null; }
            return await this.GetDetails(fileID);
         }
         catch (Exception ex) { throw new Exception($"Error while uploading file [{fileID}] with localDrive service", ex); }
      }

      public Task<FileVM> Upload(string directoryID, string fileName, byte[] fileContent)
      { return this.Upload($"{directoryID}{Path.DirectorySeparatorChar}{fileName}", fileContent); }

   }
}