using Android.App;
using Android.OS;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public static void Init(Activity activity, Bundle bundle)
      {
         ImplementationProvider.Add<LocalDriveService>(() => new LocalDriveService());
      }

   }
}
