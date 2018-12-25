using System;
using Xunit;
using Xamarin.OneDrive.Connector;

namespace Xamarin.OneDrive.Connector.Tests
{
   public class TokenTests
   {
      const string TestClientID = "MyClientID";

      [Fact]
      public void ClientProperty_MustMatch_Definition()
      {
         using (var configs = new Configs { ClientID = TestClientID })
         {
            using (var token = new Token(configs))
            {
               Assert.Equal(TestClientID, token.Configs.ClientID);
            }
         }
      }

   }
}
