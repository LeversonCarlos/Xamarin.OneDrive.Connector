using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
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
