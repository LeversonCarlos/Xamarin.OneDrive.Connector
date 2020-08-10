using System;
using System.IO;
using System.Net.Http;

namespace Xamarin.CloudDrive.Connector
{
   public partial class OneDriveClient : IOneDriveClient
   {
      internal const string MicrosoftGraphUrl = "https://graph.microsoft.com/v1.0/";

      internal OneDriveClient(IOneDriveToken token) :
         this(token, new OneDriveClientHandler(token))
      { }

      internal OneDriveClient(IOneDriveToken token, HttpMessageHandler messageHandler)
      {
         Token = token;
         _HttpClient = new HttpClient(messageHandler)
         { BaseAddress = new Uri(MicrosoftGraphUrl) };
      }

      internal readonly HttpClient _HttpClient;

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
