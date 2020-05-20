using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService
   {

      public async Task<FileVM[]> GetFiles(DirectoryVM directory) => throw new System.NotImplementedException();
      public async Task<FileVM> GetDetails(string fileID) => throw new System.NotImplementedException();

   }
}