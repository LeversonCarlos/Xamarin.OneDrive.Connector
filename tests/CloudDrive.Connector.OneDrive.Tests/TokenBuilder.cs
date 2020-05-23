using Moq;

namespace Xamarin.CloudDrive.Connector.OneDrive.Tests
{
   internal class TokenBuilder
   {

      readonly Mock<IToken> Mock;
      public TokenBuilder() => this.Mock = new Mock<IToken>();
      public static TokenBuilder Create() => new TokenBuilder();
      public IToken Builder() => this.Mock.Object;

      public TokenBuilder WithTokenState(bool state)
      {
         this.Mock.Setup(m => m.IsTokenValid()).Returns(state);
         return this;
      }

      public TokenBuilder WithRefreshToken(bool state)
      {
         this.Mock.Setup(m => m.RefreshTokenAsync()).ReturnsAsync(state);
         return this;
      }

      public TokenBuilder WithAcquireToken(bool state)
      {
         this.Mock.Setup(m => m.AcquireTokenAsync()).ReturnsAsync(state);
         return this;
      }

   }
}