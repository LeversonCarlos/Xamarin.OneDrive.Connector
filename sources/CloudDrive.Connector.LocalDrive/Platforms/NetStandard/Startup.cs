using Microsoft.Extensions.DependencyInjection;

namespace Xamarin.CloudDrive.Connector
{
   public static class LocalDriveExtention
   {

      public static IServiceCollection AddLocalDriveConnector(this IServiceCollection serviceCollection)
      {
         return serviceCollection
            .AddSingleton<LocalDriveService>();
      }

   }
}
