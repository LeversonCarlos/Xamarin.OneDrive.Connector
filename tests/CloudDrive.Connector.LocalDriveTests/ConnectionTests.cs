using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class ConnectionTests
   {

      [Fact]
      public async void ConnectShouldBeTrue()
      {
         var service = new LocalDriveConnection();

         var expected = true;
         var value = await service.ConnectAsync();

         Assert.Equal(expected, value);
      }

      [Fact]
      public async void CheckConnectionShouldBeTrue()
      {
         var service = new LocalDriveConnection();

         var expected = true;
         var value = await service.CheckConnectionAsync();

         Assert.Equal(expected, value);
      }

   }
}
