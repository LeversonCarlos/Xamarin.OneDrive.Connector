using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public async Task<FileVM[]> GetFiles(DirectoryVM directory)
      {
         try
         {
            var fileList = new List<FileVM>();

            var IDs = GetIDs(directory.ID);
            var httpPath = "" +
               $"drives/{IDs.DriveID}/items/{IDs.ID}/children" +
               "?" +
               "$filter=file ne null&" +
               "$select=id,name,createdDateTime,size,@microsoft.graph.downloadUrl,file,parentReference&" +
               "$top=1000";

            while (!string.IsNullOrEmpty(httpPath))
            {

               // REQUEST DATA FROM SERVER
               var httpResult = await Client
                  .GetAsync<DTOs.FileSearch>(httpPath);

               // STORE RESULT
               var files = httpResult.value
                  .Where(x => x.file != null)
                  .Select(x => GetDetails(x))
                  .ToList();
               fileList.AddRange(files);

               // CHECK IF THERE IS ANOTHER PAGE OF RESULTS
               httpPath = httpResult.nextLink;
               if (!string.IsNullOrEmpty(httpPath))
                  httpPath = httpPath.Replace(OneDriveClient.MicrosoftGraphUrl, string.Empty);

            }

            return fileList.ToArray();
         }
         catch (Exception) { throw; }
      }

   }
}