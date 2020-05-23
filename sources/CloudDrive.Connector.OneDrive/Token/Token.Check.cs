using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      public async Task<bool> CheckConnectionAsync()
      {
         try
         {

            // CHECK IF TOKEN IS STILL VALID
            if (this.IsTokenValid()) { return true; }

            // TRY TO REFRESH AN EXPIRED TOKEN 
            if (await this.RefreshTokenAsync()) { return true; }

            // OTHERWISE
            return false;

         }
         catch (Exception) { throw; }
      }

   }
}
