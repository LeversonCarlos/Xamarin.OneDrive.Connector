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

      public HandlerBuilder WithGetAsync(HttpResponseMessage responseMessage)
      {
         SendAsync().ReturnsAsync(responseMessage);
         return this;
      }
      public HandlerBuilder WithGetAsync(Exception ex)
      {
         SendAsync().ThrowsAsync(ex);
         return this;
      }

      public HttpMessageHandler Build() => Mock.Object;
   }
}
