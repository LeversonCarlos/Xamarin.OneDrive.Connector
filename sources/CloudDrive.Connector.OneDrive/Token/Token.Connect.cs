using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      public async Task<bool> ConnectAsync()
      {
         try
         {

            // CHECK IF TOKEN IS STILL VALID
            if (this.IsTokenValid()) { return true; }

            // TRY TO REFRESH AN EXPIRED TOKEN 
            if (await this.RefreshTokenAsync()) { return true; }

            // ACQUIRE A NEW TOKEN THAT WILL ASK USER FOR AUTHENTICATION
            if (await this.AcquireTokenAsync()) { return true; }

            // OTHERWISE
            return false;

         }
         catch (Exception) { throw; }
      }

   }
}
