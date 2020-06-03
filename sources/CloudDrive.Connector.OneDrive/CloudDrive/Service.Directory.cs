using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class OneDriveService
   {

      public async Task<DirectoryVM[]> GetDirectories(DirectoryVM directory)
      {
         try
         {
            var folderList = new List<DirectoryVM>();

            var httpPath = $"me/drive/items/{directory.ID}/children";
            httpPath += "?";
            httpPath += "$filter=folder ne null&";
            httpPath += "$select=id,name,folder,parentReference&";
            httpPath += "$top=1000";

            // AUXILIARY FUNCTIONS
            var getFullPath = new Func<DTOs.Directory, string>(item =>
            {
               if (item.parentReference == null || string.IsNullOrEmpty(item.parentReference.path)) { return ""; }
               var fullPath = item.parentReference.path;
               fullPath = System.Uri.UnescapeDataString(fullPath);
               fullPath = fullPath.Replace("/drive/root:", "");
               return $"{fullPath}/{item.name}";
            });

            while (!string.IsNullOrEmpty(httpPath))
            {

               // REQUEST DATA FROM SERVER
               var httpResult = await this.Client.GetAsync<DTOs.DirectorySearch>(httpPath);

               // STORE RESULT
               var folders = httpResult?.value?
                  .Where(x => x.folder != null)
                  .Select(x => new DirectoryVM
                  {
                     ID = x.id,
                     Name = x.name,
                     Path = getFullPath(x),
                     ParentID = directory.ID
                  })
                  .ToList();
               folderList.AddRange(folders);

               // CHECK IF THERE IS ANOTHER PAGE OF RESULTS
               httpPath = httpResult.nextLink;
               if (!string.IsNullOrEmpty(httpPath))
               { httpPath = httpPath.Replace(((System.Net.Http.HttpClient)this.Client).BaseAddress.AbsoluteUri, string.Empty); }

            }

            return folderList.ToArray();
         }
         catch (Exception) { throw; }
      }

   }
}