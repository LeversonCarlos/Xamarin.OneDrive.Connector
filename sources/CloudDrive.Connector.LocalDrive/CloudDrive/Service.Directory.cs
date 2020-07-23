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

            if (directory == null) return null;
            var directoryList = await GetDirectoryList(directory.ID);

            var resultList = directoryList
               .Select(dir => GetDirectoryInfo(dir))
               .Where(dirInfo => dirInfo != null)
               .Select(dirInfo => new DirectoryVM
               {
                  ID = dirInfo.FullName,
                  Name = dirInfo.Name,
                  Path = dirInfo.FullName,
                  ParentID = directory.ID
               })
               .ToArray();

            return resultList;
         }
         catch (Exception) { throw; }
      }

      internal DirectoryInfo GetDirectoryInfo(string path)
      {
         if (Directory.Exists(path))
            return new DirectoryInfo(path);
         else
            return null;
      }

      internal Task<string[]> GetDirectoryList(string path)
      {
         var folderQuery = Directory
            .EnumerateDirectories(path)
            .Where(x => !string.IsNullOrEmpty(x))
            .OrderBy(x => x)
            .AsQueryable();
         return Task.FromResult(folderQuery.ToArray());
      }

   }
}