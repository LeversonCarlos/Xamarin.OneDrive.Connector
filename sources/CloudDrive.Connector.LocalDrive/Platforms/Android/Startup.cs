using Android.App;
using Android.OS;

namespace Xamarin.CloudDrive.Connector
{
   public class LocalDrive
   {

      public static void Init(Activity activity, Bundle bundle)
      {
         ImplementationProvider.Add(() => new LocalDriveService());
      }

   }
}
