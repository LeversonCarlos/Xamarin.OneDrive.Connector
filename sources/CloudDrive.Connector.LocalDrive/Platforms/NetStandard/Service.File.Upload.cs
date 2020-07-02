using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public Task<FileVM> Upload(string fileID, byte[] fileContent) => throw new System.NotImplementedException();
      public Task<FileVM> Upload(string directoryID, string fileName, byte[] fileContent) => throw new System.NotImplementedException();

   }
}