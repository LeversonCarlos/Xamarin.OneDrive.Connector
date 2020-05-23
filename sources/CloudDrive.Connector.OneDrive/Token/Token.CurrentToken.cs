using Microsoft.Identity.Client;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      AuthenticationResult AuthResult { get; set; }

      public string GetToken()
      {
         if (!this.IsTokenValid()) { return ""; }
         return this.AuthResult.AccessToken;
      }

   }
}
