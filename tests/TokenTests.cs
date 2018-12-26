using System;
using Xunit;
using Xamarin.OneDrive.Connector;

namespace Xamarin.OneDrive.Connector.Tests
{
   public class TokenTests : IClassFixture<TokenFixture>
   {

      TokenFixture TokenFixture;

      public TokenTests()
      {
         this.TokenFixture = new TokenFixture();
      }

      [Fact]
      public void ClientID_MustMatch_DefaultValue()
      {
         Assert.Equal(Settings.ClientID, this.TokenFixture.Token.Configs.ClientID);
      }

      [Fact]
      public void InitialToken_MustBe_Invalid()
      {
         Assert.False(this.TokenFixture.Token.IsValid());
      }

   }
}