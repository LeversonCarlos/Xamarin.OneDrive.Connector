using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      async Task<bool> AcquireTokenAsync()
      {
         try
         {
            this.AuthResult = await this.AcquireTokenFromIdentity();
            if (!this.IsTokenValid()) { return false; }
            return true;
         }
         catch (Exception) { throw; }
      }

   }
}
