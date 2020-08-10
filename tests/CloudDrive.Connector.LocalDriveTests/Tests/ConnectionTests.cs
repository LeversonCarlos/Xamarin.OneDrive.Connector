using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class ConnectionTests
   {

      [Fact]
      public async void Connection_Connect_MustReturnTrue()
      {
         var service = new LocalDriveConnection();

         var value = await service.ConnectAsync();

         Assert.True(value);
      }

      [Fact]
      public async void Service_Connect_MustReturnTrue()
      {
         var service = new LocalDriveService();

         var value = await service.ConnectAsync();

         Assert.True(value);
      }

      [Fact]
      public async void Connection_Disconnect_MustExists()
      {
         var service = new LocalDriveConnection();

         var expected = true;
         await service.DisconnectAsync();

         Assert.True(expected);
      }

      [Fact]
      public async void Service_Disconnect_MustExists()
      {
         var service = new LocalDriveService();

         var expected = true;
         await service.DisconnectAsync();

         Assert.True(expected);
      }

      [Fact]
      public async void Connection_CheckConnection_MustReturnTrue()
      {
         var service = new LocalDriveConnection();

         var value = await service.CheckConnectionAsync();

         Assert.True(value);
      }

      [Fact]
      public async void Service_CheckConnection_MustReturnTrue()
      {
         var service = new LocalDriveService();

         var value = await service.CheckConnectionAsync();

         Assert.True(value);
      }

   }
}
