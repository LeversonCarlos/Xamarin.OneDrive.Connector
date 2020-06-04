using System;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      bool IsTokenValid()
      {
         if (this.AuthResult == null) { return false; }
         if (string.IsNullOrEmpty(this.AuthResult.AccessToken)) { return false; }
         if (this.AuthResult.ExpiresOn < DateTimeOffset.UtcNow.AddMinutes(2)) { return false; }
         return true;
      }

   }
}
