using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class OneDriveService
   {

      public async Task<FileVM[]> SearchFiles(DirectoryVM directory, string searchPattern, int limit)
      {
         try
         {
            var fileList = new List<FileVM>();

            string httpPath;
            if (string.IsNullOrEmpty(directory?.ID)) { httpPath = "me/drive/root"; }
            else { httpPath = $"me/drive/items/{directory.ID}"; }
            httpPath += "/search(q='{searchText}')";
            httpPath += "?select=id,name,createdDateTime,size,@microsoft.graph.downloadUrl,file,parentReference&$top=1000";

            while (!string.IsNullOrEmpty(httpPath))
            {

               // REQUEST DATA FROM SERVER
               var httpResult = await this.Client.GetAsync<DTOs.FileSearch>(httpPath);

               // STORE RESULT
               var files = httpResult?.value?
                  .Where(x => x.file != null)
                  .Select(x => this.GetDetails(x))
                  .ToList();
               fileList.AddRange(files);

               // CHECK IF THERE IS ANOTHER PAGE OF RESULTS
               httpPath = httpResult.nextLink;
               if (!string.IsNullOrEmpty(httpPath))
               { httpPath = httpPath.Replace(((System.Net.Http.HttpClient)this.Client).BaseAddress.AbsoluteUri, string.Empty); }

            }

            /*
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

             */

            return fileList.ToArray();
         }
         catch (Exception) { throw; }
      }

   }
}