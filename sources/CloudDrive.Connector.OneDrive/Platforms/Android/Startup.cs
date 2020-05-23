using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Startup
   {

      /*
      public static void AddOneDriveConnector(this Activity activity,
         string clientID, params string[] scopes)
      {
         Common.DependencyProvider.Add(() => activity);
         Common.DependencyProvider.Add(() =>
         {
            var redirectUri = $"msal{clientID}://auth";
            var identity = Token.CreateIdentity(clientID, redirectUri, () => activity);
            var token = new Token(identity, scopes);
            var client = new Client(token);
            return new OneDriveService(client);
         });
      }
      */

      public static void SetOneDriveAuthenticationResult(this Activity activity,
         int requestCode, Result resultCode, Intent data)
      {
         AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
      }

   }
}
