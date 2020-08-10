using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal interface IOneDriveToken
   {

      Task<bool> ConnectAsync();
      Task<bool> CheckConnectionAsync();
      Task DisconnectAsync();

      string GetToken();

   }
}
