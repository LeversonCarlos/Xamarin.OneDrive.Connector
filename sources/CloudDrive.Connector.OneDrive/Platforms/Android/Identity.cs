using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveIdentity
   {

      public OneDriveIdentity()
      {
         var settings = Dependency.GetService<OneDriveSettings>();
         if (settings == null)
            throw new ArgumentException("The settings object for the onedrive client wasnt found on dependency service");
         _Identity = PublicClientApplicationBuilder
            .Create(settings.ClientID)
            .WithAuthority(_GetAuthorityUri())
            .WithRedirectUri(settings.RedirectUri)
            .WithParentActivityOrWindow(() => settings.Activity)
            .Build();
         _Scopes = settings.Scopes;
      }

      public Task<AuthenticationResult> AcquireTokenFromIdentity() =>
         (_Identity as IPublicClientApplication)
            ?.AcquireTokenInteractive(_Scopes)
            ?.ExecuteAsync();

   }
}
