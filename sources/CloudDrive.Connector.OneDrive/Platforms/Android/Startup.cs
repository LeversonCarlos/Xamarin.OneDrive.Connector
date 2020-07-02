using Android.App;
using Android.Content;
using Microsoft.Identity.Client;
using System;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Startup
   {

      public static void AddOneDriveConnector(this Activity activity,
         string clientID, params string[] scopes)
      {
         ImplementationProvider.Add(() =>
         {
            if (string.IsNullOrEmpty(clientID) || clientID == "{YOUR_MICROSOFT_APPLICATION_ID}") { throw new ArgumentNullException("The application ID argument for the onedrive client must be set"); }
            var redirectUri = $"msal{clientID}://auth";
            var identity = Token.CreateIdentity(clientID, redirectUri, () => activity);
            var token = new Token(identity, scopes);
            var client = new OneDriveClient(token);
            return new OneDriveService(client);
         });
      }

      public static void SetOneDriveAuthenticationResult(this Activity activity,
         int requestCode, Result resultCode, Intent data)
      {
         AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
      }

   }
}
