using System;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      internal async Task<bool> RefreshTokenAsync()
      {
         try
         {
            var accounts = await Identity.GetAccountsAsync();
            var account = accounts?.FirstOrDefault();
            if (account != null)
               _AuthResult = await Identity.AcquireTokenSilentAsync(account);
            return this.IsTokenValid();
         }
         catch (Exception) { return false; }
      }

   }
}
