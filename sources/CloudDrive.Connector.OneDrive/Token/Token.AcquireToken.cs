using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      internal async Task<bool> AcquireTokenAsync()
      {
         _AuthResult = await Identity.AcquireTokenFromIdentityAsync();
         return IsTokenValid();
      }

   }
}
