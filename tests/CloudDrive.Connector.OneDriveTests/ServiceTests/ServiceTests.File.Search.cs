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

         var value = await Assert.ThrowsAsync<ArgumentException>(async () => await service.SearchFiles(directory, "", 1));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

      [Fact]
      internal async void SearchFiles_WithZeroLimit_MustResultAndEmptyArray()
      {
         var client = ClientBuilder.Create().Build();
         var service = new OneDriveService(client);
         var directory = new DirectoryVM { };

         var value = await service.SearchFiles(directory, "", 0);

         Assert.NotNull(value);
         Assert.Empty(value);
      }

      [Fact]
      internal async void SearchFiles_WithSingleFileAndNoChildFolder_MustSingleArray()
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
         Assert.Single(value);
         Assert.Equal("fileID", value[0].ID);
      }

      [Theory]
      [InlineData("*.txt", 2)]
      [InlineData("*.txt", 1)]
      [InlineData("*.txt", 3)]
      [InlineData("*.zip", 2)]
      internal async void SearchFiles_WithValidFilesAndFolder_MustResultAsSpected(string searchPattern, int searchLimit)
      {
         var fileData = new DTOs.FileSearch
         {
            value = new DTOs.File[] {
               new DTOs.File {
                  file = new DTOs.FileDetails{ },
                  id="file1ID", name="file1Name.txt",
                  parentReference=new DTOs.DirectoryParent{ id="parentID", path="/parent/folderName" }
               },
               new DTOs.File {
                  file = new DTOs.FileDetails{ },
                  id="file2ID", name="file2Name.zip",
                  parentReference=new DTOs.DirectoryParent{ id="parentID", path="/parent/folderName" }
               },
               new DTOs.File {
                  file = new DTOs.FileDetails{ },
                  id="file3ID", name="file3Name.txt",
                  parentReference=new DTOs.DirectoryParent{ id="parentID", path="/parent/folderName" }
               }
            }
         };
         var directoryData = new DTOs.DirectorySearch
         {
            value = new DTOs.Directory[] {
               new DTOs.Directory {
                  folder =new DTOs.DirectoryDetails{ },
                  id="driveID!folderID", name="folderName",
                  parentReference=new DTOs.DirectoryParent{ path="/rootName" }
               }
            }
         };
         var client = ClientBuilder.Create()
            .With("$select=id,name,createdDateTime,size,@microsoft.graph.downloadUrl,file,parentReference&$top=1000", fileData)
            .With("$select=id,name,folder,parentReference&$top=1000", directoryData)
            .Build();
         var service = new OneDriveService(client);
         var directory = new DirectoryVM { ID = "driveID!folderID" };

         var value = await service.SearchFiles(directory, searchPattern, searchLimit);

         Assert.NotNull(value);
         Assert.Equal(searchLimit, value.Length);
      }

      [Theory]
      [InlineData("*.txt", 2, 2)]
      [InlineData("*.txt", 5, 3)]
      internal async void SearchFiles_WithValidFilesAndLimits_MustResultAsSpected(string searchPattern, int searchLimit, int expectedSize)
      {
         var fileData = new DTOs.FileSearch
         {
            value = new DTOs.File[] {
               new DTOs.File {
                  file = new DTOs.FileDetails{ },
                  id="file1ID", name="file1Name.txt",
                  parentReference=new DTOs.DirectoryParent{ id="parentID", path="/parent/folderName" }
               },
               new DTOs.File {
                  file = new DTOs.FileDetails{ },
                  id="file2ID", name="file2Name.zip",
                  parentReference=new DTOs.DirectoryParent{ id="parentID", path="/parent/folderName" }
               },
               new DTOs.File {
                  file = new DTOs.FileDetails{ },
                  id="file3ID", name="file3Name.txt",
                  parentReference=new DTOs.DirectoryParent{ id="parentID", path="/parent/folderName" }
               },
               new DTOs.File {
                  file = new DTOs.FileDetails{ },
                  id="file4ID", name="file4Name.txt",
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

         var value = await service.SearchFiles(directory, searchPattern, searchLimit);

         Assert.NotNull(value);
         Assert.Equal(expectedSize, value.Length);
      }

   }
}
