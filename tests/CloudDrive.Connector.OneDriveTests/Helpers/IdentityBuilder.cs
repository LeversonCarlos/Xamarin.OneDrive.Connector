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

      string _AccessCode;
      public IdentityBuilder With(string accessCode)
      {
         _AccessCode = accessCode;
         return this;
      }

      DateTimeOffset _ExpiresOn;
      DateTimeOffset _ExpiresOnExtended;
      public IdentityBuilder With(DateTimeOffset expiresOn)
      {
         _ExpiresOn = expiresOn;
         _ExpiresOnExtended = expiresOn.AddMinutes(1);
         return this;
      }

      string[] _Scopes;
      public IdentityBuilder With(string[] scopes)
      {
         _Scopes = scopes;
         return this;
      }

      public IdentityBuilder WithResult()
      {
         var result = new AuthenticationResult(_AccessCode, false, null, _ExpiresOn, _ExpiresOnExtended, null, null, null, _Scopes, Guid.Empty);
         this.Mock.Setup(m => m.AcquireTokenFromIdentityAsync()).ReturnsAsync(result);
         return this;
      }

      public IOneDriveIdentity Build() => this.Mock.Object;
   }
}
