using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      public async Task DisconnectAsync()
      {
         try
         {
            var accounts = await Identity.GetAccountsAsync();
            if (accounts != null)
            {
               foreach (var account in accounts)
                  await Identity.RemoveAsync(account);
            }
            _AuthResult = null;
         }
         catch (Exception) { throw; }
      }

   }
}
