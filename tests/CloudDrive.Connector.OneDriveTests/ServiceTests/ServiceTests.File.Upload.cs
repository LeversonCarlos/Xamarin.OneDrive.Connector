using System;
using System.Net.Http;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      internal async void Upload_WithExceptionOnCall_MustThrowException()
      {
         var driveID = "driveID";
         var fileID = "fileID";
         var httpPath = $"drives/{driveID}/items/{driveID}!{fileID}/content";
         var exception = new Exception($"Error while uploading file [{httpPath}] with oneDrive service");
         var client = ClientBuilder.Create().With("", exception).Build();
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.Upload($"{driveID}!{fileID}", new byte[] { }));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

      [Fact]
      internal async void Upload_WithBadResponse_MustThrowException()
      {
         var driveID = "driveID";
         var fileID = "fileID";
         var httpPath = $"drives/{driveID}/items/{driveID}!{fileID}/content";
         var exceptionMessage = $"Error while uploading file [{httpPath}] with oneDrive service";
         var client = ClientBuilder.Create().With("", new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)).Build();
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.Upload($"{driveID}!{fileID}", new byte[] { }));

         Assert.NotNull(value);
         Assert.Equal(exceptionMessage, value.Message);
      }

      [Fact]
      internal async void Upload_WithValidData_MustResultAsSpected()
      {
         var driveID = "driveID";
         var fileID = "fileID";
         var httpPath = $"drives/{driveID}/items/{driveID}!{fileID}/content";
         var fileDTO = new DTOs.File { id = "fileID", name = "fileName", parentReference = new DTOs.DirectoryParent { path = "/parentFolder", id = "parentID" } };
         var fileVM = new FileVM { ID = "fileID" };
         var client = ClientBuilder.Create().With(httpPath, fileDTO).Build();
         var service = new OneDriveService(client);

         var value = await service.Upload($"{driveID}!{fileID}", new byte[] { });

         Assert.NotNull(value);
         Assert.Equal(fileVM.ID, value.ID);
      }

      [Fact]
      internal async void UploadNewFile_WithExceptionOnCall_MustThrowException()
      {
         var driveID = "driveID";
         var folderID = "folderID";
         var fileName = "fileName";
         var httpPath = $"drives/{driveID}/items/{driveID}!{folderID}:/{fileName}:/content";
         var exception = new Exception($"Error while uploading file [{httpPath}] with oneDrive service");
         var client = ClientBuilder.Create().With("", exception).Build();
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.Upload($"{driveID}!{folderID}", fileName, new byte[] { }));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

   }
}
