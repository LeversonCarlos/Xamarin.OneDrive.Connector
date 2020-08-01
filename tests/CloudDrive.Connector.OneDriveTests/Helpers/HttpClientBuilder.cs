using Moq;
using System;
using System.Net.Http;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   internal class HttpClientBuilder
   {
      readonly Mock<HttpClient> Mock;
      public HttpClientBuilder() => this.Mock = new Mock<HttpClient>();
      public static HttpClientBuilder Create() => new HttpClientBuilder();

      public HttpClientBuilder WithGetAsync(string requestUri, HttpResponseMessage responseMessage)
      {
         this.Mock.Setup(m => m.GetAsync(requestUri)).ReturnsAsync(responseMessage);
         return this;
      }
      public HttpClientBuilder WithGetAsync(string requestUri, Exception ex)
      {
         this.Mock.Setup(m => m.GetAsync(requestUri)).ThrowsAsync(ex);
         return this;
      }

      public HttpClient Build() => this.Mock.Object;
   }
}
