using Microsoft.Extensions.DependencyInjection;

namespace Xamarin.CloudDrive.Connector
{

   partial class LocalDriveService
   {
      public LocalDriveService()
      {
         _Connection = new LocalDriveConnection();
         _Storage = new LocalDriveStorage();
      }
   }

   public static class LocalDriveExtention
   {

      public static IServiceCollection AddLocalDriveConnector(this IServiceCollection serviceCollection)
      {
         return serviceCollection
            .AddSingleton<LocalDriveService>();
      }

   }
}
