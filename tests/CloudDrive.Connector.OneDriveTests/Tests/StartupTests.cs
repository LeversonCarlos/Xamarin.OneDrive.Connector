using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public class StartupTests
   {

      [Fact]
      public void AddOneDriveConnector_RegisteredInstance_MustNotBeNull()
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

   }
}
