using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public class StartupTests
   {

      [Fact]
      public void AddOneDriveConnector_Instance_MustNotBeNull()
      {
         var clientID = "clientID";
         var scopes = new string[] { "scopes" };
         var serviceCollection = new ServiceCollection();
         var serviceProvider = serviceCollection
            .AddOneDriveConnector(clientID, "clientSecret", "redirectUri", scopes)
            .BuildServiceProvider();

         var value = serviceProvider.GetService<OneDriveSettings>();

         Assert.NotNull(value);
         Assert.Equal(clientID, value.ClientID);
         Assert.Equal(scopes, value.Scopes);
      }

      [Fact]
      public void AddOneDriveConnector_Instance_WithoutIentity_MustThrowException()
      {
         var serviceCollection = new ServiceCollection();
         var serviceProvider = serviceCollection
            .AddOneDriveConnector("clientID", "clientSecret", "redirectUri", new string[] { "scopes" })
            .BuildServiceProvider();

         var value = Assert.Throws<MsalClientException>(() => serviceProvider.GetService<OneDriveService>());

         Assert.NotNull(value);
         Assert.Equal("Error: ClientId is not a Guid.", value.Message);
      }

      [Fact]
      public void ServiceInjected_WithDummyIdentity_MustNotBeNull()
      {
         var serviceCollection = new ServiceCollection();
         var serviceProvider = serviceCollection
            .AddSingleton(serviceProvider => IdentityBuilder.Create().Build())
            .AddSingleton<IOneDriveToken, OneDriveToken>()
            .AddSingleton<IOneDriveClient, OneDriveClient>()
            .AddSingleton<OneDriveService>()
            .BuildServiceProvider();

         var value = serviceProvider.GetService<OneDriveService>();

         Assert.NotNull(value);
      }

      [Fact]
      public void ServiceInjected_ForTheSecondTime_MustNotBeNull()
      {
         var serviceCollection = new ServiceCollection();
         var serviceProvider = serviceCollection
            .AddOneDriveConnector("clientID", "clientSecret", "redirectUri", new string[] { "scopes" })
            .AddSingleton(serviceProvider => IdentityBuilder.Create().Build())
            .AddSingleton<OneDriveService>()
            .BuildServiceProvider();

         var value = serviceProvider.GetService<OneDriveService>();

         Assert.NotNull(value);
      }

   }
}
