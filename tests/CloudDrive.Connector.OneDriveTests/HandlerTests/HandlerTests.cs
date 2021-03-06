using System.Net.Http;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public partial class Handler
   {

      [Fact]
      public async void CreateMessage_WithContent_MustResultAsSpected()
      {
         var token = TokenBuilder.Create().Build();
         var handler = new OneDriveClientHandler(token);

         var httpCode = System.Net.HttpStatusCode.BadRequest;
         var httpContent = "Dummy Content";
         var value = handler.CreateMessage(httpCode, httpContent);

         Assert.Equal(httpCode, value.StatusCode);
         Assert.Equal(httpContent, await value.Content.ReadAsStringAsync());
      }

      [Fact]
      public void CreateMessage_WithoutContent_MustResultAsSpected()
      {
         var token = TokenBuilder.Create().Build();
         var handler = new OneDriveClientHandler(token);

         var httpCode = System.Net.HttpStatusCode.BadRequest;
         var httpContent = "";
         var value = handler.CreateMessage(httpCode, httpContent);

         Assert.Equal(httpCode, value.StatusCode);
         Assert.Null(value.Content);
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
