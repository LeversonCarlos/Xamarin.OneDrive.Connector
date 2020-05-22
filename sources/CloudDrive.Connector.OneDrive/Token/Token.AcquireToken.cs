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
            await Task.CompletedTask;
            return false;
         }
         catch (Exception) { throw; }
      }

   }
}
