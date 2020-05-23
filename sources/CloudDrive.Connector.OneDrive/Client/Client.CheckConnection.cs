using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Client
   {

      public async Task<bool> CheckConnectionAsync()
      {
         try
         {

            // CHECK IF TOKEN IS STILL VALID
            if (this.Token.IsTokenValid()) { return true; }

            // TRY TO REFRESH AN EXPIRED TOKEN 
            if (await this.Token.RefreshTokenAsync()) { return true; }

            // OTHERWISE
            return false;

         }
         catch (Exception) { throw; }
      }

   }
}
