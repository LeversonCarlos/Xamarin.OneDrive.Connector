using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveClient
   {

      public async Task<T> GetAsync<T>(string httpPath)
      {
         var httpMessage = await this.GetAsync(httpPath);
         var httpResult = await this.GetValueAsync<T>(httpMessage);
         return httpResult;
      }

      public async Task<T> PostAsync<T>(string httpPath, HttpContent parameter)
      {
         HttpContent httpParameter = (dynamic)parameter;
         var httpMessage = await this.PostAsync(httpPath, httpParameter);
         var httpResult = await this.GetValueAsync<T>(httpMessage);
         return httpResult;
      }

      public async Task<R> PostAsync<T, R>(string httpPath, T parameter)
      {
         var jsonParameter = System.Text.Json.JsonSerializer.Serialize(parameter, new System.Text.Json.JsonSerializerOptions { });
         HttpContent httpParameter = new StringContent(jsonParameter, System.Text.Encoding.UTF8, "application/json");
         var httpMessage = await this.PostAsync(httpPath, httpParameter);
         var httpResult = await this.GetValueAsync<R>(httpMessage);
         return httpResult;
      }

      private async Task<T> GetValueAsync<T>(HttpResponseMessage httpMessage)
      {
         if (!httpMessage.IsSuccessStatusCode) throw new Exception(await httpMessage.Content.ReadAsStringAsync());
         var httpContent = await httpMessage.Content.ReadAsStreamAsync();
         var httpResult = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(httpContent);
         return httpResult;
      }

   }
}
