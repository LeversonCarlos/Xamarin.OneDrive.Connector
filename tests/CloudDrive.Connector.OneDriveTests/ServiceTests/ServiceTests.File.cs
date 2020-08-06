using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      internal async void GetFiles_WithInvalidArgument_MustThrowException()
      {
         var exception = new ArgumentException("The directory ID for the onedrive client is invalid");
         var client = ClientBuilder.Create().With("", exception).Build();
         var service = new OneDriveService(client);
         var directory = new DirectoryVM { };

         var value = await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetFiles(directory));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

      [Fact]
      internal async void GetFiles_WithValidArgument_MustResultAsSpected()
      {
         var firstData = new DTOs.FileSearch
         {
            value = new DTOs.File[] {
               new DTOs.File {
                  file = new DTOs.FileDetails{ },
                  id="fileID", name="fileName",
                  parentReference=new DTOs.DirectoryParent{ id="parentID", path="/parent/folderName" }
               }
            },
            nextLink = "$top=1000&$page=2"
         };
         var secondData = new DTOs.FileSearch
         {
            value = new DTOs.File[] {
               new DTOs.File { },
               new DTOs.File {
                  file = new DTOs.FileDetails{ },
                  id="file2ID", name="file2Name",
                  parentReference=new DTOs.DirectoryParent{ id="parentID", path ="/parent/folderName" }
               },
            },
            nextLink = "https://graph.microsoft.com/v1.0/"
         };
         var client = ClientBuilder
            .Create()
            .With("$top=1000", firstData)
            .With("$top=1000&$page=2", secondData)
            .Build();
         var service = new OneDriveService(client);
         var directory = new DirectoryVM { ID = "driveID!folderID" };

         var value = await service.GetFiles(directory);

         Assert.NotNull(value);
         Assert.Equal(2, value.Length);
         Assert.Equal("fileID", value[0].ID);
         Assert.Equal("fileName", value[0].Name);
         Assert.Equal("/parent/folderName", value[0].Path);
         Assert.Equal("parentID", value[0].ParentID);
      }

   }
}
