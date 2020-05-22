using Android.App;
using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      static IClientApplicationBase CreateIdentity(string clientID, string redirectUri, Func<Activity> parentWindow)
      {
         return PublicClientApplicationBuilder
            .Create(clientID)
            .WithAuthority(Token.GetAuthorityUri())
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
