using System.IO;
using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class DirectoryTests
   {

      [Fact]
      public async void Service_WithoutConnection_MustReturnNull()
      {
         var connection = ConnectionBuilder.Create().WithCheckConnectionValue(false).Build();
         var service = new LocalDriveService(connection);

         DirectoryVM directoryVM = null;
         var value = await service.GetDirectories(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void GetDirectories_WithNullParameter_MustReturnNull()
      {
         var service = new LocalDriveService();

         DirectoryVM directoryVM = null;
         var value = await service.GetDirectories(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void GetDirectories_WithEmptyID_MustReturnNull()
      {
         var service = new LocalDriveService();

         DirectoryVM directoryVM = new DirectoryVM { };
         var value = await service.GetDirectories(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void GetDirectories_WithInvalidID_MustReturnNull()
      {
         var service = new LocalDriveService();

         var invalidDirectoryPath = Directory.GetCurrentDirectory()
            .Replace(Path.DirectorySeparatorChar.ToString(), ":,^");
         DirectoryVM directoryVM = new DirectoryVM { ID = invalidDirectoryPath };
         var value = await service.GetDirectories(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void GetDirectories_WithCurrentDirectory_MustReturnSpectedChildren()
      {
         var service = new LocalDriveService();

         var currentDirectory = Directory.GetCurrentDirectory();
         var expectedValue = Directory
            .EnumerateDirectories(currentDirectory)
            .Where(x => !string.IsNullOrEmpty(x))
            .OrderBy(dir => dir)
            .Select(dir => new DirectoryInfo(dir))
            .Where(dirInfo => dirInfo != null)
            .Select(dirInfo => new DirectoryVM
            {
               ID = dirInfo.FullName,
               Name = dirInfo.Name,
               Path = dirInfo.FullName,
               ParentID = currentDirectory
            })
            .ToArray();
         var directoryVM = new DirectoryVM { ID = currentDirectory };
         var value = await service.GetDirectories(directoryVM);

         Assert.Equal(expectedValue?.Length, value?.Length);
      }

   }
}
