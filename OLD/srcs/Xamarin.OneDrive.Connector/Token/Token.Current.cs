using System;
using Microsoft.Identity.Client;

namespace Xamarin.OneDrive
{
   partial class Token
   { 
      AuthenticationResult AuthResult { get; set; }

      internal string CurrentToken
      {
         get
         {
            if (!this.IsValid()) { return string.Empty; }
            return this.AuthResult.AccessToken;
         }
      }

      internal bool IsValid()
      {
         if (this.AuthResult == null) { return false; }
         if (string.IsNullOrEmpty(this.AuthResult.AccessToken)) { return false; }
         return (this.AuthResult.ExpiresOn > DateTimeOffset.UtcNow.AddMinutes(5));
      }

   }
}