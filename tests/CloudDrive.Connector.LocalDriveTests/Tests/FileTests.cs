using System.IO;
using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class FileTests
   {

      [Fact]
      public async void GetFiles_WithoutConnection_MustReturnNull()
      {
         var connection = ConnectionBuilder.Create().WithCheckConnectionValue(false).Build();
         var service = new LocalDriveService(connection);

         DirectoryVM directoryVM = null;
         var value = await service.GetFiles(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void GetFiles_WithNullParameter_MustReturnNull()
      {
         var service = new LocalDriveService();

         DirectoryVM directoryVM = null;
         var value = await service.GetFiles(directoryVM);

         Assert.Null(value);
      }

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      [InlineData("#:/$%^")]
      public async void GetFiles_WithInvalidParameter_MustReturnNull(string directoryID)
      {
         var service = new LocalDriveService();

         var directoryVM = new DirectoryVM { ID = directoryID };
         var value = await service.GetFiles(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void GetFiles_WithCurrentDirectory_MustReturnSpectedChildren()
      {
         var service = new LocalDriveService();

         var currentDirectory = Directory.GetCurrentDirectory();
         var expectedValue = Directory
            .EnumerateFiles(currentDirectory, "*.*", SearchOption.TopDirectoryOnly)
            .Where(file => !string.IsNullOrEmpty(file))
            .Where(file => File.Exists(file))
            .OrderBy(file => file)
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
         var value = await service.GetFiles(directoryVM);

         Assert.Equal(expectedValue?.Select(x => x.ID)?.ToArray(), value?.Select(x => x.ID)?.ToArray());
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

   }
}
