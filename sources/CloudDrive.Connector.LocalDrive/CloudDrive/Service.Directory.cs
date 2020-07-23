using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

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