using Microsoft.Extensions.DependencyInjection;

namespace Xamarin.CloudDrive.Connector
{
   public static class OneDriveExtention
   {

      public static IServiceCollection AddOneDriveConnector(this IServiceCollection serviceCollection,
         string clientID, string redirectUri, string clientSecret, params string[] scopes)
      {
         return serviceCollection
            .AddScoped<OneDriveService>(serviceProvier =>
            {
               var identity = OneDriveToken.CreateIdentity(clientID, redirectUri, clientSecret);
               var token = new OneDriveToken(identity, scopes);
               var client = new OneDriveClient(token);
               return new OneDriveService(client);
            });
      }

   }
}
