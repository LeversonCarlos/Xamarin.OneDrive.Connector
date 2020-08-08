using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      [InlineData("fileID")]
      internal async void Download_WithInvalidArgument_MustThrowException(string fileID)
      {
         var client = ClientBuilder.Create().Build();
         var exceptionMessage = $"Error while downloading file [{fileID}] with oneDrive service";
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.Download(fileID));

         Assert.NotNull(value);
         Assert.Equal(exceptionMessage, value.Message);
      }

      [Fact]
      internal async void Download_WithExceptionOnCall_MustThrowException()
      {
         var fileID = "driveID!fileID";
         var exception = new Exception($"Error while downloading file [{fileID}] with oneDrive service");
         var client = ClientBuilder.Create().With("", exception).Build();
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.Download(fileID));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

      [Fact]
      internal async void Download_WithValidData_MustResultAsSpected()
      {
         var fileID = "driveID!fileID";
         var expected = new FileVM { ID = "fileID" };
         var client = ClientBuilder.Create().With("", expected).Build();
         var service = new OneDriveService(client);

         var valueStream = await service.Download(fileID);
         var value = await System.Text.Json.JsonSerializer.DeserializeAsync<FileVM>(valueStream);

         Assert.NotNull(value);
         Assert.Equal(expected.ID, value.ID);
      }

   }
}
