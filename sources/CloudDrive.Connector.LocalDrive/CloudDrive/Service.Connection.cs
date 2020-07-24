using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      internal LocalDriveService(IConnection connection) =>
         _Connection = connection;

      IConnection _Connection { get; }

      public Task<bool> ConnectAsync() => _Connection.ConnectAsync();
      public Task DisconnectAsync() => _Connection.DisconnectAsync();
      public Task<bool> CheckConnectionAsync() => _Connection.CheckConnectionAsync();

   }
}