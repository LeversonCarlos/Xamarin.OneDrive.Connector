using Xamarin.Essentials;
using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.Helpers
{
   internal class PermissionHelper
   {

      private static bool AlreadyAskedPermissions { get; set; } = false;
      internal static async Task<bool> StoragePermissionAlert()
      {
         var mainPage = Xamarin.Forms.Application.Current.MainPage;
         var message = "Will need storage permission to browser for files";
         var response = await mainPage.DisplayAlert(mainPage.Title, message, "OK", "Cancel");
         return response;
      }

      internal static async Task<bool> HasStoragePermission()
      {
         try
         {
            var permissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            return (permissionStatus == PermissionStatus.Granted);
         }
         catch (Exception ex) { Console.WriteLine($"Exception:{ex}"); return false; }
      }

      internal static async Task<bool> AskStoragePermission()
      {
         try
         {
            var permissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (permissionStatus != PermissionStatus.Granted)
            {

               if (AlreadyAskedPermissions)
               {
                  if (!await StoragePermissionAlert()) { return false; }
               }

               permissionStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
               AlreadyAskedPermissions = true;
            }

            return (permissionStatus == PermissionStatus.Granted);
         }
         catch (Exception ex) { Console.WriteLine($"Exception:{ex}"); return false; }
      }

   }
}
