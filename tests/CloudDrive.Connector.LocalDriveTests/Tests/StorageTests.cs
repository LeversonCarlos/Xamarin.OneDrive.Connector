using System.ComponentModel.Design;
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

      [Fact]
      public async void Service_WithoutConnection_MustReturnNull()
      {
         var connection = ConnectionBuilder.Create().WithCheckConnectionValue(false).Build();
         var service = new LocalDriveService(connection);

         var value = await service.GetDrives();

         Assert.Null(value);
      }
      [Fact]
      public async void Service_GetStorageList_MustReturnSpectedValue()
      {
         var service = new LocalDriveService();

         var expectedValue = System.IO.DriveInfo
            .GetDrives()
            .Select(drive => drive.RootDirectory.FullName)
            .ToArray();
         var drivesList = await service.GetDrives();
         var value = drivesList.Select(x => x.ID).ToArray();

         Assert.Equal(expectedValue, value);
      }

   }
}
