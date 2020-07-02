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
         ImplementationProvider.Add<OneDriveService>(() =>
         {
            if (string.IsNullOrEmpty(clientID) || clientID == "{YOUR_MICROSOFT_APPLICATION_ID}") { throw new ArgumentNullException("The application ID argument for the onedrive client must be set"); }
            if (string.IsNullOrEmpty(redirectUri) || redirectUri == "msal{YOUR_MICROSOFT_APPLICATION_ID}://auth") { throw new ArgumentNullException("The redirectUri argument for the onedrive client must be set"); }
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
