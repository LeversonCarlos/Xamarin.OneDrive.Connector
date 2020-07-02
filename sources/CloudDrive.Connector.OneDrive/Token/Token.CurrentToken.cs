using Microsoft.Identity.Client;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      AuthenticationResult AuthResult { get; set; }

      public string GetToken()
      {
         if (!this.IsTokenValid()) { return ""; }
         return this.AuthResult.AccessToken;
      }

   }
}
