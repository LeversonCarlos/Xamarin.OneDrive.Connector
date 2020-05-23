using System;
using System.Net.Http;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public class Client : HttpClient, IClient
   {

      const string BaseURL = "https://graph.microsoft.com/v1.0/";
      readonly IToken Token;

      public Client(IToken token)
      {
         if (token == null) throw new ArgumentException("The token argument for the OneDrive client must be set");
         this.Token = token;
         this.BaseAddress = new Uri(BaseURL);
      }

   }
}
