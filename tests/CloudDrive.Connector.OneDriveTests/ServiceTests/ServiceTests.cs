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
         var value = Assert.Throws<ArgumentException>(() => service.GetIDs(itemID));

         Assert.Equal(expected, value.Message);
      }

      [Fact]
      public void GetIDs_WithValidArguments_MustResultSpectedValue()
      {
         var client = ClientBuilder.Create().Build();
         var service = new OneDriveService(client: client);

         var expectedDrive = "drive";
         var expectedFile = "file";
         var value = service.GetIDs($"{expectedDrive}!{expectedFile}");

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

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      public void GetPath_WithInvalidArguments_MustResultEmpty(string path)
      {
         var client = ClientBuilder.Create().Build();
         var service = new OneDriveService(client: client);

         var expected = "";
         var value = service.GetPath(path);

         Assert.Equal(expected, value);
      }

      [Theory]
      [InlineData("/drive/root:/main/folder/subfolder", "/main/folder/subfolder")]
      [InlineData("/drives/my-shared-drive:/main/folder/subfolder", "/main/folder/subfolder")]
      [InlineData("/root/drive:/main/folder/subfolder", "/root/drive:/main/folder/subfolder")]
      [InlineData("/drive/root:/main/folder:/subfolder", "/main/folder:/subfolder")]
      [InlineData("/drive/root/main/folder/subfolder", "/drive/root/main/folder/subfolder")]
      [InlineData("/root%20folder/sub%26folder", "/root folder/sub&folder")]
      public void GetPath_WithValidArguments_MustResultAsSpected(string path, string expected)
      {
         var client = ClientBuilder.Create().Build();
         var service = new OneDriveService(client: client);

         var value = service.GetPath(path);

         Assert.Equal(expected, value);
      }

   }
}
