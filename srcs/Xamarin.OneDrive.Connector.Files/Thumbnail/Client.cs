using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive.Files
{
   partial class Extender
   {

      public static async Task<bool> SetThumbnailAsync(this Xamarin.OneDrive.Connector connector, FileData file, System.IO.Stream image)
      {
         try
         { 

            var httpPath = $"me/drive/items/{file.id}/thumbnails/0/source/content";
            var httpContent = new System.Net.Http.StreamContent(image);
            var httpMessage = await connector.PutAsync(httpPath, httpContent);

            if (!httpMessage.IsSuccessStatusCode)
            { throw new Exception(await httpMessage.Content.ReadAsStringAsync()); }
            return true;

         }
         catch (Exception) { throw; }
      }

   }
}