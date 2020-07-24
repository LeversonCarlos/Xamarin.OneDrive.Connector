using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      internal LocalDriveService(IStorage storage) =>
         _Storage = storage;
      IStorage _Storage { get; }

      public async Task<DirectoryVM[]> GetDrivers()
      {
         if (!await CheckConnectionAsync()) return null;

         var driveList = await _Storage.GetStorageList();

         var driveResult = driveList
            .Select(x => new DirectoryVM
            {
               ID = x,
               Name = x,
               Path = x
            })
            .ToArray();

         return driveResult;
      }

   }
}