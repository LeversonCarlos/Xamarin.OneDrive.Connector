using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      static IClientApplicationBase CreateIdentity(string clientID, string redirectUri, string clientSecret)
      {
         return ConfidentialClientApplicationBuilder
            .Create(clientID)
            .WithAuthority(Token.GetAuthorityUri())
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
