using System;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      public bool IsTokenValid()
      {
         if (this.AuthResult == null) { return false; }
         if (string.IsNullOrEmpty(this.AuthResult.AccessToken)) { return false; }
         if (this.AuthResult.ExpiresOn < DateTimeOffset.UtcNow.AddMinutes(1)) { return false; }
         return true;
      }

   }
}
