using Microsoft.Identity.Client;
using System;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      AuthenticationResult AuthResult { get; set; }

      bool IsAuthValid()
      {
         if (this.AuthResult == null) { return false; }
         if (string.IsNullOrEmpty(this.AuthResult.AccessToken)) { return false; }
         if (this.AuthResult.ExpiresOn < DateTimeOffset.UtcNow.AddMinutes(1)) { return false; }
         return true;
      }

      public string GetCurrentToken()
      {
         if (!this.IsAuthValid()) { return ""; }
         return this.AuthResult.AccessToken;
      }

   }
}
