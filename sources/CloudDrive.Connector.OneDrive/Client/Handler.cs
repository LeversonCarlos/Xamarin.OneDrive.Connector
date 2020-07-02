using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal class OneDriveClientHandler : DelegatingHandler
   {

      readonly Func<IOneDriveToken> GetToken;
      IOneDriveToken Token { get { return this.GetToken(); } }

      public OneDriveClientHandler(Func<IOneDriveToken> getToken)
      {
         this.GetToken = getToken;
         this.InnerHandler = new HttpClientHandler();
      }

      protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {
         if (!await this.Token.CheckConnectionAsync())
         { return this.CreateMessage(HttpStatusCode.Unauthorized, "The token connect method has failed"); }

         request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", this.Token.GetToken());
         return await base.SendAsync(request, cancellationToken);
      }

      public HttpResponseMessage CreateMessage(HttpStatusCode statusCode) => this.CreateMessage(statusCode, string.Empty);
      public HttpResponseMessage CreateMessage(HttpStatusCode statusCode, string content)
      {
         var responseMessage = new HttpResponseMessage(statusCode);
         if (!string.IsNullOrEmpty(content))
         { responseMessage.Content = new StringContent(content); }
         return responseMessage;
      }

   }
}
