using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public async Task<DirectoryVM[]> GetDirectories(DirectoryVM directory)
      {
         try
         {
            var folderList = new List<DirectoryVM>();

            // PREPARE URL 
            var IDs = GetIDs(directory.ID);
            var httpPath = "" +
               $"drives/{IDs.DriveID}/items/{IDs.ID}/children" +
               "?" +
               "$filter=folder ne null&" +
               "$select=id,name,folder,parentReference&" +
               "$top=1000";

            // MOUNT FULL PATH
            var getFullPath = new Func<DTOs.Directory, string>(item =>
               $"{GetPath(item?.parentReference?.path)}/{item.name}"
            );

            while (!string.IsNullOrEmpty(httpPath))
            {

               // REQUEST DATA FROM SERVER
               var httpResult = await Client
                  .GetAsync<DTOs.DirectorySearch>(httpPath);

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
                  httpPath = httpPath.Replace(OneDriveClient.MicrosoftGraphUrl, string.Empty);

            }

            return folderList.ToArray();
         }
         catch (Exception) { throw; }
      }

   }
}