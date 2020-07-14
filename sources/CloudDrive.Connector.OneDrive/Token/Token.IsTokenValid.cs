using System;
using System.Linq;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      bool IsTokenValid()
      {
         if (AuthResult == null) return false;
         if (string.IsNullOrEmpty(AuthResult.AccessToken)) return false;
         if (AuthResult.ExpiresOn < DateTimeOffset.UtcNow.AddMinutes(-1)) return false;
         if (!IsScopeValid()) return false;
         return true;
      }

      bool IsScopeValid()
      {
         if (AuthResult == null) return false;
         if (AuthResult.Scopes == null) return false;
         foreach (var scope in Scopes)
            if (!AuthResult.Scopes.Contains(scope.ToLower())) return false;
         return true;
      }

   }
}
