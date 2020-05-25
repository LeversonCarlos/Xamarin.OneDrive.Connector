using System;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class OneDriveService
   {

      public async Task<DirectoryVM[]> GetDrivers()
      {
         try
         {
            var profile = await this.GetProfile();
            var resultData = new DirectoryVM
            {
               ID = "root",
               Name = profile.Description,
               Path = "/"
            };
            return new DirectoryVM[] { resultData };
         }
         catch (Exception) { throw; }
      }

   }
}