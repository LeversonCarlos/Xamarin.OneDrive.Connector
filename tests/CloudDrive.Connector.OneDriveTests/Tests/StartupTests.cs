using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public class StartupTests
   {

      [Fact]
      public void AddOneDriveConnector_RegisteredInstance_MustNotBeNull()
      {
         var serviceCollection = new ServiceCollection();
         var serviceProvider = serviceCollection
            .AddOneDriveConnector("clientID", "clientSecret", "redirectUri", new string[] { "scopes" })
            .BuildServiceProvider();

         var value = serviceProvider.GetService<OneDriveSettings>();

         Assert.NotNull(value);
      }

   }
}
