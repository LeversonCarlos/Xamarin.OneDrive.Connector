using Android.App;
using Microsoft.Identity.Client;
using System;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      static IClientApplicationBase CreateClientApplication(string clientID, string redirectUri, Func<Activity> parentWindow)
      {
         return PublicClientApplicationBuilder
            .Create(clientID)
            .WithAuthority(Token.GetAuthorityUri())
            .WithRedirectUri(redirectUri)
            .WithParentActivityOrWindow(parentWindow)
            .Build();
      }

   }
}
