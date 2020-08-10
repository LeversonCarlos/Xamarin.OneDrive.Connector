using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal class LocalDriveStorage : IStorage
   {

      public Task<string[]> GetStorageList()
      {
         var drivesList = System.IO.DriveInfo
            .GetDrives()
            .Select(drive => drive.RootDirectory.FullName)
            .ToArray();
         return Task.FromResult(drivesList);
      }

   }
}