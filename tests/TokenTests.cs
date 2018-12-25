using System;
using Xunit;
using Xamarin.OneDrive.Connector;

namespace Xamarin.OneDrive.Connector.Tests
{
   public class TokenTests
   {

      [Fact]
      public void ClientProperty_MustMatch_Definition()
      {
         using (var configs = new Configs { ClientID = Settings.ClientID })
         {
            using (var token = new Token(configs))
            {
               Assert.Equal(Settings.ClientID, token.Configs.ClientID);
            }
         }
      }

   }
}