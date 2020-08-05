using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      internal async void GetDetails_WithException_MustThrowException()
      {
         var fileID = "driveID!fileID";
         var exception = new Exception($"Error while loading details for file [{fileID}] with oneDrive service");
         var client = ClientBuilder.Create().With("", exception).Build();
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.GetDetails(fileID));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

      [Theory]
      [MemberData(nameof(GetDetails_WithValidArgument_MustResultSpectedValue_Data))]
      internal void GetDetails_WithValidArgument_MustResultSpectedValue(DTOs.File param)
      {
         var client = ClientBuilder.Create().Build();
         var service = new OneDriveService(client);

         var value = service.GetDetails(param);

         Assert.NotNull(value);
         Assert.Equal("id", value.ID);
      }
      public static IEnumerable<object[]> GetDetails_WithValidArgument_MustResultSpectedValue_Data() =>
         new[] {
            new object[] {
               new DTOs.File {
                  id="id", name="name", size=123, downloadUrl="http://test.com",
                  parentReference = new DTOs.DirectoryParent { path="/parent/folderName" },
                  createdDateTime="2020-08-05 20:22:15",
               }
            },
            new object[] {
               new DTOs.File {
                  id="id", name="name", size=123, downloadUrl="http://test.com",
                  parentReference = new DTOs.DirectoryParent { path="/parent/folderName" },
                  createdDateTime="",
               }
            },
            new object[] {
               new DTOs.File {
                  id="id", name="name", size=123, downloadUrl="http://test.com",
                  parentReference = new DTOs.DirectoryParent { path="/parent/folderName" },
                  createdDateTime="2-456-21 77:23:9875 J",
               }
            }
         };

   }
}
