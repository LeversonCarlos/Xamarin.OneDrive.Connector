using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Client
   {

      public async Task DisconnectAsync()
      {
         try
         {
            await this.Token.RemoveTokenAsync();
         }
         catch (Exception) { throw; }
      }

   }
}
