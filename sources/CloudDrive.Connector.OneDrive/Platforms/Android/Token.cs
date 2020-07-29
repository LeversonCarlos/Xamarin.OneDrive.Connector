using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      internal OneDriveToken()
      {
         var settings = Dependency.GetService<OneDriveSettings>();
         if (settings == null)
            throw new ArgumentException("The settings object for the onedrive client wasnt found on dependency service");
         Identity = PublicClientApplicationBuilder
            .Create(settings.ClientID)
            .WithAuthority(OneDriveToken.GetAuthorityUri())
            .WithRedirectUri(settings.RedirectUri)
            .WithParentActivityOrWindow(() => settings.Activity)
            .Build();
         Scopes = settings.Scopes;
      }

      Task<AuthenticationResult> AcquireTokenFromIdentity() =>
         (this.Identity as IPublicClientApplication)
            ?.AcquireTokenInteractive(this.Scopes)
            ?.ExecuteAsync();

   }
}
