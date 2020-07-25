using System.IO;
using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class FileTests
   {

      [Fact]
      public async void Service_WithoutConnection_MustReturnNull()
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

      [Fact]
      public async void GetFiles_WithEmptyID_MustReturnNull()
      {
         var service = new LocalDriveService();

         DirectoryVM directoryVM = new DirectoryVM { };
         var value = await service.GetFiles(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void GetFiles_WithInvalidID_MustReturnNull()
      {
         var service = new LocalDriveService();

         var invalidDirectoryPath = Directory.GetCurrentDirectory()
            .Replace(Path.DirectorySeparatorChar.ToString(), ":,^");
         DirectoryVM directoryVM = new DirectoryVM { ID = invalidDirectoryPath };
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

         Assert.Equal(expectedValue?.Length, value?.Length);
      }

   }
}
