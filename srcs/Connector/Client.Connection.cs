using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{
   partial class Connector
   {

      public async Task<bool> ConnectAsync()
      {
         return await this.ConnectorAsync(ConnectorHandler.InnerConnectionConnect);
      }

      public async Task<bool> DisconnectAsync()
      {
         return await this.ConnectorAsync(ConnectorHandler.InnerConnectionDisconnect);
      }

      private async Task<bool> ConnectorAsync(string connectorState)
      {
         try
         {
            var httpParam = new StringContent(connectorState);
            var httpMessage = await this.PostAsync(ConnectorHandler.InnerConnectionPath, httpParam);
            if (httpMessage.IsSuccessStatusCode) { return true; }
            else
            {
               var httpReason = httpMessage.ReasonPhrase;
               var httpContent = string.Empty;
               if (httpMessage.Content != null) { 
                  httpContent = await httpMessage.Content.ReadAsStringAsync();
               }
               throw new Exception($"{httpReason}\n{httpContent}");
            }
         }
         catch (Exception) { throw; }
      }

   }
}