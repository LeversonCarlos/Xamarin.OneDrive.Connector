using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public class IdentityTests
   {

      [Fact]
      public void Constructor_WithoutSettings_MustThrowException()
      {
         var serviceCollection = new ServiceCollection();
         var serviceProvider = serviceCollection
            .AddSingleton<OneDriveIdentity>()
            .BuildServiceProvider();

         var value = Assert.Throws<ArgumentException>(() => serviceProvider.GetService<OneDriveIdentity>());

         Assert.NotNull(value);
         Assert.Equal("The settings object for the onedrive client wasnt found on dependency service", value.Message);
      }

      [Fact]
      public void Constructor_WithInvalidSettingsData_MustThrowException()
      {
         var clientID = "clientID";
         var scopes = new string[] { "scopes" };
         var serviceCollection = new ServiceCollection();
         var serviceProvider = serviceCollection
            .AddOneDriveConnector(clientID, "clientSecret", "redirectUri", scopes)
            .BuildServiceProvider();

         var value = Assert.Throws<MsalClientException>(() => serviceProvider.GetService<IOneDriveIdentity>());

         Assert.NotNull(value);
         Assert.Equal("Error: ClientId is not a Guid.", value.Message);
      }

   }
}
