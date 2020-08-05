using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Client
   {

      [Fact]
      public async void CheckConnectionAsync_InitialConnectionState_MustBeResultFalse()
      {
         var token = TokenBuilder
            .Create()
            .WithConnectionState(false)
            .Build();
         var client = new OneDriveClient(token);

         var expected = false;
         var value = await client.CheckConnectionAsync();

         Assert.Equal(expected, value);
      }

      [Fact]
      public async void Connection_StateAfterConnect_MustResultTrue()
      {
         var token = TokenBuilder
            .Create()
            .WithConnectExecution(true)
            .Build();
         var client = new OneDriveClient(token);

         var expected = true;
         var value = await client.ConnectAsync();

         Assert.Equal(expected, value);
      }

      [Fact]
      public async void Connection_StateAfterDisconnect_MustResultFalse()
      {
         var token = TokenBuilder
            .Create()
            .WithConnectExecution(false)
            .Build();
         var client = new OneDriveClient(token);

         await client.DisconnectAsync();
         var expected = false;
         var value = await client.CheckConnectionAsync();

         Assert.Equal(expected, value);
      }

   }
}
