using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.Common
{
   partial interface ICloudDriveService
   {

      Task<DirectoryVM[]> GetDrivers();
      Task<DirectoryVM[]> GetFolders(DirectoryVM value);

   }
}