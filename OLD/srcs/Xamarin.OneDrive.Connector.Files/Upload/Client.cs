using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive.Files
{
   public static partial class Extender
   {

      public static async Task<FileData> UploadAsync(this Xamarin.OneDrive.Connector connector, FileData file, System.IO.Stream content)
      {
         try
         {

            var httpPath = $"me/drive/items/{file.id}/content";
            if (string.IsNullOrEmpty(file.id))
            { httpPath = $"me/drive/items/{file.parentID}:/{file.FileName}:/content"; }
            var httpData = new System.Net.Http.StreamContent(content);
            var httpMessage = await connector.PutAsync(httpPath, httpData);

            if (!httpMessage.IsSuccessStatusCode)
            { throw new Exception(await httpMessage.Content.ReadAsStringAsync()); }

            var httpContent = await httpMessage.Content.ReadAsStreamAsync();
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(FileData));
            var httpResult = (FileData)serializer.ReadObject(httpContent);
            
            return httpResult;

         }
         catch (Exception) { throw; }
      }

   }
}