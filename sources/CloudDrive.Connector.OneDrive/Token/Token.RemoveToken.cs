using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      internal async Task RemoveTokenAsync()
      {
         try
         {
            var accounts = await Identity.GetAccountsAsync();
            if (accounts != null)
            {
               foreach (var account in accounts)
                  await Identity.RemoveAsync(account);
            }
         }
         catch (Exception) { throw; }
      }

   }
}
