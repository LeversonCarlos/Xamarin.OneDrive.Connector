using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{
   internal class ConnectorHandler : DelegatingHandler
   {
      Token Token { get; set; }
      internal const string InnerConnectionPath = "Xamarin-OneDrive-Connector";
      internal const string InnerConnectionConnect = "CONNECT";
      internal const string InnerConnectionDisconnect = "DISCONNECT";

      public ConnectorHandler(Configs configs)
      { 
         this.Token = new Token(configs);
         this.InnerHandler = new HttpClientHandler();
      }

      protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {

         var InnerConnectionResult = await this.InnerConnectionHandlerAsync(request, cancellationToken);
         if (InnerConnectionResult != HttpStatusCode.SeeOther) { return new HttpResponseMessage(InnerConnectionResult);}

         if (!await this.Token.ConnectAsync()) { return new HttpResponseMessage(HttpStatusCode.Unauthorized); }

         request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", this.Token.CurrentToken);
         return await base.SendAsync(request, cancellationToken);

      }

      private async Task<HttpStatusCode> InnerConnectionHandlerAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {
         try
         {
            if (!request.RequestUri.AbsolutePath.EndsWith(InnerConnectionPath)) { return HttpStatusCode.SeeOther; }
            if (request.Method != HttpMethod.Post || request.Content == null) { return HttpStatusCode.BadRequest; }

            var command = await request.Content.ReadAsStringAsync();
            if (command == InnerConnectionConnect)
            {
               var result = await this.Token.ConnectAsync();
               if (result) { return HttpStatusCode.OK; }
               else { return HttpStatusCode.InternalServerError; }
            }
            else if (command == InnerConnectionDisconnect)
            {
               await this.Token.DisconnectAsync();
               return HttpStatusCode.OK;
            }
            else
            { return HttpStatusCode.BadRequest; }

         }
         catch (Exception) { return HttpStatusCode.InternalServerError; }
      }

   }
}