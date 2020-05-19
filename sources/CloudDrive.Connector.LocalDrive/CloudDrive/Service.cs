using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   public partial class LocalDriveService : ICloudDriveService
   {

      public Task<FileVM[]> GetFiles(DirectoryVM directory) => throw new System.NotImplementedException();
      public Task<FileVM> GetDetails(string fileID) => throw new System.NotImplementedException();

      public Task<FileVM> Upload(string fileID, byte[] fileContent) => throw new System.NotImplementedException();
      public Task<FileVM> Upload(string directoryID, string fileName, byte[] fileContent) => throw new System.NotImplementedException();
      public Task<byte[]> Download(string fileID) => throw new System.NotImplementedException();

   }
}