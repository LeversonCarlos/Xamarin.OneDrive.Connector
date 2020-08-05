using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      public async void GetProfile_WithException_MustThrowException()
      {
         var exception = new Exception("Some Dummy Exception");
         var client = ClientBuilder.Create().With("me?$select=id,displayName,userPrincipalName", exception).Build();
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.GetProfile());

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

   }
}
