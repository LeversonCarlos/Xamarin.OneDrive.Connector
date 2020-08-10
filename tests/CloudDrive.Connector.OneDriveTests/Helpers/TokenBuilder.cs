using Moq;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   internal class TokenBuilder
   {

      readonly Mock<IOneDriveToken> Mock;
      public TokenBuilder() => this.Mock = new Mock<IOneDriveToken>();
      public static TokenBuilder Create() => new TokenBuilder();
      public IOneDriveToken Build() => this.Mock.Object;

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
