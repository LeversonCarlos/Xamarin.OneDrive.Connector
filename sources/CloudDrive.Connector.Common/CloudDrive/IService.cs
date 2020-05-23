using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.Common
{
   public partial interface ICloudDriveService
   {

      Task<bool> ConnectAsync();
      Task DisconnectAsync();

      Task<bool> CheckConnectionAsync();

   }
}