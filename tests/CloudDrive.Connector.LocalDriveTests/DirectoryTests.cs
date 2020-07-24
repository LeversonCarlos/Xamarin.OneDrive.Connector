using System.IO;
using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class DirectoryTests
   {

      [Fact]
      public void ValidDirectoryShouldHasValidName()
      {
         var service = new LocalDriveService();

         var currentDirectory = Directory.GetCurrentDirectory();
         var directoryName = Path.GetFileName(currentDirectory);
         var value = service.GetDirectoryInfo(currentDirectory);

         Assert.Equal(directoryName, value.Name);
      }

      [Fact]
      public void InvalidDirectoryShouldReturnNull()
      {
         var service = new LocalDriveService();

         var currentDirectory = Directory.GetCurrentDirectory()
            .Replace(Path.DirectorySeparatorChar.ToString(), ":,^");
         var value = service.GetDirectoryInfo(currentDirectory);

         Assert.Null(value);
      }

      [Fact]
      public async void ChlidDirectoriesMustBeAsSpected()
      {
         var service = new LocalDriveService();

         var currentDirectory = Directory.GetCurrentDirectory();
         var expectedValue = Directory
            .EnumerateDirectories(currentDirectory)
            .Where(x => !string.IsNullOrEmpty(x))
            .OrderBy(x => x)
            .ToArray();
         var value = await service.GetDirectoryList(currentDirectory);

         Assert.Equal(expectedValue, value);
      }

      [Fact]
      public async void WithoutConnectionMustReturnNull()
      {
         var connection = ConnectionBuilder.Create().WithCheckConnectionValue(false).Build();
         var service = new LocalDriveService(connection);

         DirectoryVM directoryVM = null;
         var value = await service.GetDirectories(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void NullDirectoryMustReturnNull()
      {
         var service = new LocalDriveService();

         DirectoryVM directoryVM = null;
         var value = await service.GetDirectories(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void EmptyDirectoryIDMustReturnNull()
      {
         var service = new LocalDriveService();

         DirectoryVM directoryVM = new DirectoryVM { };
         var value = await service.GetDirectories(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void InvalidDirectoryIDMustReturnNull()
      {
         var service = new LocalDriveService();

         var invalidDirectoryPath = Directory.GetCurrentDirectory()
            .Replace(Path.DirectorySeparatorChar.ToString(), ":,^");
         DirectoryVM directoryVM = new DirectoryVM { ID = invalidDirectoryPath };
         var value = await service.GetDirectories(directoryVM);

         Assert.Null(value);
      }

   }
}
