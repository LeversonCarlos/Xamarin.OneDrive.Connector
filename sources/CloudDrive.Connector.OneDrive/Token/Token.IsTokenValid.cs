using System;
using System.Linq;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      internal bool IsTokenValid()
      {
         if (_AuthResult == null) return false;
         if (string.IsNullOrEmpty(_AuthResult.AccessToken)) return false;
         if (_AuthResult.ExpiresOn < DateTimeOffset.UtcNow.AddMinutes(-1)) return false;
         if (!IsScopeValid()) return false;
         return true;
      }

      bool IsScopeValid()
      {
         if (_AuthResult == null) return false;
         if (_AuthResult.Scopes == null) return false;
         foreach (var scope in this.Identity.Scopes)
            if (!_AuthResult.Scopes.Contains(scope.ToLower())) return false;
         return true;
      }

   }
}
