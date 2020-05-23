using System;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{
   partial class Token
   {

      internal async Task<bool> ConnectAsync()
      {
         try
         {

            // TOKEN STILL VALID
            if (this.IsValid()) { return true; }

            // REFRESH AN EXPIRED TOKEN 
            if (await this.RefreshAsync()) { return true; }

            // ACQUIRE A NEW TOKEN 
            if (await this.AcquireAsync()) { return true; }

            // OTHERWISE
            return false;

         }
         catch (Exception) { throw; }
      }

      internal async Task DisconnectAsync()
      {
         try
         {
            var accounts = await this.Client.GetAccountsAsync();
            if (accounts != null && accounts.Count() != 0)
            {
               foreach (var account in accounts)
               { await this.Client.RemoveAsync(account); }
            }

         }
         catch (Exception) { throw; }
      }

   }
}