using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      public async void GetProfile_WithException_MustThrowException()
      {
         var token = TokenBuilder.Create().Build();
         var exception = new Exception("");
         var httpClient = HandlerBuilder.Create().WithException(exception).Build();
         var client = new OneDriveClient(token, httpClient);
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async ()=> await service.GetProfile());

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

   }
}
