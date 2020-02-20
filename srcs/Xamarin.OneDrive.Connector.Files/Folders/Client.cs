using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.OneDrive.Files
{
   public static partial class Extender
   {

      public static async Task<List<FileData>> GetChildFoldersAsync(this Xamarin.OneDrive.Connector connector)
      {
         try
         {
            var httpPath = $"me/drive/root/children";
            var folderList = await GetChildFoldersAsync(connector, httpPath);
            folderList.ForEach(x => x.FilePath = $"/ {x.FileName}");
            return folderList;
         }
         catch (Exception) { throw; }
      }

      public static async Task<List<FileData>> GetChildFoldersAsync(this Xamarin.OneDrive.Connector connector, FileData folder)
      {
         try
         {
            var httpPath = $"me/drive/items/{folder.id}/children";
            var folderList = await GetChildFoldersAsync(connector, httpPath);
            folderList.ForEach(x => x.FilePath = $"{folder.FilePath} / {x.FileName}");
            return folderList;
         }
         catch (Exception) { throw; }
      }

      private static async Task<List<FileData>> GetChildFoldersAsync(this Xamarin.OneDrive.Connector connector, string httpPath)
      {
         try
         {
            var folderList = new List<FileData>();
            httpPath += "?select=id,name,folder&$top=1000";

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
               var folders = httpResult.Files.Where(x => x.folderData != null).ToList();
               folderList.AddRange(folders);

               // CHECK IF THERE IS ANOTHER PAGE OF RESULTS
               httpPath = httpResult.nextLink;
               if (!string.IsNullOrEmpty(httpPath))
               { httpPath = httpPath.Replace(connector.BaseAddress.AbsoluteUri, string.Empty); }

            }

            // NORMALIZE FOLDER's PATHS
            foreach (var folder in folderList)
            {
               if (string.IsNullOrEmpty(folder.FilePath))
               { folder.FilePath = string.Empty; }

               var sep = folder.FilePath.IndexOf(":");
               if (sep != -1)
               { folder.FilePath = folder.FilePath.Substring(sep + 1); }

               folder.FilePath = Uri.UnescapeDataString(folder.FilePath);
            }

            // RESULT
            folderList = folderList
               .OrderBy(x => x.FilePath)
               .ThenBy(x => x.FileName)
               .ToList();
            return folderList;

         }
         catch (Exception) { throw; }
      }


      public static async Task<List<FileData>> GetChildFilesAsync(this Xamarin.OneDrive.Connector connector, FileData folder)
      {
         try
         {
            var fileList = new List<FileData>();
            var httpPath = $"me/drive/items/{folder.id}/children";
            httpPath += "?select=id,name,createdDateTime,size,file&$top=1000";

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
               var files = httpResult.Files.Where(x => x.fileData != null).ToList();
               fileList.AddRange(files);

               // CHECK IF THERE IS ANOTHER PAGE OF RESULTS
               httpPath = httpResult.nextLink;
               if (!string.IsNullOrEmpty(httpPath))
               { httpPath = httpPath.Replace(connector.BaseAddress.AbsoluteUri, string.Empty); }

            }

            // NORMALIZE FILE's PATHS
            foreach (var file in fileList)
            {
               file.parentID = folder.id;
               file.FilePath = folder.FilePath;               
            }

            // RESULT
            return fileList;

         }
         catch (Exception) { throw; }
      }
      
     public async Task<FileData>GetThisAppFolderAsync()
     {
         var httpMessage = await GetAsync("drive/special/approot:/");
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

   }
}
