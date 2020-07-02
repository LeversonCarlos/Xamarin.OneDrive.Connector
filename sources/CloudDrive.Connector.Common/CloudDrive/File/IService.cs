using System.IO;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial interface ICloudDriveService
   {

      Task<FileVM[]> SearchFiles(DirectoryVM directory, string searchPattern, int limit);
      Task<FileVM[]> GetFiles(DirectoryVM directory);
      Task<FileVM> GetDetails(string fileID);

      Task<FileVM> Upload(string fileID, byte[] fileContent);
      Task<FileVM> Upload(string directoryID, string fileName, byte[] fileContent);
      
      Task<Stream> Download(string fileID);

   }
}