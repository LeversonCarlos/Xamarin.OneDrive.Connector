using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal interface IConnection
   {
      Task<bool> ConnectAsync();
      Task DisconnectAsync();

      Task<bool> CheckConnectionAsync();
   }
}
