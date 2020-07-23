using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class ConnectionTests
   {

      [Fact]
      public async void ConnectShouldBeTrue()
      {
         var service = ConnectionBuilder
            .Create()
            .WithConnectValue(true)
            .Build();

         var expected = true;
         var value = await service.ConnectAsync();

         Assert.Equal(expected, value);
      }

      [Fact]
      public async void CheckConnectionShouldBeTrue()
      {
         var service = ConnectionBuilder
            .Create()
            .WithCheckConnectionValue(true)
            .Build();

         var expected = true;
         var value = await service.CheckConnectionAsync();

         Assert.Equal(expected, value);
      }

   }
}
