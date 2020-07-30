using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public static void Init(Activity activity, string clientID, string redirectUri, params string[] scopes)
      {
         Xamarin.Forms.DependencyService.Register<OneDriveSettings>();
         Xamarin.Forms.DependencyService.Get<OneDriveSettings>().Init(activity, clientID, redirectUri, scopes);
         Xamarin.Forms.DependencyService.Register<OneDriveIdentity>();
         Xamarin.Forms.DependencyService.Register<OneDriveToken>();
         Xamarin.Forms.DependencyService.Register<OneDriveClient>();
         Xamarin.Forms.DependencyService.Register<OneDriveService>();
      }

      public static void SetAuthenticationResult(int requestCode, Result resultCode, Intent data) =>
         AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);

   }
}
