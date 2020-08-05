using Moq;
using Moq.Language.Flow;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   internal class ClientBuilder
   {
      readonly Mock<HttpMessageHandler> Mock;
      public ClientBuilder() => Mock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
      public static ClientBuilder Create() => new ClientBuilder();

      ISetup<HttpMessageHandler, Task<HttpResponseMessage>> SendAsync(string requestUri) =>
         Mock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().EndsWith(requestUri)),
            ItExpr.IsAny<CancellationToken>());

      public ClientBuilder With(string requestUri, Exception ex)
      {
         SendAsync(requestUri).ThrowsAsync(ex);
         return this;
      }
      public ClientBuilder With(string requestUri, HttpResponseMessage value)
      {
         SendAsync(requestUri).ReturnsAsync(value);
         return this;
      }
      public ClientBuilder With<T>(string requestUri, T value)
      {
         SendAsync(requestUri).ReturnsAsync(GetResponseMessage(value));
         return this;
      }

      bool _ConnectionState = true;
      public ClientBuilder WithoutConnection()
      {
         _ConnectionState = false;
         return this;
      }

      HttpResponseMessage GetResponseMessage<T>(T value)
      {
         var jsonParameter = System.Text.Json.JsonSerializer.Serialize(value, new System.Text.Json.JsonSerializerOptions { });
         var responseContent = new StringContent(jsonParameter, System.Text.Encoding.UTF8, "application/json");
         var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = responseContent };
         return responseMessage;
      }

      public OneDriveClient Build() =>
         new OneDriveClient(TokenBuilder.Create().WithConnectionState(_ConnectionState).Build(), Mock.Object);
   }
}
