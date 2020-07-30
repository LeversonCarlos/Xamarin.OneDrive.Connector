using System;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      async Task<bool> RefreshTokenAsync()
      {
         try
         {
            var accounts = await this.Identity.GetAccountsAsync();
            if (accounts != null && accounts.Count() != 0)
            {
               var account = accounts.FirstOrDefault();
               if (account != null)
               {
                  this.AuthResult = await this.Identity.AcquireTokenSilentAsync(account);
               }
            }
            return this.IsTokenValid();
         }
         catch (Exception) { return false; }
      }

   }
}
