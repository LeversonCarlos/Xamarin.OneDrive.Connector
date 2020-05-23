using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      public async Task<bool> AcquireTokenAsync()
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
