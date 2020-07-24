using Moq;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   internal class ConnectionBuilder
   {

      readonly Mock<IConnection> Mock;
      public ConnectionBuilder() => this.Mock = new Mock<IConnection>();

      public static ConnectionBuilder Create() => new ConnectionBuilder();

      public IConnection Build() => this.Mock.Object;

      public ConnectionBuilder WithConnectValue(bool value)
      {
         this.Mock.Setup(m => m.ConnectAsync()).ReturnsAsync(value);
         return this;
      }

      public ConnectionBuilder WithCheckConnectionValue(bool value)
      {
         this.Mock.Setup(m => m.CheckConnectionAsync()).ReturnsAsync(value);
         return this;
      }

   }
}