using System;
using System.Net.Http;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public class Client : HttpClient, IClient
   {

      public Client(Token token)
      {
         if (token == null) throw new ArgumentException("The token argument for the OneDrive client must be set");
         this.Token = token;
      }

      readonly Token Token;

   }
}
