using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class StorageTests
   {

      [Fact]
      public async void Storage_GetStorageList_MustReturnSpectedValue()
      {
         var service = new LocalDriveStorage();

         var expectedValue = System.IO.DriveInfo
            .GetDrives()
            .Select(drive => drive.RootDirectory.FullName)
            .ToArray();
         var value = await service.GetStorageList();

         Assert.Equal(expectedValue, value);
      }



   }
}
