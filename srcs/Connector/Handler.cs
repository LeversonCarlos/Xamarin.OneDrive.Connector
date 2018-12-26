using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{
   internal class ConnectorHandler : DelegatingHandler
   {
      Token Token { get; set; }

      public ConnectorHandler(Configs configs)
      { 
         this.Token = new Token(configs);
         this.InnerHandler = new HttpClientHandler();
      }

      protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {
         if (!await this.Token.ConnectAsync()) { return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized); }
         request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", this.Token.CurrentToken);
         return await base.SendAsync(request, cancellationToken);
      }

   }
}