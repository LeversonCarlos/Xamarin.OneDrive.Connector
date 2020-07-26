using System;
using System.IO;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public async Task<FileVM> Upload(string fileID, byte[] fileContent)
      {
         try
         {
            if (!await ConnectAsync()) return null;

            if (string.IsNullOrEmpty(fileID)) return null;
            if (fileContent == null) return null;
            if (fileContent.Length == 0) return null;

            if (File.Exists(fileID)) File.Delete(fileID);

            await Task.Run(() => File.WriteAllBytes(fileID, fileContent));

            var fileDetails = await GetDetails(fileID);
            return fileDetails;
         }
         catch (Exception) { return null; }
      }

      public Task<FileVM> Upload(string directoryID, string fileName, byte[] fileContent) =>
         Upload($"{directoryID}{Path.DirectorySeparatorChar}{fileName}", fileContent);

   }
}