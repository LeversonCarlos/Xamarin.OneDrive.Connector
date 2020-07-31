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

      public IdentityBuilder WithAcquireTokenSilent(IAccount account, string accessCode, DateTimeOffset expiresOn, string[] scopes)
      {
         var result = GetAuthResult(accessCode, expiresOn, scopes);
         this.Mock.Setup(m => m.AcquireTokenSilentAsync(account)).ReturnsAsync(result);
         return this;
      }

      public IdentityBuilder WithAcquireTokenFromIdentity(string accessCode, DateTimeOffset expiresOn, string[] scopes)
      {
         var result = GetAuthResult(accessCode, expiresOn, scopes);
         this.Mock.Setup(m => m.AcquireTokenFromIdentityAsync()).ReturnsAsync(result);
         return this;
      }
      public IdentityBuilder WithAcquireTokenFromIdentity(Exception ex)
      {
         this.Mock.Setup(m => m.AcquireTokenFromIdentityAsync()).ThrowsAsync(ex);
         return this;
      }

      public IdentityBuilder WithGetAccounts(string userName)
      {
         return WithGetAccounts(new IAccount[] { GetAccount(userName) });
      }
      public IdentityBuilder WithGetAccounts(IAccount[] accounts)
      {
         this.Mock.Setup(m => m.GetAccountsAsync()).ReturnsAsync(accounts);
         return this;
      }
      public IdentityBuilder WithGetAccounts(Exception ex)
      {
         this.Mock.Setup(m => m.GetAccountsAsync()).ThrowsAsync(ex);
         return this;
      }

      public static IAccount GetAccount(string userName)
      {
         var mock = new Mock<IAccount>();
         mock.SetupGet(m => m.Username).Returns(userName);
         return mock.Object;
      }

      public static AuthenticationResult GetAuthResult(string accessCode, DateTimeOffset expiresOn, string[] scopes) =>
         new AuthenticationResult(accessCode, false, null, expiresOn, expiresOn.AddMinutes(1), null, null, null, scopes, Guid.Empty);

      public IOneDriveIdentity Build() => this.Mock.Object;
   }
}
