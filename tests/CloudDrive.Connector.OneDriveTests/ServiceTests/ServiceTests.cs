using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public partial class ServiceTests
   {

      [Fact]
      public void Constructor_WithNullArguments_MustThrowException()
      {
         var creator = new Action(() => new OneDriveService(client: null));

         var expected = "The client argument for the OneDrive service must be set";
         var value = Assert.Throws<ArgumentException>(creator);

         Assert.Equal(expected, value.Message);
      }

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      [InlineData("file")]
      [InlineData("file.ext")]
      [InlineData("file!")]
      [InlineData("drive!file!ext")]
      public void GetIDs_WithInvalidArguments_MustThrowException(string itemID)
      {
         var client = ClientBuilder.Create().Build();
         var service = new OneDriveService(client: client);

         var expected = "The directory ID for the onedrive client is invalid";
         var value = Assert.Throws<ArgumentException>(()=> service.GetIDs(itemID));

         Assert.Equal(expected, value.Message);
      }

      [Fact]
      public void GetIDs_WithValidArguments_MustResultSpectedValue()
      {
         var client = ClientBuilder.Create().Build();
         var service = new OneDriveService(client: client);

         var expectedDrive = "drive";
         var expectedFile = "file";
         var value =  service.GetIDs($"{expectedDrive}!{expectedFile}");

         Assert.Equal(expectedDrive, value.DriveID);
         Assert.Equal($"{expectedDrive}!{expectedFile}", value.ID);
      }

      [Fact]
      public void GetIDs_WithValidRootArguments_MustResultSpectedValue()
      {
         var client = ClientBuilder.Create().Build();
         var service = new OneDriveService(client: client);

         var expectedDrive = "drive";
         var expectedFile = "root";
         var value = service.GetIDs($"{expectedDrive}!{expectedFile}");

         Assert.Equal(expectedDrive, value.DriveID);
         Assert.Equal(expectedFile, value.ID);
      }

   }
}
