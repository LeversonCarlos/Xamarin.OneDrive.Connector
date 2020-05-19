using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.Common
{
   partial interface ICloudDriveService
   {

      Task<FileVM[]> GetFiles(DirectoryVM directory);
      Task<FileVM> GetDetails(string fileID);

      Task<FileVM> Upload(string fileID, byte[] fileContent);
      Task<FileVM> Upload(string directoryID, string fileName, byte[] fileContent);
      
      Task<byte[]> Download(string fileID);

   }
}