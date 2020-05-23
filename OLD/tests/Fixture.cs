using System;
using Xamarin.OneDrive;
using Xamarin.OneDrive.Profile;

namespace Xamarin.OneDrive.Tests
{
   public class Fixture : IDisposable
   {
      internal Connector Connector { get; private set; }

      public Fixture()
      {
         var configs = new Configs { ClientID = Settings.ClientID, Scopes = Settings.Scopes };
         this.Connector = new Connector(configs);
      }

      public void Dispose()
      {
         this.Connector.Dispose();
         this.Connector = null;
      }

   }
}