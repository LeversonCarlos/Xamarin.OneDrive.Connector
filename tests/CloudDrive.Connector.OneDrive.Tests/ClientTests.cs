using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDrive.Tests
{
   public class Client
   {

      [Fact]
      public void ConstructorArgumentsMustBeSet()
      {
         var creator = new Action(() => new OneDrive.Client(null));

         var expected = "The token argument for the http client must be set";
         var value = Assert.Throws<ArgumentException>(creator);

         Assert.Equal(expected, value.Message);
      }

      [Fact]
      public void BaseAddressMustBePointingToCorrectUri()
      {
         var client = new OneDrive.Client(TokenBuilder.Create().Builder());

         var expected = "https://graph.microsoft.com/v1.0/";
         var value = client.BaseAddress.AbsoluteUri;

         Assert.Equal(expected, value);
      }

      [Fact]
      public async void InitialConnectionStateMustBeOff()
      {
         var token = TokenBuilder
            .Create()
            .WithConnectionState(false)
            .Builder();
         var client = new OneDrive.Client(token);

         var expected = false;
         var value = await client.CheckConnectionAsync();

         Assert.Equal(expected, value);
      }

      [Fact]
      public async void ConnectionStateMustBeValidAfterConnect()
      {
         var token = TokenBuilder
            .Create()
            .WithConnectExecution(true)
            .Builder();
         var client = new OneDrive.Client(token);

         var expected = true;
         var value = await client.ConnectAsync();

         Assert.Equal(expected, value);
      }

   }
}
