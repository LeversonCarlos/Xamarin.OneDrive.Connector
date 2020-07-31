using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      public async Task<bool> CheckConnectionAsync()
      {

         // CHECK IF TOKEN IS STILL VALID
         if (IsTokenValid()) return true;

         // TRY TO REFRESH AN EXPIRED TOKEN 
         if (await RefreshTokenAsync()) return true;

         // OTHERWISE
         return false;

      }

   }
}
