using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.Common
{
   partial interface ICloudDriveService
   {

      Task<ProfileVM> GetProfile();

   }
}