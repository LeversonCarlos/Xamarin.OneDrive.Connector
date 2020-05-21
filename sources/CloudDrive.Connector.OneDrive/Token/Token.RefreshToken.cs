using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      async Task<bool> RefreshToken()
      {
         try
         {

            await Task.CompletedTask;
            return false;

         }
         catch (Exception) { throw; }
      }

   }
}
