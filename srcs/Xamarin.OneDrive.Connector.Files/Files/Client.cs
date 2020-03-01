using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive.Files
{
   public static partial class Extender
   {

      public static async Task<FileData> GetDetailsAsync(this Xamarin.OneDrive.Connector connector)
      {
         var httpPath = $"me/drive/root";
         var folder = await GetDetailsAsync(connector, httpPath);
         if (string.IsNullOrEmpty(folder.FilePath))
         { folder.FilePath = "/drive/root:"; }
         return folder;
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
            httpPath += "?select=id,name,parentReference";

            // REQUEST DATA FROM SERVER
            var httpMessage = await connector.GetAsync(httpPath);
            if (!httpMessage.IsSuccessStatusCode)
            { throw new Exception(await httpMessage.Content.ReadAsStringAsync()); }

            // SERIALIZE AND STORE RESULT
            var httpContent = await httpMessage.Content.ReadAsStreamAsync();
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(FileData));
            var httpResult = (FileData)serializer.ReadObject(httpContent);

            // RESULT
            if (httpResult.parentReference != null)
            { httpResult.FilePath = httpResult.parentReference.FilePath; }
            return httpResult;

         }
         catch (Exception) { throw; }
      }

      public static async Task<bool> DeleteFile(this Xamarin.OneDrive.Connector connector, FileData file)
      {
            if (string.IsNullOrEmpty(file.id))
                throw new ArgumentException("OneDrive.DeleteFile called with null FileData.id");

            var httpMessage = await connector.DeleteAsync("drive/items/" + file.id);
            return httpMessage.IsSuccessStatusCode;
      }

      public static async Task<bool> DeleteFile(this Xamarin.OneDrive.Connector connector, string httpFilePath)
      {
            if (string.IsNullOrEmpty(httpFilePath))
                throw new ArgumentException("OneDrive.DeleteFile called with null httpFilePath");

            if (!httpFilePath.StartsWith("/"))
                httpFilePath = "/" + httpFilePath;
            var httpMessage = await connector.DeleteAsync("drive/root:" + httpFilePath);
            return httpMessage.IsSuccessStatusCode;
      }

      public static async Task<bool> DeleteFile(this Xamarin.OneDrive.Connector connector, FileData parentFolder, string fileName)
      {
            if (string.IsNullOrEmpty(parentFolder.id))
                throw new ArgumentException("OneDrive.DeleteFile called with null parentFolder.id");
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("OneDrive.DeleteFile called with null fileName");

            var httpMessage = await connector.DeleteAsync("drive/items/" + parentFolder.id + ":/" + fileName);
            return httpMessage.IsSuccessStatusCode;
      }


   }
}
