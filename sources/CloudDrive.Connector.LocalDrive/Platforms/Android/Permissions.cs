using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   internal class Permissions
   {

      internal static Action StoragePermissionAlert { get; set; } = new Action(() =>
      {
         var mainPage = Xamarin.Forms.Application.Current.MainPage;
         var message = "Will need storage permission to browser for files";
         mainPage.DisplayAlert(mainPage.Title, message, "Ok");
      });

      internal static async Task<bool> HasStoragePermission()
      {
         try
         {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            return (permissionStatus == PermissionStatus.Granted);
         }
         catch (Exception ex) { Console.WriteLine($"Exception:{ex}"); return false; }
      }

      internal static async Task<bool> AskStoragePermission()
      {
         try
         {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (permissionStatus != PermissionStatus.Granted)
            {
               if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
               { StoragePermissionAlert(); }

               var requestPermissions = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
               if (requestPermissions.ContainsKey(Permission.Storage))
               { permissionStatus = requestPermissions[Permission.Storage]; }
            }

            return (permissionStatus == PermissionStatus.Granted);
         }
         catch (Exception ex) { Console.WriteLine($"Exception:{ex}"); return false; }
      }

   }
}
