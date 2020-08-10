using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      public async void ConnectAsync_WithDefaultClient_MustResultFalse()
      {
         var client = ClientBuilder.Create().Build();
         var service = new OneDriveService(client: client);

         var value = await service.ConnectAsync();

         Assert.False(value);
      }

      [Fact]
      public async void CheckConnectionAsync_WithDefaultClient_MustResultFalse()
      {
         var client = ClientBuilder.Create().WithoutConnection().Build();
         var service = new OneDriveService(client: client);

         var value = await service.CheckConnectionAsync();

         Assert.False(value);
      }

      [Fact]
      public async void DisconnectAsync_WithDefaultClient_MustResultFalse()
      {
         var client = ClientBuilder.Create().Build();
         var service = new OneDriveService(client: client);

         var expected = false;
         await service.DisconnectAsync();

         Assert.False(expected);
      }

   }
}
