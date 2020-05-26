using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
            httpPath += "?select=id,name,folder,parentReference&$top=1000";

            // AUXILIARY FUNCTIONS
            var getFullPath = new Func<DTOs.DirectoryDTO, string>(item =>
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
               var httpResult = await this.Client.GetAsync<DTOs.DirectorySearchDTO>(httpPath);

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

   namespace DTOs
   {

      internal class DirectorySearchDTO
      {
         public DirectoryDTO[] value { get; set; }

         [DataMember(Name = "@odata.nextLink")]
         public string nextLink { get; set; }
      }

      internal class DirectoryDTO
      {
         public string id { get; set; }
         public string name { get; set; }
         public DirectoryDetailsDTO folder { get; set; }
         public DirectoryParentDTO parentReference { get; set; }
      }

      internal class DirectoryDetailsDTO
      {
         public int childCount { get; set; }
      }

      internal class DirectoryParentDTO
      {
         public string id { get; set; }
         public string path { get; set; }
         public string driveId { get; set; }
      }

   }

}