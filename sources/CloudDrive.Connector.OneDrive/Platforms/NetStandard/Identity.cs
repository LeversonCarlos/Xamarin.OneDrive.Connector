using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveIdentity
   {

      public OneDriveIdentity(IServiceProvider serviceProvider)
      {
         var settings = Dependency.GetService<OneDriveSettings>(serviceProvider);
         if (settings == null)
            throw new ArgumentException("The settings object for the onedrive client wasnt found on dependency service");
         Scopes = settings.Scopes;
         _Identity = ConfidentialClientApplicationBuilder
            .Create(settings.ClientID)
            .WithAuthority(_GetAuthorityUri())
            .WithRedirectUri(settings.RedirectUri)
            .WithClientSecret(settings.ClientSecret)
            .Build();
      }

      public Task<AuthenticationResult> AcquireTokenFromIdentityAsync() =>
         (_Identity as IConfidentialClientApplication)
            ?.AcquireTokenForClient(Scopes)
            ?.ExecuteAsync();

   }
}
