using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Client
   {

      [Fact]
      public async void GetAsync_WithException_MustThrowException()
      {
         var token = TokenBuilder.Create().Build();
         var requestUri = "someDummyUrl";
         var exception = new Exception("Some dummy exception");
         var httpClient = HandlerBuilder.Create().WithGetAsync(exception).Build();
         var client = new OneDriveClient(token, httpClient);

         var expected = exception;
         var value = await Assert.ThrowsAsync<Exception>(async () => await client.GetAsync(requestUri));

         Assert.NotNull(value);
         Assert.Equal(expected.Message, value.Message);
      }

   }
}
