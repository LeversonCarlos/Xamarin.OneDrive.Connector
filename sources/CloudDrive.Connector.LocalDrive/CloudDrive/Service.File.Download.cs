using System.IO;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public async Task<Stream> Download(string fileID)
      {
         if (!await CheckConnectionAsync()) return null;

         if (string.IsNullOrEmpty(fileID)) return null;
         if (!File.Exists(fileID)) return null;

         var memoryStream = new MemoryStream();
         using (var fileStream = System.IO.File.OpenRead(fileID))
         {
            await fileStream.CopyToAsync(memoryStream);
            await memoryStream.FlushAsync();
            memoryStream.Position = 0;
         }
         return memoryStream;
      }

   }
}