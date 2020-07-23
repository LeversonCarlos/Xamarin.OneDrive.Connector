using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class DirectoryTests
   {

      [Fact]
      public void ValidDirectoryShouldHasValidName()
      {
         var service = ServiceBuilder.Create().Build();

         var currentDirectory = System.IO.Directory.GetCurrentDirectory();
         var directoryName = System.IO.Path.GetFileName(currentDirectory);
         var value = service.GetDirectoryInfo(currentDirectory);

         Assert.Equal(directoryName, value.Name);
      }

      [Fact]
      public void InvalidDirectoryShouldReturnNull()
      {
         var service = ServiceBuilder.Create().Build();

         var currentDirectory = System.IO.Directory.GetCurrentDirectory()
            .Replace(System.IO.Path.DirectorySeparatorChar.ToString(), ":,^");
         var value = service.GetDirectoryInfo(currentDirectory);

         Assert.Null(value);
      }

   }
}
