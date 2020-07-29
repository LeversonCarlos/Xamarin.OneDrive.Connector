using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      internal OneDriveToken(IServiceProvider serviceProvider)
      {
         var settings = Dependency.GetService<OneDriveSettings>(serviceProvider);
         if (settings == null)
            throw new ArgumentException("The settings object for the onedrive client wasnt found on dependency service");
         Identity = ConfidentialClientApplicationBuilder
            .Create(settings.ClientID)
            .WithAuthority(OneDriveToken.GetAuthorityUri())
            .WithRedirectUri(settings.RedirectUri)
            .WithClientSecret(settings.ClientSecret)
            .Build();
         Scopes = settings.Scopes;
      }

      Task<AuthenticationResult> AcquireTokenFromIdentity() =>
         (this.Identity as IConfidentialClientApplication)
            ?.AcquireTokenForClient(this.Scopes)
            ?.ExecuteAsync();

   }
}
