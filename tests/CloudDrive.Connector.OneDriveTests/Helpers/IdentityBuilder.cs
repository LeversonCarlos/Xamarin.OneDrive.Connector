using Microsoft.Identity.Client;
using Moq;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   internal class IdentityBuilder
   {

      readonly Mock<IClientApplicationBase> Mock;
      public IdentityBuilder() => this.Mock = new Mock<IClientApplicationBase>();
      public static IdentityBuilder Create() => new IdentityBuilder();
      public IClientApplicationBase Builder() => this.Mock.Object;

   }
}