using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class Startup
   {

      public static void AddLocalDriveConnector(this Activity activity, Bundle bundle)
      {
         Common.DependencyProvider.Add(() => activity);
         Common.DependencyProvider.Add(() => new LocalDriveService());
         Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(activity, bundle);
      }

      public static void SetLocalDrivePermissionsResult(this Activity activity,
         int requestCode, string[] permissions, Permission[] grantResults)
      {
         Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }

   }
}
