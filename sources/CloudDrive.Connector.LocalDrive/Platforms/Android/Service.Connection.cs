using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService 
   {
      
      public Task<bool> ConnectAsync() => PermissionHelper.AskStoragePermission();
      public Task DisconnectAsync() => Task.CompletedTask;
      public Task<bool> CheckConnectionAsync() => PermissionHelper.HasStoragePermission();

   }
}