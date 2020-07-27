using System.IO;
using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class SearchTests
   {

      [Fact]
      public async void SearchFiles_WithoutConnection_MustReturnNull()
      {
         var connection = ConnectionBuilder.Create().WithCheckConnectionValue(false).Build();
         var service = new LocalDriveService(connection);

         DirectoryVM directoryVM = null;
         var value = await service.SearchFiles(directoryVM, "*.*");

         Assert.Null(value);
      }

      [Fact]
      public async void SearchFiles_WithNullParameter_MustReturnNull()
      {
         var service = new LocalDriveService();

         DirectoryVM directoryVM = null;
         var value = await service.SearchFiles(directoryVM, "*.*");

         Assert.Null(value);
      }

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      [InlineData("#:/$%^")]
      [InlineData("#,.*%")]
      public async void SearchFiles_WithInvalidDirectory_MustReturnNull(string directoryID)
      {
         var service = new LocalDriveService();

         var directoryVM = new DirectoryVM { ID = directoryID };
         var value = await service.SearchFiles(directoryVM, "*.*");

         Assert.Null(value);
      }

      [Theory]
      [InlineData(0)]
      [InlineData(-1)]
      [InlineData(int.MinValue)]
      public async void SearchFiles_WithInvalidLimit_MustReturnNull(int limit)
      {
         var service = new LocalDriveService();

         var directoryVM = new DirectoryVM { ID = Helpers.FileSystem.CurrentDirectory };
         var value = await service.SearchFiles(directoryVM, "*.*", limit);

         Assert.Null(value);
      }

      [Theory]
      [InlineData(1)]
      [InlineData(10)]
      [InlineData(int.MaxValue)]
      public async void SearchFiles_WithCurrentDirectory_MustReturnSpectedChildren(int limit)
      {
         var service = new LocalDriveService();

         var currentDirectory = Helpers.FileSystem.CurrentDirectory;
         var expectedValue = Directory
            .EnumerateFiles(currentDirectory, "*.dll", SearchOption.AllDirectories)
            .Where(file => !string.IsNullOrEmpty(file))
            .Where(file => File.Exists(file))
            .OrderBy(file => file)
            .Take(limit)
            .Select(file => new FileInfo(file))
            .Where(fileInfo => fileInfo != null)
            .Select(fileInfo => new FileVM
            {
               ID = fileInfo.FullName,
               Name = fileInfo.Name,
               CreatedDateTime = fileInfo.CreationTime,
               SizeInBytes = fileInfo.Length,
               Path = fileInfo.DirectoryName,
               ParentID = fileInfo.DirectoryName
            })
            .ToArray();
         var directoryVM = new DirectoryVM { ID = currentDirectory };
         var value = await service.SearchFiles(directoryVM, "*.dll", limit);

         Assert.Equal(expectedValue?.Select(x => x.ID)?.ToArray(), value?.Select(x => x.ID)?.ToArray());
         Assert.InRange(value.Length, 0, limit);
      }

      [Fact]
      public async void GetDetails_WithoutConnection_MustReturnNull()
      {
         var connection = ConnectionBuilder.Create().WithCheckConnectionValue(false).Build();
         var service = new LocalDriveService(connection);

         var fileID = (string)null;
         var value = await service.GetDetails(fileID);

         Assert.Null(value);
      }

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      [InlineData("#:/$%^")]
      public async void GetDetails_WithInvalidParameter_MustReturnNull(string fileID)
      {
         var service = new LocalDriveService();

         var value = await service.GetDetails(fileID);

         Assert.Null(value);
      }

      [Fact]
      public async void GetDetails_WithValidFile_MustReturnSpectedData()
      {
         var service = new LocalDriveService();

         var sampleFile = Helpers.FileSystem.SampleFile;
         var expectedValue = (new string[] { sampleFile })
            .Select(file => new FileInfo(file))
            .Where(fileInfo => fileInfo != null)
            .Select(fileInfo => new FileVM
            {
               ID = fileInfo.FullName,
               Name = fileInfo.Name,
               CreatedDateTime = fileInfo.CreationTime,
               SizeInBytes = fileInfo.Length,
               Path = fileInfo.DirectoryName,
               ParentID = fileInfo.DirectoryName
            })
            .FirstOrDefault();
         var value = await service.GetDetails(sampleFile);

         Assert.Equal(expectedValue?.ID, value?.ID);
      }

   }
}
