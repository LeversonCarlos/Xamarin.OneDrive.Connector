using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public partial class Token : IToken
   {

      public Token(IClientApplicationBase identity, params string[] scopes)
      {
         if (identity == null) throw new ArgumentException("The identity argument for the OneDrive client must be set");
         this.Identity = identity;
         this.Scopes = scopes;
      }

      readonly string[] Scopes;
      readonly IClientApplicationBase Identity;

      // tenants { common, organizations, consumers }
      const string Authority = "https://login.microsoftonline.com/{tenant}";
      static Uri GetAuthorityUri() => new Uri(Authority.Replace("{tenant}", "common"));

      public Task<bool> Connect() => throw new NotImplementedException();
      public Task Disconnect() => throw new NotImplementedException();

   }
}
