using System;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{
   partial class Token
   {

      internal async Task<bool> RefreshAsync()
      {
         try
         {
            var accounts = await this.Client.GetAccountsAsync();
            if (accounts != null && accounts.Count() != 0)
            {
               var account = accounts.FirstOrDefault();
               if (account != null)
               {
                  this.AuthResult = await this.Client.AcquireTokenSilentAsync(this.Configs.Scopes, account);
               }
            }
            return this.IsValid();
         }
         catch (Exception) { throw; }
      }

   }
}