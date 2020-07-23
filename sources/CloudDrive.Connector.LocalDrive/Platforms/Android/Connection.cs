using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Xamarin.CloudDrive.Connector
{
   internal class LocalDriveConnection : IConnection
   {

      static bool _AlreadyAskedPermissions { get; set; } = false;
      static async Task<bool> _StoragePermissionAlert()
      {
         var mainPage = Xamarin.Forms.Application.Current.MainPage;
         var message = "Will need storage permission to browser for files";
         var response = await mainPage.DisplayAlert(mainPage.Title, message, "OK", "Cancel");
         return response;
      }

      public async Task<bool> CheckConnectionAsync()
      {
         try
         {
            var permissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            return (permissionStatus == PermissionStatus.Granted);
         }
         catch (Exception) { return false; }
      }

      public async Task<bool> ConnectAsync()
      {
         try
         {
            var permissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (permissionStatus != PermissionStatus.Granted)
            {

               if (_AlreadyAskedPermissions)
               {
                  if (!await _StoragePermissionAlert()) { return false; }
               }

               permissionStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
               _AlreadyAskedPermissions = true;
            }

            return (permissionStatus == PermissionStatus.Granted);
         }
         catch (Exception) { return false; }
      }

      public Task DisconnectAsync() => Task.CompletedTask;

   }
}
