using System;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
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
                  this.AuthResult = await this.Identity.AcquireTokenSilent(this.Scopes, account).ExecuteAsync();
               }
            }
            return this.IsTokenValid();
         }
         catch (Exception) { throw; }
      }

   }
}
