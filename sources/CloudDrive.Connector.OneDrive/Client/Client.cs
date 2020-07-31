using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal partial class OneDriveClient : HttpClient, IOneDriveClient
   {

      internal OneDriveClient(IOneDriveToken token) :
         base(new OneDriveClientHandler(token))
      {
         Token = token;
         BaseAddress = new Uri("https://graph.microsoft.com/v1.0/");
      }

      IOneDriveToken _Token;
      internal IOneDriveToken Token
      {
         get => _Token;
         private set
         {
            if (value == null)
               throw new ArgumentException("The token argument for the http client must be set");
            _Token = value;
         }
      }

      public Task<bool> ConnectAsync() =>
         Token.ConnectAsync();

      public Task<bool> CheckConnectionAsync() =>
         Token.CheckConnectionAsync();

      public Task DisconnectAsync() =>
         Token.DisconnectAsync();

   }
}
