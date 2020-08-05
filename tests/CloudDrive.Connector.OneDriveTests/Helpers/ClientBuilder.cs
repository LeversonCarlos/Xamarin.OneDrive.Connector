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

      ISetup<HttpMessageHandler, Task<HttpResponseMessage>> SendAsync(HttpRequestMessage requestMessage) =>
         Mock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.Is<HttpRequestMessage>(req => req.Method == requestMessage.Method && req.RequestUri == req.RequestUri),
            ItExpr.IsAny<CancellationToken>());

      public ClientBuilder With(HttpRequestMessage requestMessage, Exception ex)
      {
         SendAsync(requestMessage).ThrowsAsync(ex);
         return this;
      }
      public ClientBuilder With<T>(HttpRequestMessage requestMessage, T value)
      {
         SendAsync(requestMessage).ReturnsAsync(GetResponseMessage(value));
         return this;
      }

      internal static HttpRequestMessage GetRequestMessage(string requestUri) =>
         new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/" + requestUri);
      internal static HttpRequestMessage GetRequestMessage<T>(string requestUri, T value, HttpMethod method)
      {
         var jsonContent = System.Text.Json.JsonSerializer.Serialize(value, new System.Text.Json.JsonSerializerOptions { });
         var stringContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
         var message = new HttpRequestMessage(method, requestUri) { Content = stringContent };
         return message;
      }

      HttpResponseMessage GetResponseMessage<T>(T value)
      {
         var jsonParameter = System.Text.Json.JsonSerializer.Serialize(value, new System.Text.Json.JsonSerializerOptions { });
         var responseContent = new StringContent(jsonParameter, System.Text.Encoding.UTF8, "application/json");
         var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = responseContent };
         return responseMessage;
      }

      public OneDriveClient Build() =>
         new OneDriveClient(TokenBuilder.Create().Build(), Mock.Object);
   }
}
