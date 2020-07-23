using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class ConnectionTests
   {

      [Fact]
      public async void ConnectShouldBeTrue()
      {
         var service = new LocalDriveConnection();

         var value = await service.ConnectAsync();

         Assert.True(value);
      }

      [Fact]
      public async void DisconnectShouldBeTrue()
      {
         var service = new LocalDriveConnection();

         var expected = true;
         await service.DisconnectAsync();

         Assert.True(expected);
      }

      [Fact]
      public async void CheckConnectionShouldBeTrue()
      {
         var service = new LocalDriveConnection();

         var value = await service.CheckConnectionAsync();

         Assert.True(value);
      }

   }
}
