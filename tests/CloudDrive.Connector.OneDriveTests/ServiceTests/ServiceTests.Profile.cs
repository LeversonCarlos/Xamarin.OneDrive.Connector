using System;
using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      public async void GetProfile_WithoutConnection_MustResultNull()
      {
         var client = ClientBuilder.Create().WithoutConnection().Build();
         var service = new OneDriveService(client);

         var value = await service.GetProfile();

         Assert.Null(value);
      }

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

      [Fact]
      public async void GetProfile_WithSpecificData_MustResultSpectedValue()
      {
         var param = new DTOs.Profile { id = "id", displayName = "displayName", userPrincipalName = "userPrincipalName" };
         var client = ClientBuilder.Create().With("me?$select=id,displayName,userPrincipalName", param).Build();
         var service = new OneDriveService(client);

         var value = await service.GetProfile();

         Assert.NotNull(value);
         Assert.Equal(value.ID, param.id);
         Assert.Equal(value.Description, param.displayName);
         Assert.Equal(value.KeyValues.Where(k => k.Key == "EMail").Select(v => v.Value).FirstOrDefault(), param.userPrincipalName);
      }

   }
}
