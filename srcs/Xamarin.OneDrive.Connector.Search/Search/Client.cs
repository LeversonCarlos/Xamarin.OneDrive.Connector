using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive.Search
{
   public static class SearchConnector
   {

      public static async Task<List<FileData>> SearchFilesAsync(this Xamarin.OneDrive.Connector connector, string searchText)
      {
         try
         { 

            // INITIALIZE
            var fileList = new List<FileData>();
            var httpPath = $"me/drive/root/search(q='{searchText}')?select=id,name,createdDateTime,size,parentReference,@microsoft.graph.downloadUrl";

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
               { httpPath = httpPath.Replace(this.BaseAddress.AbsoluteUri, string.Empty); }

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
               file.FilePath = folderList
                  .Where(folder => folder.id == file.parentReference.id)
                  .Select(folder => folder.FilePath)
                  .FirstOrDefault();
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