using Microsoft.Identity.Client;
using Moq;
using System;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   internal class IdentityBuilder
   {
      readonly Mock<IOneDriveIdentity> Mock;
      public IdentityBuilder() => this.Mock = new Mock<IOneDriveIdentity>();
      public static IdentityBuilder Create() => new IdentityBuilder();

      public IdentityBuilder WithTokenForClient(string accessCode, DateTimeOffset expiresOn, string[] scopes)
      {
         var result = new AuthenticationResult(accessCode, false, null, expiresOn, expiresOn.AddMinutes(1), null, null, null, scopes, Guid.Empty);
         this.Mock.Setup(m => m.AcquireTokenFromIdentityAsync()).ReturnsAsync(result);
         return this;
      }

      public IOneDriveIdentity Build() => this.Mock.Object;
   }
}
