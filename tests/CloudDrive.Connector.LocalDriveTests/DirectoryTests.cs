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
         var service = ServiceBuilder.Create().Build();

         var currentDirectory = Directory.GetCurrentDirectory();
         var directoryName = Path.GetFileName(currentDirectory);
         var value = service.GetDirectoryInfo(currentDirectory);

         Assert.Equal(directoryName, value.Name);
      }

      [Fact]
      public void InvalidDirectoryShouldReturnNull()
      {
         var service = ServiceBuilder.Create().Build();

         var currentDirectory = Directory.GetCurrentDirectory()
            .Replace(Path.DirectorySeparatorChar.ToString(), ":,^");
         var value = service.GetDirectoryInfo(currentDirectory);

         Assert.Null(value);
      }

      [Fact]
      public async void ChlidDirectoriesMustBeAsSpected()
      {
         var service = ServiceBuilder.Create().Build();

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
      public async void NullDirectoryMustReturnNull()
      {
         var service = ServiceBuilder.Create().Build();

         DirectoryVM directoryVM = null;
         var value = await service.GetDirectories(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void EmptyDirectoryIDMustReturnNull()
      {
         var service = ServiceBuilder.Create().Build();

         DirectoryVM directoryVM = new DirectoryVM { };
         var value = await service.GetDirectories(directoryVM);

         Assert.Null(value);
      }

      [Fact]
      public async void InvalidDirectoryIDMustReturnNull()
      {
         var service = ServiceBuilder.Create().Build();

         var invalidDirectoryPath = Directory.GetCurrentDirectory()
            .Replace(Path.DirectorySeparatorChar.ToString(), ":,^");
         DirectoryVM directoryVM = new DirectoryVM { ID = invalidDirectoryPath };
         var value = await service.GetDirectories(directoryVM);

         Assert.Null(value);
      }

   }
}
