using Moq;

namespace Xamarin.CloudDrive.Connector.OneDrive.Tests
{
   internal class TokenBuilder
   {

      readonly Mock<IToken> Mock;
      public TokenBuilder() => this.Mock = new Mock<IToken>();
      public static TokenBuilder Create() => new TokenBuilder();
      public IToken Builder() => this.Mock.Object;

      public TokenBuilder WithConnectionState(bool state)
      {
         this.Mock.Setup(m => m.CheckConnectionAsync()).ReturnsAsync(state);
         return this;
      }

      public TokenBuilder WithConnectExecution(bool result)
      {
         this.Mock.Setup(m => m.ConnectAsync()).ReturnsAsync(result);
         return this;
      }

   }
}
