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

         var identityScopes = Identity.Scopes.OrderBy(x => x).ToArray();
         var authScopes = _AuthResult.Scopes.OrderBy(x => x).ToArray();
         if (!identityScopes.SequenceEqual(authScopes)) return false;

         return true;
      }

   }
}
