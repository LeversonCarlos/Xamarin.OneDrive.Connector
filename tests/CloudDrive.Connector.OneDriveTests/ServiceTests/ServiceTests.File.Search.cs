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

         var value = await Assert.ThrowsAsync<ArgumentException>(async () => await service.SearchFiles(directory, new string[] { }, addFilesUntilLimit: null));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

   }
}
