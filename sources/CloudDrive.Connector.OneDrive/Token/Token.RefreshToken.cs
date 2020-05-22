using System;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      async Task<bool> RefreshToken()
      {
         try
         {
            var accounts = await this.Client.GetAccountsAsync();
            if (accounts != null && accounts.Count() != 0)
            {
               var account = accounts.FirstOrDefault();
               if (account != null)
               {
                  this.AuthResult = await this.Client.AcquireTokenSilent(this.Scopes, account).ExecuteAsync();
               }
            }
            return this.IsAuthValid();
         }
         catch (Exception) { throw; }
      }

   }
}
