using Android.App;
using Android.OS;

namespace Xamarin.CloudDrive.Connector
{

   partial class LocalDriveService
   {

      internal LocalDriveService()
      {
         _Connection = new LocalDriveConnection();
         _Storage = new LocalDriveStorage();
      }

      public static void Init(Activity activity, Bundle bundle)
      {
         ImplementationProvider.Add<LocalDriveService>(() => new LocalDriveService());
      }

   }
}
