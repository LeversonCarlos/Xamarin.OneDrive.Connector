using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class OneDriveService
   {

      public Task<FileVM> Upload(string fileID, byte[] fileContent) => throw new System.NotImplementedException();
      public Task<FileVM> Upload(string directoryID, string fileName, byte[] fileContent) => throw new System.NotImplementedException();

   }
}