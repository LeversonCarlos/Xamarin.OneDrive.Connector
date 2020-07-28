using Android.App;
using Android.Content;
using Microsoft.Identity.Client;
using System;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public static void Init(Activity activity, string clientID, string redirectUri, params string[] scopes)
      {
         Xamarin.Forms.DependencyService.Register<OneDriveSettings>();
         Xamarin.Forms.DependencyService.Get<OneDriveSettings>().Init(clientID, redirectUri, scopes);

         ImplementationProvider.Add<OneDriveService>(() =>
         {
            var identity = OneDriveToken.CreateIdentity(clientID, redirectUri, () => activity);
            var token = new OneDriveToken(identity, scopes);
            var client = new OneDriveClient(token);
            return new OneDriveService(client);
         });
      }

      public static void SetAuthenticationResult(int requestCode, Result resultCode, Intent data)
      {
         AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
      }

   }
}
