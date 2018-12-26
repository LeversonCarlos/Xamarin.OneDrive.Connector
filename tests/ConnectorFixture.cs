using System;
using Xamarin.OneDrive;

namespace Xamarin.OneDrive.Tests
{
   public class ConnectorFixture : IDisposable
   { 
      internal Connector Connector { get; private set; }

      public ConnectorFixture()
      {
         var configs = new Configs { ClientID = Settings.ClientID };
         this.Connector = new Connector(configs);
      }

      public void Dispose()
      {
         this.Connector.Dispose();
         this.Connector = null;
      }

   }
}