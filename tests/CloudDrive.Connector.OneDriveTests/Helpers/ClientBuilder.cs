using Moq;
using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   internal class ClientBuilder
   {

      readonly Mock<OneDriveClient> Mock;
      public ClientBuilder() => this.Mock = new Mock<OneDriveClient>();
      public static ClientBuilder Create() => new ClientBuilder();

      public ClientBuilder With(Expression<Func<OneDriveClient, Task<HttpResponseMessage>>> expression, Exception ex)
      {
         this.Mock.Setup(expression).ThrowsAsync(ex);
         return this;
      }
      public ClientBuilder With<R>(Expression<Func<OneDriveClient, Task<HttpResponseMessage>>> expression, R value)
      {
         this.Mock.Setup(expression).ReturnsAsync(GetResponseMessage(value));
         return this;
      }

      HttpResponseMessage GetResponseMessage<T>(T value)
      {
         var jsonParameter = System.Text.Json.JsonSerializer.Serialize(value, new System.Text.Json.JsonSerializerOptions { });
         var responseContent = new StringContent(jsonParameter, System.Text.Encoding.UTF8, "application/json");
         var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = responseContent };
         return responseMessage;
      }

      public OneDriveClient Build() => this.Mock.Object;
   }
}
