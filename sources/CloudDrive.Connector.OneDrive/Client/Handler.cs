using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal partial class OneDriveClientHandler : DelegatingHandler
   {

      readonly IOneDriveToken Token;

      public OneDriveClientHandler(IOneDriveToken token)
      {
         Token = token;
         InnerHandler = new HttpClientHandler();
      }

      protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {
         if (!await Token.CheckConnectionAsync())
            return CreateMessage(HttpStatusCode.Unauthorized, "The token connect method has failed");

         request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Token.GetToken());

         var responseMessage = await base.SendAsync(request, cancellationToken);
         return responseMessage;
      }

      internal HttpResponseMessage CreateMessage(HttpStatusCode statusCode, string content)
      {
         var responseMessage = new HttpResponseMessage(statusCode);
         if (!string.IsNullOrEmpty(content))
            responseMessage.Content = new StringContent(content);
         return responseMessage;
      }

   }
}
