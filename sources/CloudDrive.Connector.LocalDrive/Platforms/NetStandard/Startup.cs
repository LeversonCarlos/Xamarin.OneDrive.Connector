using Microsoft.Extensions.DependencyInjection;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class Startup
   {

      public static IServiceCollection AddLocalDriveConnector(this IServiceCollection serviceCollection)
      {
         return serviceCollection
            .AddScoped<LocalDriveService>();
      }

   }
}
