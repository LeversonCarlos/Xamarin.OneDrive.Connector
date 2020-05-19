using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.Common
{
   public partial interface ICloudDriveService
   {

      Task<bool> Connect();
      Task Disconnect();

      Task<bool> IsConnected();

   }
}