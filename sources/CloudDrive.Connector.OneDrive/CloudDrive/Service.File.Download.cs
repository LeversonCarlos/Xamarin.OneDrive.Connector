using System;
using System.IO;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class OneDriveService
   {

      public async Task<Stream> Download(string fileID)
      {
         try
         {
            var httpPath = $"me/drive/items/{fileID}/content";

            var httpMessage = await this.Client.GetAsync(httpPath);
            if (!httpMessage.IsSuccessStatusCode) throw new Exception(await httpMessage.Content.ReadAsStringAsync());
            var httpContent = await httpMessage.Content.ReadAsStreamAsync();

            return httpContent;
         }
         catch (Exception ex) { throw new Exception($"Error while downloading file [{fileID}] with oneDrive service", ex); }
      }

   }
}