using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public Task<ProfileVM> GetProfile() =>
         Task.FromResult(new ProfileVM
         {
            ID = Environment.CommandLine,
            Description = $"{Environment.UserName} on {Environment.MachineName}"
         });

   }
}