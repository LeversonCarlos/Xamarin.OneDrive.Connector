using System;
using Xunit;
using Xamarin.OneDrive.Connector;

namespace Xamarin.OneDrive.Connector.Tests
{
   public class TokenTests
   {

      [Fact]
      public void ClientProperty_MustMatch_DefaultValue()
      {
         using (var configs = new Configs { ClientID = Settings.ClientID })
         {
            using (var token = new Token(configs))
            {
               Assert.Equal(Settings.ClientID, token.Configs.ClientID);
            }
         }
      }

      [Fact]
      public void InitialToken_MustBe_Invalid()
      {
         using (var configs = new Configs { ClientID = Settings.ClientID })
         {
            using (var token = new Token(configs))
            {
               Assert.Equal(false, token.IsValid());
            }
         }
      }

   }
}