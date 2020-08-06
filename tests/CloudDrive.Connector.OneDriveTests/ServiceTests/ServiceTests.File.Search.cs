using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      internal async void SearchFiles_WithInvalidArguments_MustThrowException()
      {
         var exception = new ArgumentException("The directory ID for the onedrive client is invalid");
         var client = ClientBuilder.Create().With("", exception).Build();
         var service = new OneDriveService(client);
         var directory = new DirectoryVM { };

         var value = await Assert.ThrowsAsync<ArgumentException>(async () => await service.SearchFiles(directory, "", 0));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

      [Fact]
      internal async void SearchFiles_WithValidArguments_MustResultAsSpected()
      {
         var fileData = new DTOs.FileSearch
         {
            value = new DTOs.File[] {
               new DTOs.File {
                  file = new DTOs.FileDetails{ },
                  id="fileID", name="fileName.txt",
                  parentReference=new DTOs.DirectoryParent{ id="parentID", path="/parent/folderName" }
               }
            }
         };
         var directoryData = new DTOs.DirectorySearch
         {
            value = new DTOs.Directory[] { }
         };
         var client = ClientBuilder.Create()
            .With("$select=id,name,createdDateTime,size,@microsoft.graph.downloadUrl,file,parentReference&$top=1000", fileData)
            .With("$select=id,name,folder,parentReference&$top=1000", directoryData)
            .Build();
         var service = new OneDriveService(client);
         var directory = new DirectoryVM { ID = "driveID!folderID" };

         var value = await service.SearchFiles(directory, "*.txt");

         Assert.NotNull(value);
         Assert.NotEmpty(value);
         Assert.Equal("fileID", value[0].ID);
      }

   }
}
