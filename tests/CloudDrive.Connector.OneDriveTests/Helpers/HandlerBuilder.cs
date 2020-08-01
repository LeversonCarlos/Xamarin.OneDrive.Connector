using Moq;
using Moq.Language.Flow;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   internal class HandlerBuilder
   {
      readonly Mock<HttpMessageHandler> Mock;
      public HandlerBuilder() => Mock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
      public static HandlerBuilder Create() => new HandlerBuilder();

      ISetup<HttpMessageHandler, Task<HttpResponseMessage>> SendAsync() =>
         Mock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());

      public HandlerBuilder WithGetAsync(Exception ex)
      {
         SendAsync().ThrowsAsync(ex);
         return this;
      }
      public HandlerBuilder WithGetAsync<T>(T value)
      {
         SendAsync().ReturnsAsync(GetResponseMessage(value));
         return this;
      }

      HttpResponseMessage GetResponseMessage<T>(T value)
      {
         var jsonParameter = System.Text.Json.JsonSerializer.Serialize(value, new System.Text.Json.JsonSerializerOptions { });
         var responseContent = new StringContent(jsonParameter, System.Text.Encoding.UTF8, "application/json");
         var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = responseContent };
         return responseMessage;
      }

      public HttpMessageHandler Build() => Mock.Object;
   }
}
