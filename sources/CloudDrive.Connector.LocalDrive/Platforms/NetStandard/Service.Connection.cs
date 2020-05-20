using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService 
   {

      public Task<bool> Connect() => Task.FromResult(true);
      public Task Disconnect() => Task.CompletedTask;
      public Task<bool> IsConnected() => Task.FromResult(true);

   }
}