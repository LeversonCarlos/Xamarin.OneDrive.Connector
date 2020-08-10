using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public Task<bool> ConnectAsync() =>
         Client.ConnectAsync();

      public Task<bool> CheckConnectionAsync() =>
         Client.CheckConnectionAsync();

      public Task DisconnectAsync() =>
         Client.DisconnectAsync();

   }
}