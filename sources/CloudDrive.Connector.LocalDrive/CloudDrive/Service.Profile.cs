using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public Task<ProfileVM> GetProfile()
      {
         var profile = new ProfileVM();
         profile.ID = Environment.CommandLine;
         profile.Description = $"{Environment.UserName} on {Environment.MachineName}";
         return Task.FromResult(profile);
      }

   }
}