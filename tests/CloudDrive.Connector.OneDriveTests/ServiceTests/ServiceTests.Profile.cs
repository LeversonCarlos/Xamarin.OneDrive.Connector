using System;
using System.Linq;
using System.Net.Http;
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
         Assert.Equal(param.id, value.ID);
         Assert.Equal(param.displayName, value.Description);
         Assert.Equal(param.userPrincipalName, value.KeyValues.Where(k => k.Key == "EMail").Select(v => v.Value).FirstOrDefault());
      }

      [Fact]
      public async void GetProfilePicture_WithoutConnection_MustResultNull()
      {
         var client = ClientBuilder.Create().WithoutConnection().Build();
         var service = new OneDriveService(client);

         var value = await service.GetProfilePicture();

         Assert.Null(value);
      }

      [Fact]
      public async void GetProfilePicture_WithException_MustThrowException()
      {
         var exception = new Exception("Some Dummy Exception");
         var client = ClientBuilder.Create().With("me/photo/$value", exception).Build();
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.GetProfilePicture());

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

      [Fact]
      public async void GetProfilePicture_WithSpecificData_MustResultSpectedValue()
      {
         var param = System.Text.Encoding.UTF8.GetBytes("Some Dummy Message");
         var paramContent = new ByteArrayContent(param, 0, param.Length);
         var paramMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = paramContent };
         var client = ClientBuilder.Create().With("me/photo/$value", paramMessage).Build();
         var service = new OneDriveService(client);

         var value = await service.GetProfilePicture();

         Assert.NotNull(value);
         Assert.Equal(param, value);
      }

   }
}
