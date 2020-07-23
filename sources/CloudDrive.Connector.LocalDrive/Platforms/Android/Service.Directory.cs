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
            if (!await this.CheckConnectionAsync()) { return null; }

            var folderQuery = System.IO.Directory
               .EnumerateDirectories(directory.ID)
               .Where(x => !string.IsNullOrEmpty(x))
               .OrderBy(x => x)
               .AsQueryable();
            var folderQueryResult = await Task.FromResult(folderQuery.ToList());

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