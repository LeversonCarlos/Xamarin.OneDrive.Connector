using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public async Task<DirectoryVM[]> GetDirectories(DirectoryVM directory)
      {
         try
         {
            if (!await CheckConnectionAsync()) return null;

            var folderQueryResult = await GetDirectoryList(directory.ID);

            var folderList = folderQueryResult
               .Select(x => GetDirectoryInfo(x))
               .Where(x => x != null)
               .Select(x => new DirectoryVM
               {
                  ID = x.FullName,
                  Name = x.Name,
                  Path = x.FullName,
                  ParentID = directory.ID
               })
               .ToArray();

            return folderList;
         }
         catch (Exception) { throw; }
      }

   }
}