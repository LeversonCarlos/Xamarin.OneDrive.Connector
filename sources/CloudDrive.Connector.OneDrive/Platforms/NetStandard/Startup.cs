using Microsoft.Extensions.DependencyInjection;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Startup
   {

      /*
      public static IServiceCollection AddOneDriveConnector(this IServiceCollection serviceCollection,
         string clientID, string redirectUri, string clientSecret, params string[] scopes)
      {
         return serviceCollection
            .AddScoped<OneDriveService>(serviceProvier =>
            {
               var identity = Token.CreateIdentity(clientID, redirectUri, clientSecret);
               var token = new Token(identity, scopes);
               var client = new Client(token);
               // return new OneDriveService(client);
            });
      }
      */

   }
}
