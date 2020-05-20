using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService 
   {
      
      public Task<bool> Connect() => Permissions.AskStoragePermission();
      public Task Disconnect() => Task.CompletedTask;
      public Task<bool> IsConnected() => Permissions.HasStoragePermission();

   }
}