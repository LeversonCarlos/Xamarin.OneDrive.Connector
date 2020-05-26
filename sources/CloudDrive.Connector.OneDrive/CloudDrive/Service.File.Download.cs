using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class OneDriveService
   {

      public async Task<byte[]> Download(string fileID)
      {
         try
         {
            var httpPath = $"me/drive/items/{fileID}/content";

            var httpMessage = await this.Client.GetAsync(httpPath);
            if (!httpMessage.IsSuccessStatusCode) throw new Exception(await httpMessage.Content.ReadAsStringAsync());
            var httpContent = await httpMessage.Content.ReadAsByteArrayAsync();

            return httpContent;
         }
         catch (Exception) { throw; }
      }

   }
}