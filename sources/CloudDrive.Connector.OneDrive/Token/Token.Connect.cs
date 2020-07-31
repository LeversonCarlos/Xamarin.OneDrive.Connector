using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      public async Task<bool> ConnectAsync()
      {

         // CHECK IF TOKEN IS STILL VALID
         if (IsTokenValid()) return true;

         // TRY TO REFRESH AN EXPIRED TOKEN 
         if (await RefreshTokenAsync()) return true;

         // ACQUIRE A NEW TOKEN THAT WILL ASK USER FOR AUTHENTICATION
         if (await AcquireTokenAsync()) return true;

         // OTHERWISE
         return false;

      }

   }
}
