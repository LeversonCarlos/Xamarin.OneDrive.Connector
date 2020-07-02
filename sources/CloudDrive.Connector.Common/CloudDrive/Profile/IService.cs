using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial interface ICloudDriveService
   {

      Task<ProfileVM> GetProfile();

   }
}