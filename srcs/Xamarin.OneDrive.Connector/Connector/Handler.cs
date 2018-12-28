using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{
   internal partial class ConnectorHandler : DelegatingHandler
   {
      Token Token { get; set; }
      internal const string InnerConnectionPath = "Xamarin-OneDrive-Connector";
      internal const string InnerConnectionConnect = "CONNECT";
      internal const string InnerConnectionDisconnect = "DISCONNECT";

      public ConnectorHandler(Configs configs) 
      {

         // VALIDATION
         if (configs == null) { throw new NullReferenceException("The configs parameter must be defined with the microsoft graph definitions"); }
         if (string.IsNullOrEmpty(configs.ClientID)) { throw new ArgumentNullException("Your microsoft graph client id must be informed"); }
         if (configs.Scopes == null || configs.Scopes.Length == 0) { throw new ArgumentNullException("Your microsoft graph required scopes must be informed"); }
         if (configs.Scopes.Count(x => !string.IsNullOrEmpty(x)) == 0) { throw new ArgumentNullException("Your microsoft graph required scopes must be informed"); }

         // INITIALIZATION
         Dependency.Current.Initialize(configs);
         this.Token = new Token(configs);
         this.InnerHandler = new HttpClientHandler();
      }

      protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {

         var InnerConnectionResult = await this.InnerConnectionHandlerAsync(request, cancellationToken);
         if (InnerConnectionResult.StatusCode != HttpStatusCode.SeeOther) { return InnerConnectionResult;}

         if (!await this.Token.ConnectAsync()) { return this.CreateMessage(HttpStatusCode.Unauthorized, "The token connect method has failed"); }

         request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", this.Token.CurrentToken);
         return await base.SendAsync(request, cancellationToken);

      }

      private async Task<HttpResponseMessage> InnerConnectionHandlerAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {
         try
         {
            if (!request.RequestUri.AbsolutePath.EndsWith(InnerConnectionPath)) { return this.CreateMessage(HttpStatusCode.SeeOther); }
            if (request.Method != HttpMethod.Post || request.Content == null) { return this.CreateMessage(HttpStatusCode.BadRequest, "Method must be POST and content must be CONNECT or DISCONNECT"); }

            var command = await request.Content.ReadAsStringAsync();
            if (command == InnerConnectionConnect)
            {
               var result = await this.Token.ConnectAsync();
               if (result) { return this.CreateMessage(HttpStatusCode.OK); }
               else { return this.CreateMessage(HttpStatusCode.InternalServerError, "The token connect method has failed"); }
            }
            else if (command == InnerConnectionDisconnect)
            {
               await this.Token.DisconnectAsync();
               return this.CreateMessage(HttpStatusCode.OK);
            }
            else
            { return this.CreateMessage(HttpStatusCode.BadRequest, "Method must be POST and content must be CONNECT or DISCONNECT"); }

         }
         catch (Exception ex) { return this.CreateMessage(HttpStatusCode.InternalServerError, ex.ToString()); }
      }

   }
}