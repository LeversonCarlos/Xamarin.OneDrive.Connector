using System.Net.Http;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   public interface IOneDriveClient
   {

      Task<bool> ConnectAsync();
      Task<bool> CheckConnectionAsync();
      Task DisconnectAsync();

      Task<HttpResponseMessage> GetAsync(string httpPath);
      Task<T> GetAsync<T>(string httpPath);
      Task<T> PostAsync<T>(string httpPath, HttpContent parameter);
      Task<R> PostAsync<T, R>(string httpPath, T parameter);
      Task<HttpResponseMessage> PutAsync(string httpPath, HttpContent httpParameter);

   }
}
