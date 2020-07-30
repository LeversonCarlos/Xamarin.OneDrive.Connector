using Microsoft.Identity.Client;
using Moq;
using System;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   internal interface IIdentity : IClientApplicationBase, IConfidentialClientApplication, IPublicClientApplication { }

   internal class IdentityBuilder
   {
      readonly Mock<IIdentity> Mock;
      public IdentityBuilder() => this.Mock = new Mock<IIdentity>();
      public static IdentityBuilder Create() => new IdentityBuilder();

      public IdentityBuilder WithTokenForClient(string accessCode, DateTimeOffset expiresOn, string[] scopes)
      {
         var result = IdentityResultForClientBuilder.Create().With(accessCode, expiresOn, scopes).Build();
         this.Mock.Setup(m => m.AcquireTokenForClient(scopes)).Returns(result);
         return this;
      }

      public IdentityBuilder WithTokenInteractive(string accessCode, DateTimeOffset expiresOn, string[] scopes)
      {
         var result = IdentityResultInteractiveBuilder.Create().With(accessCode, expiresOn, scopes).Build();
         this.Mock.Setup(m => m.AcquireTokenInteractive(scopes)).Returns(result);
         return this;
      }

      public IIdentity Build() => this.Mock.Object;
   }

   internal class IdentityResultForClientBuilder
   {
      readonly Mock<AcquireTokenForClientParameterBuilder> Mock;
      public IdentityResultForClientBuilder() => this.Mock = new Mock<AcquireTokenForClientParameterBuilder>();
      public static IdentityResultForClientBuilder Create() => new IdentityResultForClientBuilder();

      public IdentityResultForClientBuilder With(string accessCode, DateTimeOffset expiresOn, string[] scopes)
      {
         var result = new AuthenticationResult(accessCode, false, null, expiresOn, expiresOn.AddMinutes(1), null, null, null, scopes, Guid.Empty);
         this.Mock.Setup(m => m.ExecuteAsync()).ReturnsAsync(result);
         return this;
      }

      public AcquireTokenForClientParameterBuilder Build() => this.Mock.Object;
   }

   internal class IdentityResultInteractiveBuilder
   {
      readonly Mock<AcquireTokenInteractiveParameterBuilder> Mock;
      public IdentityResultInteractiveBuilder() => this.Mock = new Mock<AcquireTokenInteractiveParameterBuilder>();
      public static IdentityResultInteractiveBuilder Create() => new IdentityResultInteractiveBuilder();

      public IdentityResultInteractiveBuilder With(string accessCode, DateTimeOffset expiresOn, string[] scopes)
      {
         var result = new AuthenticationResult(accessCode, false, null, expiresOn, expiresOn.AddMinutes(1), null, null, null, scopes, Guid.Empty);
         this.Mock.Setup(m => m.ExecuteAsync()).ReturnsAsync(result);
         return this;
      }

      public AcquireTokenInteractiveParameterBuilder Build() => this.Mock.Object;
   }

}
