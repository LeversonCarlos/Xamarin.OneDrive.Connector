using System;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public async Task<DirectoryVM[]> GetDrivers()
      {
         try
         {
            if (!await this.CheckConnectionAsync()) { return null; }

            var driveList = Helpers.StorageHelper.GetStorages()
               .Select(x => new DirectoryVM
               {
                  ID = x,
                  Name = x,
                  Path = x
               })
               .ToArray();

            await Task.CompletedTask;
            return driveList;
         }
         catch (Exception) { throw; }
      }

   }
}