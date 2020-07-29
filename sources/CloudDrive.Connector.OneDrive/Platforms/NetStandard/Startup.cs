using Microsoft.Extensions.DependencyInjection;

namespace Xamarin.CloudDrive.Connector
{
   public static class OneDriveExtention
   {

      public static IServiceCollection AddOneDriveConnector(this IServiceCollection serviceCollection,
         string clientID, string clientSecret, string redirectUri, params string[] scopes) =>
         serviceCollection
            .AddSingleton<OneDriveSettings>(serviceProvider => new OneDriveSettings(clientID, clientSecret, redirectUri, scopes))
            .AddSingleton<OneDriveToken>()
            .AddSingleton<OneDriveClient>()
            .AddSingleton<OneDriveService>();

   }
}
