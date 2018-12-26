using System;
using Xamarin.OneDrive;

namespace Xamarin.OneDrive.Tests
{
   public class ConnectorFixture : IDisposable
   { 
      internal Connector Connector { get; private set; }

      public ConnectorFixture()
      {
         var configs = new Configs
         {
            ClientID = Settings.ClientID,
            Scopes = new string[] { "User.Read" }
         };
         this.Connector = new Connector(configs);
         // var connectorResult = this.Connector.ConnectAsync().GetAwaiter().GetResult();
      }

      public void Dispose()
      {
         this.Connector.Dispose();
         this.Connector = null;
      }

   }
}