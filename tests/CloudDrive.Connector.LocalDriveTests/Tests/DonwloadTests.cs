using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class DownloadTests
   {

      [Fact]
      public async void Download_WithoutConnection_MustReturnNull()
      {
         var connection = ConnectionBuilder.Create().WithCheckConnectionValue(false).Build();
         var service = new LocalDriveService(connection);

         var fileID = (string)null;
         var value = await service.Download(fileID);

         Assert.Null(value);
      }

   }
}
