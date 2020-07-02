using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial interface ICloudDriveService
   {

      Task<DirectoryVM[]> GetDrivers();
      Task<DirectoryVM[]> GetDirectories(DirectoryVM directory);

   }
}