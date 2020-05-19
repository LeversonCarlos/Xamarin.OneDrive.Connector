using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.Common
{
   partial interface ICloudDriveService
   {

      Task<DirectoryVM[]> GetDrivers();
      Task<DirectoryVM[]> GetDirectories(DirectoryVM directory);

   }
}