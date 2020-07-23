using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      IConnection _Connection { get; } = new LocalDriveConnection();

      public Task<bool> ConnectAsync() => _Connection.ConnectAsync();
      public Task DisconnectAsync() => _Connection.DisconnectAsync();
      public Task<bool> CheckConnectionAsync() => _Connection.CheckConnectionAsync();

   }
}