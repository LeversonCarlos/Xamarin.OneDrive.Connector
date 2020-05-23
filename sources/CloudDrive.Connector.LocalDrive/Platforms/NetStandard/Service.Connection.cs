using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService 
   {

      public Task<bool> ConnectAsync() => Task.FromResult(true);
      public Task DisconnectAsync() => Task.CompletedTask;
      public Task<bool> IsConnected() => Task.FromResult(true);

   }
}