using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial interface ICloudDriveService
   {

      Task<DirectoryVM[]> GetDrives();
      Task<DirectoryVM[]> GetDirectories(DirectoryVM directory);

   }
}