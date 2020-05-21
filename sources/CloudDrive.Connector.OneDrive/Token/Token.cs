using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public partial class Token : IToken
   {

      public Token(IClientApplicationBase client, params string[] scopes)
      {
         if (client == null) throw new ArgumentException("The client application argument for the OneDrive client must be set");
         this.Client = client;
         this.Scopes = scopes;
      }

      readonly string[] Scopes;
      readonly IClientApplicationBase Client;

      // tenants { common, organizations, consumers }
      const string Authority = "https://login.microsoftonline.com/{tenant}";
      static Uri GetAuthorityUri() => new Uri(Authority.Replace("{tenant}", "common"));

      public Task<bool> Connect() => throw new NotImplementedException();
      public Task Disconnect() => throw new NotImplementedException();

   }
}
