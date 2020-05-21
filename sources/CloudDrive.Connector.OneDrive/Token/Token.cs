using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public partial class Token : IToken
   {

      public Token(IClientApplicationBase client, params string[] scopes)
      {
         this.Scopes = scopes;
         this.Client = client;
      }

      readonly string[] Scopes;
      readonly IClientApplicationBase Client;

      public Task<bool> Connect() => throw new NotImplementedException();
      public Task<bool> IsConnected() => throw new NotImplementedException();
      public Task Disconnect() => throw new NotImplementedException();

   }
}
