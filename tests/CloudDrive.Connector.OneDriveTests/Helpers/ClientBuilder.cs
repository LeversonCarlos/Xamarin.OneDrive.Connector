using Moq;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   internal class ClientBuilder
   {

      readonly Mock<IOneDriveClient> Mock;
      public ClientBuilder() => this.Mock = new Mock<IOneDriveClient>();
      public static ClientBuilder Create() => new ClientBuilder();

      public IOneDriveClient Build() => this.Mock.Object;
   }
}
