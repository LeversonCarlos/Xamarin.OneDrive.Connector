using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive.Files
{
   partial class Extender
   {

      public static async Task<string> GetDownloadUrlAsync(this Xamarin.OneDrive.Connector connector, FileData file)
      {
         try
         { 

            var httpPath = $"me/drive/items/{file.id}?select=id,@microsoft.graph.downloadUrl";
            var httpMessage = await connector.GetAsync(httpPath);
            if (!httpMessage.IsSuccessStatusCode)
            { throw new Exception(await httpMessage.Content.ReadAsStringAsync()); }

            var httpContent = await httpMessage.Content.ReadAsStreamAsync();
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(FileData));
            var httpResult = (FileData)serializer.ReadObject(httpContent);
            return httpResult.downloadUrl;

         }
         catch (Exception) { throw; }
      }

   }
}