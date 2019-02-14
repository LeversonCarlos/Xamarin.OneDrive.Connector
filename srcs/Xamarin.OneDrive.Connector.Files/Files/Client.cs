using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive.Files
{
   public static partial class Extender
   {

      public static async Task<FileData> GetDetailsAsync(this Xamarin.OneDrive.Connector connector)
      {
         var httpPath = $"me/drive/root";
         return await GetDetailsAsync(connector, httpPath);
      }

      public static async Task<FileData> GetDetailsAsync(this Xamarin.OneDrive.Connector connector, FileData folder)
      {
         var httpPath = $"me/drive/items/{folder.id}";
         return await GetDetailsAsync(connector, httpPath);
      }

      private static async Task<FileData> GetDetailsAsync(this Xamarin.OneDrive.Connector connector, string httpPath)
      {
         try
         {
            httpPath += "?select=id,name,parentReference&$top=1";

            // REQUEST DATA FROM SERVER
            var httpMessage = await connector.GetAsync(httpPath);
            if (!httpMessage.IsSuccessStatusCode)
            { throw new Exception(await httpMessage.Content.ReadAsStringAsync()); }

            // SERIALIZE AND STORE RESULT
            var httpContent = await httpMessage.Content.ReadAsStreamAsync();
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(FileData));
            var httpResult = (FileData)serializer.ReadObject(httpContent);

            // RESULT
            if (string.IsNullOrEmpty(httpResult.FilePath))
            { httpResult.FilePath = httpResult.FileName; }
            if (httpResult.parentReference != null)
            {
                httpResult.FilePath = httpResult.parentReference.FilePath; 
            }
            return httpResult;            

         }
         catch (Exception) { throw; }
      }


   }
}