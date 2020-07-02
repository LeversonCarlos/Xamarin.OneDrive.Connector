using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      internal static IClientApplicationBase CreateIdentity(string clientID, string redirectUri, string clientSecret)
      {
         return ConfidentialClientApplicationBuilder
            .Create(clientID)
            .WithAuthority(OneDriveToken.GetAuthorityUri())
            .WithRedirectUri(redirectUri)
            .WithClientSecret(clientSecret)
            .Build();
      }

      private Task<AuthenticationResult> AcquireTokenFromIdentity()
      {
         return (this.Identity as IConfidentialClientApplication)?.AcquireTokenForClient(this.Scopes).ExecuteAsync();
      }

   }
}
