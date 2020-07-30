using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      async Task<bool> AcquireTokenAsync()
      {
         try
         {
            this.AuthResult = await this.Identity.AcquireTokenFromIdentityAsync();
            if (!this.IsTokenValid()) { return false; }
            return true;
         }
         catch (Exception) { throw; }
      }

   }
}
