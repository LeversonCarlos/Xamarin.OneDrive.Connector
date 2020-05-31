using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService
   {

      public async Task<DirectoryVM[]> GetDrivers()
      {
         try
         {
            if (!await this.CheckConnectionAsync()) { return null; }

            var driveList = Storages.GetStorages()
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