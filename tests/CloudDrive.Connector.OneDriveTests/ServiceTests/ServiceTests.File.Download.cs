using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      [InlineData("fileID")]
      internal async void Download_WithInvalidArgument_MustThrowException(string fileID)
      {
         var exception = new Exception($"Error while downloading file [{fileID}] with oneDrive service");
         var client = ClientBuilder.Create().With("", exception).Build();
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.Download(fileID));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

   }
}
