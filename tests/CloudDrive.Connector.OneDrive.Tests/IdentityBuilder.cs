using Microsoft.Identity.Client;
using Moq;

namespace Xamarin.CloudDrive.Connector.OneDrive.Tests
{
   internal class IdentityBuilder
   {

      readonly Mock<IClientApplicationBase> Mock;
      public IdentityBuilder() => this.Mock = new Mock<IClientApplicationBase>();
      public static IdentityBuilder Create() => new IdentityBuilder();
      public IClientApplicationBase Builder() => this.Mock.Object;

      public IdentityBuilder WithEmptyAccountList()
      {
         this.Mock
            .Setup(m => m.GetAccountsAsync())
            .ReturnsAsync(() => new IAccount[] { });
         return this;
      }

   }
}
