using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.OneDrive.Files
{
   partial class Extender
   {

      public static async Task<List<FileData>> SearchFilesAsync(this Xamarin.OneDrive.Connector connector, string searchText, int top = 100)
      {
         var httpPath = $"me/drive/root/search(q='{searchText}')?select=id,name,createdDateTime,size,parentReference&$top={top}";
         return await SearchFilesAsync(connector, httpPath);
      }

      public static async Task<List<FileData>> SearchFilesAsync(this Xamarin.OneDrive.Connector connector, FileData folder, string searchText, int top = 100)
      {
         if (folder == null || string.IsNullOrEmpty(folder.id)) {
            return await SearchFilesAsync(connector, searchText, top);
         }
         else
         {
            var httpPath = $"me/drive/items/{folder.id}/search(q='{searchText}')?select=id,name,createdDateTime,size,parentReference&$top={top}";
            return await SearchFilesAsync(connector, httpPath);
         }
      }

      private static async Task<List<FileData>> SearchFilesAsync(this Xamarin.OneDrive.Connector connector, string httpPath)
      {
         try
         { 
            var fileList = new List<FileData>();

            while (!string.IsNullOrEmpty(httpPath))
            { 

               // REQUEST DATA FROM SERVER
               var httpMessage = await connector.GetAsync(httpPath);
               if (!httpMessage.IsSuccessStatusCode)
               { throw new Exception(await httpMessage.Content.ReadAsStringAsync()); }

               // SERIALIZE AND STORE RESULT
               var httpContent = await httpMessage.Content.ReadAsStreamAsync();
               var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(SearchData));
               var httpResult = (SearchData)serializer.ReadObject(httpContent);
               fileList.AddRange(httpResult.Files);

               // CHECK IF THERE IS ANOTHER PAGE OF RESULTS
               httpPath = httpResult.nextLink;
               if (!string.IsNullOrEmpty(httpPath))
               { httpPath = httpPath.Replace(connector.BaseAddress.AbsoluteUri, string.Empty); }

            }

            // GROUP DISTINCT FOLDERS 
            var folderList = fileList
               .Where(x => x.parentReference != null)
               .GroupBy(x => x.parentReference.id)
               .Select(x => x.FirstOrDefault().parentReference)
               .ToList();

            // NORMALIZE FOLDER's PATHS
            foreach(var folder in folderList)
            {
               var sep = folder.FilePath.IndexOf(":");
               if (sep != -1)
               { folder.FilePath = folder.FilePath.Substring(sep + 1); }
               folder.FilePath = Uri.UnescapeDataString(folder.FilePath);
            }

            // APPLY FOLDER's PATHS TO FILES
            foreach (var file in fileList)
            {
               if (file.parentReference == null) { continue; }
               file.parentID = file.parentReference.id;
               file.FilePath = folderList
                  .Where(folder => folder.id == file.parentReference.id)
                  .Select(folder => folder.FilePath)
                  .FirstOrDefault();
               var createdDateTime = DateTime.MinValue;
               if (DateTime.TryParse(file.CreatedDateTimeText, out createdDateTime))
               { file.CreatedDateTime = createdDateTime; }
            }

            // RESULT
            fileList = fileList
               .OrderBy(x => x.FilePath)
               .ThenBy(x => x.FileName)
               .ToList();
            return fileList;
         }
         catch (Exception) { throw; }
      }

   }
}