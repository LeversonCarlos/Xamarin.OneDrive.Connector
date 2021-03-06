using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal interface IOneDriveIdentity
   {

      string[] Scopes { get; }

      Task<AuthenticationResult> AcquireTokenFromIdentityAsync();
      Task<IEnumerable<IAccount>> GetAccountsAsync();
      Task<AuthenticationResult> AcquireTokenSilentAsync(IAccount account);
      Task RemoveAsync(IAccount account);

   }
}
