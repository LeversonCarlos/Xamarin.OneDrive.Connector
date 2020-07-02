using Android.App;
using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      internal static IClientApplicationBase CreateIdentity(string clientID, string redirectUri, Func<Activity> parentWindow)
      {
         return PublicClientApplicationBuilder
            .Create(clientID)
            .WithAuthority(OneDriveToken.GetAuthorityUri())
            .WithRedirectUri(redirectUri)
            .WithParentActivityOrWindow(parentWindow)
            .Build();
      }

      private Task<AuthenticationResult> AcquireTokenFromIdentity()
      {
         return (this.Identity as IPublicClientApplication)?.AcquireTokenInteractive(this.Scopes).ExecuteAsync();
      }

   }
}
