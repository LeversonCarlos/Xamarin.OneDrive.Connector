using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public partial class Client : HttpClient, IClient
   {

      const string BaseURL = "https://graph.microsoft.com/v1.0/";
      readonly IToken Token;

      public Client(IToken token) : base(new ClientHandler(() => token))
      {
         if (token == null) throw new ArgumentException("The token argument for the http client must be set");
         this.Token = token;
         this.BaseAddress = new Uri(BaseURL);
      }

      public Task<bool> ConnectAsync() => this.Token.ConnectAsync();
      public Task<bool> CheckConnectionAsync() => this.Token.CheckConnectionAsync();
      public Task DisconnectAsync() => this.Token.DisconnectAsync();

   }
}
