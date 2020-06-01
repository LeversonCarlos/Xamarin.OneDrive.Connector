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
            return new FileStream(fileID, FileMode.Open, FileAccess.Read);
         }
         catch (Exception) { throw; }
      }

   }
}