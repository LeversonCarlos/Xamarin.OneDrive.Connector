using System;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      async Task RemoveTokenAsync()
      {
         try
         {
            var accounts = await this.Identity.GetAccountsAsync();
            if (accounts != null && accounts.Count() != 0)
            {
               foreach (var account in accounts)
               { await this.Identity.RemoveAsync(account); }
            }
         }
         catch (Exception) { throw; }
      }

   }
}
