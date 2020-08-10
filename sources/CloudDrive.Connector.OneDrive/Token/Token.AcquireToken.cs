using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      internal async Task<bool> AcquireTokenAsync()
      {
         try
         {
            _AuthResult = await Identity.AcquireTokenFromIdentityAsync();
            return IsTokenValid();
         }
         catch (Exception) { return false; }
      }

   }
}
