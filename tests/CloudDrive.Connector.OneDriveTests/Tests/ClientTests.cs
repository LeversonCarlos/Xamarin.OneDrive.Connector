using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public class Client
   {

      [Fact]
      public void Constructor_WithNullArguments_MustThrowException()
      {
         var creator = new Action(() => new OneDriveClient(token: null));

         var expected = "The token argument for the http client must be set";
         var value = Assert.Throws<ArgumentException>(creator);

         Assert.Equal(expected, value.Message);
      }

      [Fact]
      public void Constructor_BaseAddress_MustBeAsSpected()
      {
         var client = new OneDriveClient(TokenBuilder.Create().Builder());

         var expected = "https://graph.microsoft.com/v1.0/";
         var value = client._HttpClient.BaseAddress.AbsoluteUri;

         Assert.Equal(expected, value);
      }

      [Fact]
      public async void CheckConnectionAsync_InitialConnectionState_MustBeResultFalse()
      {
         var token = TokenBuilder
            .Create()
            .WithConnectionState(false)
            .Builder();
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
            .Builder();
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
            .Builder();
         var client = new OneDriveClient(token);

         await client.DisconnectAsync();
         var expected = false;
         var value = await client.CheckConnectionAsync();

         Assert.Equal(expected, value);
      }

   }
}
