using System;
using System.Net.Http;

namespace Xamarin.CloudDrive.Connector
{
   internal partial class OneDriveClient : IOneDriveClient
   {

      internal OneDriveClient(IOneDriveToken token)
      {
         Token = token;
         _HttpClient = new HttpClient(new OneDriveClientHandler(token))
         { BaseAddress = new Uri("https://graph.microsoft.com/v1.0/") };
      }

      readonly HttpClient _HttpClient;

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

   }
}
