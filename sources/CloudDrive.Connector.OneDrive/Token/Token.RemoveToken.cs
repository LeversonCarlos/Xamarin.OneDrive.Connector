using System;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
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
