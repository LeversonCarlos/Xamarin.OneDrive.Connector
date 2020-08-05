using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      internal async void GetDirectories_WithInvalidArgument_MustThrowException()
      {
         var exception = new ArgumentException("The directory ID for the onedrive client is invalid");
         var client = ClientBuilder.Create().With("", exception).Build();
         var service = new OneDriveService(client);
         var directory = new DirectoryVM { };

         var value = await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetDirectories(directory));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

      [Fact]
      internal async void GetDirectories_WithValidArgument_MustResultAsSpected()
      {
         var firstData = new DTOs.DirectorySearch
         {
            value = new DTOs.Directory[] {
               new DTOs.Directory {
                  folder =new DTOs.DirectoryDetails{ },
                  id="fileID", name="fileName",
                  parentReference=new DTOs.DirectoryParent{ path="/parent/folderName" }
               }
            },
            nextLink = "$top=1000&$page=2"
         };
         var secondData = new DTOs.DirectorySearch
         {
            value = new DTOs.Directory[] {
               new DTOs.Directory { },
               new DTOs.Directory {
                  folder =new DTOs.DirectoryDetails{ },
                  id="file2ID", name="file2Name"
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

         var value = await service.GetDirectories(directory);

         Assert.NotNull(value);
         Assert.Equal(2, value.Length);
         Assert.Equal("fileID", value[0].ID);
         Assert.Equal("fileName", value[0].Name);
         Assert.Equal("/parent/folderName/fileName", value[0].Path);
         Assert.Equal("driveID!folderID", value[0].ParentID);
      }

   }
}
