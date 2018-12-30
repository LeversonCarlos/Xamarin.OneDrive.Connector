using System;
using System.Net.Http;

namespace Xamarin.OneDrive
{
   public partial class Connector : HttpClient
   {
      const string BaseURL = "https://graph.microsoft.com/v1.0/";

      public Connector(string clientID, params string[] scopes) : this(new Configs { ClientID = clientID, Scopes = scopes })
      { }

      internal Connector(Configs configs) : base(new ConnectorHandler(configs))
      {
         this.BaseAddress = new Uri(BaseURL);
      }

   }
}