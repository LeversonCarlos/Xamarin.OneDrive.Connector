using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class OneDriveService
   {

      public Task<bool> ConnectAsync() => this.Client.ConnectAsync();
      public Task<bool> CheckConnectionAsync() => this.Client.CheckConnectionAsync();
      public Task DisconnectAsync() => this.Client.DisconnectAsync();

   }
}