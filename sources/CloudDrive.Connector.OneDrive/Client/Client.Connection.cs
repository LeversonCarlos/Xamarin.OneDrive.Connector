using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveClient
   {

      public Task<bool> ConnectAsync() =>
         Token.ConnectAsync();

      public Task<bool> CheckConnectionAsync() =>
         Token.CheckConnectionAsync();

      public Task DisconnectAsync() =>
         Token.DisconnectAsync();

   }
}
