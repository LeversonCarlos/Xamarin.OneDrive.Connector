using Microsoft.Extensions.DependencyInjection;
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

   }
}
