using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal class LocalDriveConnection : IConnection
   {

      public Task<bool> ConnectAsync() => Task.FromResult(true);
      public Task DisconnectAsync() => Task.CompletedTask;
      public Task<bool> CheckConnectionAsync() => Task.FromResult(true);

   }
}