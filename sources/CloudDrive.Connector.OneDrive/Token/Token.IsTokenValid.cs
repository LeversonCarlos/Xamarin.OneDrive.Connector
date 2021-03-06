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

      internal bool IsScopeValid()
      {
         if (_AuthResult == null) return false;
         if (_AuthResult.Scopes == null) return false;
         if (Identity.Scopes == null) return false;

         var identityScopes = Identity.Scopes.Select(x => x.ToLower()).ToArray();
         var authScopes = _AuthResult.Scopes.Select(x => x.ToLower()).ToArray();
         foreach (var scope in identityScopes)
            if (!authScopes.Contains(scope)) return false;

         return true;
      }

   }
}
