using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class OneDriveService
   {

      public async Task<FileVM[]> GetFiles(DirectoryVM directory)
      {
         try
         {
            var fileList = new List<FileVM>();

            var httpPath = $"me/drive/items/{directory.ID}/children";
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

            return fileList.ToArray();
         }
         catch (Exception) { throw; }
      }


   }
}