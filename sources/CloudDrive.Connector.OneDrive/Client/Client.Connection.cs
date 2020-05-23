using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Client
   {

      public Task<bool> ConnectAsync() => this.Token.ConnectAsync();
      public Task<bool> CheckConnectionAsync() => this.Token.CheckConnectionAsync();
      public Task DisconnectAsync() => this.Token.DisconnectAsync();

   }
}
