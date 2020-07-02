using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class Startup
   {

      public static void AddLocalDriveConnector(this Activity activity, Bundle bundle)
      {
         ImplementationProvider.Add(() => new LocalDriveService());
         Xamarin.Essentials.Platform.Init(activity, bundle);
      }

      public static void SetLocalDrivePermissionsResult(this Activity activity,
         int requestCode, string[] permissions, Permission[] grantResults)
      {
         Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
         // Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }

   }
}
