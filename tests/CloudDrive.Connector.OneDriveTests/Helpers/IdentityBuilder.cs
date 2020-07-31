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

      public IdentityBuilder WithScopes(string[] scopes)
      {
         this.Mock.SetupGet(m => m.Scopes).Returns(scopes);
         return this;
      }

      public IdentityBuilder WithAcquireTokenFromIdentity(string accessCode, DateTimeOffset expiresOn, string[] scopes)
      {
         var result = new AuthenticationResult(accessCode, false, null, expiresOn, expiresOn.AddMinutes(1), null, null, null, scopes, Guid.Empty);
         this.Mock.Setup(m => m.AcquireTokenFromIdentityAsync()).ReturnsAsync(result);
         return this;
      }

      public IdentityBuilder WithGetAccounts(string userName)
      {
         this.Mock.Setup(m => m.GetAccountsAsync()).ReturnsAsync(new IAccount[] { GetAccount(userName) });
         return this;
      }

      static IAccount GetAccount(string userName)
      {
         var mock = new Mock<IAccount>();
         mock.SetupGet(m => m.Username).Returns(userName);
         return mock.Object;
      }

      public IOneDriveIdentity Build() => this.Mock.Object;
   }
}
