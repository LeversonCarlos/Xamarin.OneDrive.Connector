using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService 
   {
      
      public Task<bool> ConnectAsync() => Permissions.AskStoragePermission();
      public Task DisconnectAsync() => Task.CompletedTask;
      public Task<bool> IsConnected() => Permissions.HasStoragePermission();

   }
}