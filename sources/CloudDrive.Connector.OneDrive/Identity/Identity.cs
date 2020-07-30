using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal partial class OneDriveIdentity : IOneDriveIdentity
   {

      // tenants { common, organizations, consumers }
      const string Authority = "https://login.microsoftonline.com/{tenant}";
      static Uri _GetAuthorityUri() => new Uri(Authority.Replace("{tenant}", "common"));

      string[] _Scopes;
      IClientApplicationBase _Identity;

      Task<IEnumerable<IAccount>> GetAccountsAsync() =>
         _Identity.GetAccountsAsync();

      Task<AuthenticationResult> AcquireTokenSilentAsync(IAccount account) =>
         _Identity
            .AcquireTokenSilent(_Scopes, account)
            .ExecuteAsync();

   }
}
