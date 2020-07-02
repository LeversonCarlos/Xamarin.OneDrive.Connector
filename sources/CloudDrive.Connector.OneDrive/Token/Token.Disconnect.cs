using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      public async Task DisconnectAsync()
      {
         try
         {
            await this.RemoveTokenAsync();
         }
         catch (Exception) { throw; }
      }

   }
}
