using Microsoft.Identity.Client;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      static IClientApplicationBase CreateClientApplication(string clientID, string redirectUri, string clientSecret)
      {
         return ConfidentialClientApplicationBuilder
            .Create(clientID)
            .WithAuthority(Token.GetAuthorityUri())
            .WithRedirectUri(redirectUri)
            .WithClientSecret(clientSecret)
            .Build();
      }

   }
}
