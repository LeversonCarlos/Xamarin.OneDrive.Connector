using System;
using System.IO;
using System.Net.Http;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      internal async void UploadThumbnail_WithExceptionOnCall_MustThrowException()
      {
         var driveID = "driveID";
         var fileID = "fileID";
         var imageStream = new MemoryStream(new byte[] { });
         var exception = new Exception($"Error while uploading thumbnail for file [{driveID}!{fileID}] with oneDrive service");
         var client = ClientBuilder.Create().With("", exception).Build();
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.UploadThumbnail($"{driveID}!{fileID}", imageStream));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

      [Fact]
      internal async void UploadThumbnail_WithBadResponse_MustThrowException()
      {
         var driveID = "driveID";
         var fileID = "fileID";
         var imageStream = new MemoryStream(new byte[] { });
         var exceptionMessage = $"Error while uploading thumbnail for file [{driveID}!{fileID}] with oneDrive service";
         var client = ClientBuilder.Create().With("", new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)).Build();
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.UploadThumbnail($"{driveID}!{fileID}", imageStream));

         Assert.NotNull(value);
         Assert.Equal(exceptionMessage, value.Message);
      }

      [Fact]
      internal async void UploadThumbnail_WithValidData_MustResultAsSpected()
      {
         var driveID = "driveID";
         var fileID = "fileID";
         var imageStream = new MemoryStream(new byte[] { });
         var fileDTO = new DTOs.File { id = "fileID" };
         var client = ClientBuilder.Create().With("", fileDTO).Build();
         var service = new OneDriveService(client);

         var value = await service.UploadThumbnail($"{driveID}!{fileID}", imageStream);

         Assert.True(value);
      }

   }
}
