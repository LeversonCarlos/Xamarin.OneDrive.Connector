using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class StartupTests
   {

      [Fact]
      public void AddLocalDriveConnector_RegisteredInstance_MustNotBeNull()
      {
         var serviceCollection = new ServiceCollection();

         var serviceProvider = serviceCollection
            .AddLocalDriveConnector()
            .BuildServiceProvider();

         var value = serviceProvider.GetService<LocalDriveService>();

         Assert.NotNull(value);
      }

   }
}
