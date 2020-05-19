using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService 
   {

      public Task<FileVM[]> GetFiles(DirectoryVM directory) => throw new System.NotImplementedException();
      public Task<FileVM> GetDetails(string fileID) => throw new System.NotImplementedException();

   }
}