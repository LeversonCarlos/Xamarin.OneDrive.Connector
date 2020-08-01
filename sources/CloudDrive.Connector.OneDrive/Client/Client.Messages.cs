using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveClient
   {

      public Task<HttpResponseMessage> GetAsync(string httpPath) =>
         _HttpClient.GetAsync(httpPath);

      public Task<HttpResponseMessage> PutAsync(string httpPath, HttpContent httpParameter) =>
         _HttpClient.PutAsync(httpPath, httpParameter);

      public Task<HttpResponseMessage> PostAsync(string httpPath, HttpContent httpParameter) =>
         _HttpClient.PostAsync(httpPath, httpParameter);

      public async Task<T> GetAsync<T>(string httpPath)
      {
         var httpMessage = await GetAsync(httpPath);
         var httpResult = await GetValueAsync<T>(httpMessage);
         return httpResult;
      }

      public async Task<T> PostAsync<T>(string httpPath, HttpContent parameter)
      {
         HttpContent httpParameter = (dynamic)parameter;
         var httpMessage = await PostAsync(httpPath, httpParameter);
         var httpResult = await GetValueAsync<T>(httpMessage);
         return httpResult;
      }

      public async Task<R> PostAsync<T, R>(string httpPath, T parameter)
      {
         var jsonParameter = System.Text.Json.JsonSerializer.Serialize(parameter, new System.Text.Json.JsonSerializerOptions { });
         HttpContent httpParameter = new StringContent(jsonParameter, System.Text.Encoding.UTF8, "application/json");
         var httpMessage = await PostAsync(httpPath, httpParameter);
         var httpResult = await GetValueAsync<R>(httpMessage);
         return httpResult;
      }

      async Task<T> GetValueAsync<T>(HttpResponseMessage httpMessage)
      {
         if (!httpMessage.IsSuccessStatusCode)
            throw new Exception(await httpMessage.Content.ReadAsStringAsync());
         var httpContent = await httpMessage.Content.ReadAsStreamAsync();
         var httpResult = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(httpContent);
         return httpResult;
      }

   }
}
