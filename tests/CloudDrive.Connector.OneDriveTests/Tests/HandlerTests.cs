using System;
using System.Net.Http;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public partial class Handler
   {

      [Fact]
      public void Constructor_WithNullArguments_MustThrowException()
      {
         var creator = new Action(() => new OneDriveClientHandler(null));

         var expected = "The token argument for the http client must be set";
         var value = Assert.Throws<ArgumentException>(creator);

         Assert.Equal(expected, value.Message);
      }

      [Fact]
      public async void SendAsync_WithoutTokenConnection_MustResultUnauthorized()
      {
         var token = TokenBuilder.Create().Build();
         var handler = new OneDriveClientHandler(token);

#pragma warning disable CS0618 // Type or member is obsolete
         var value = await handler.InternalSendAsync(null, System.Threading.CancellationToken.None);
#pragma warning restore CS0618 // Type or member is obsolete

         Assert.Equal(System.Net.HttpStatusCode.Unauthorized, value.StatusCode);
         Assert.Equal("The token connect method has failed", await value.Content.ReadAsStringAsync());
      }

      [Fact]
      public async void SendAsync_WithTokenConnection_MustResultSpected()
      {
         var token = TokenBuilder.Create().WithConnectionState(true).Build();
         var handler = new OneDriveClientHandler(token);

#pragma warning disable CS0618 // Type or member is obsolete
         var request = new HttpRequestMessage(HttpMethod.Get, "http://www.google.com");
         var value = await handler.InternalSendAsync(request, new System.Threading.CancellationToken());
#pragma warning restore CS0618 // Type or member is obsolete

         Assert.Equal(System.Net.HttpStatusCode.OK, value.StatusCode);
         Assert.StartsWith("<!doctype html>", await value.Content.ReadAsStringAsync());
      }


   }
}
