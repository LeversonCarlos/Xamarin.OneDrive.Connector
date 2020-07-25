using System.IO;
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

   }
}
