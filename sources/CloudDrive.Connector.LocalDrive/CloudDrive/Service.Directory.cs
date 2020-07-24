using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public async Task<DirectoryVM[]> GetDirectories(DirectoryVM directory)
      {
         if (!await CheckConnectionAsync()) return null;

         if (directory == null) return null;
         if (string.IsNullOrEmpty(directory.ID)) return null;
         if (!Directory.Exists(directory.ID)) return null;

         var directoryListQuery = Directory
            .EnumerateDirectories(directory.ID)
            .Where(dir => !string.IsNullOrEmpty(dir))
            .AsQueryable();
         var directoryList = await Task.FromResult(directoryListQuery.ToArray());

         var resultList = directoryList
            .Where(dir => Directory.Exists(dir))
            .OrderBy(dir => dir)
            .Select(dir => new DirectoryInfo(dir))
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

   }
}