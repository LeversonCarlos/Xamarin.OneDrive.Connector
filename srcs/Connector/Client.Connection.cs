using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{
   partial class Connector
   {

      public async Task<bool> ConnectAsync()
      {
         try
         {
            var httpContent = new StringContent(ConnectorHandler.InnerConnectionConnect);
            var httpMessage = await this.PostAsync(ConnectorHandler.InnerConnectionPath, httpContent);
            if (httpMessage.IsSuccessStatusCode) { return true; }
            else
            {
               var httpResult = await httpMessage.Content.ReadAsStringAsync();
               throw new Exception(httpResult);
            }
         }
         catch (Exception) { throw; }
      }

      public async Task<bool> DisconnectAsync()
      {
         try
         {
            var httpContent = new StringContent(ConnectorHandler.InnerConnectionDisconnect);
            var httpMessage = await this.PostAsync(ConnectorHandler.InnerConnectionPath, httpContent);
            if (httpMessage.IsSuccessStatusCode) { return true; }
            else
            {
               var httpResult = await httpMessage.Content.ReadAsStringAsync();
               throw new Exception(httpResult);
            }
         }
         catch (Exception) { throw; }
      }


   }
}