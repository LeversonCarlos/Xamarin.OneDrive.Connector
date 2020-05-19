using System;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService
   {

      public async Task<ProfileVM> GetProfile()
      {
         try
         {
            var profile = new ProfileVM
            {
               ID = $"{Environment.CommandLine}",
               Description = $"{Environment.UserName} on {Environment.MachineName}"
            };
            await Task.CompletedTask;
            return profile;
         }
         catch (Exception) { throw; }
      };

   }
}