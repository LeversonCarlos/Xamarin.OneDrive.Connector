using System.Net;
using System.Net.Http;

namespace Xamarin.OneDrive
{
   partial class ConnectorHandler
   {

      public HttpResponseMessage CreateMessage(HttpStatusCode statusCode)
      {
         return this.CreateMessage(statusCode, string.Empty);
      }

      public HttpResponseMessage CreateMessage(HttpStatusCode statusCode, string content)
      {
         var responseMessage = new HttpResponseMessage(statusCode);
         if (!string.IsNullOrEmpty(content)) {
            responseMessage.Content = new StringContent(content);
         }
         return responseMessage;
      }

   }
}