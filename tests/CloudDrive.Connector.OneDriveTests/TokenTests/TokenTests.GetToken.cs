using Microsoft.Identity.Client;
using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Token
   {

      [Fact]
      public async void GetToken_WithInvalidToken_MustResultEmpty()
      {
         var identity = IdentityBuilder.Create().Build();
         var token = new OneDriveToken(identity);

         var expected = false;
         var actual = await token.RefreshTokenAsync();

         Assert.Equal(expected, actual);
      }

      [Fact]
      public async void GetToken_WithValidToken_MustResultItsValue()
      {
         var dummyToken = "My Dummy Token";
         var scopes = new string[] { "A" };
         var identity = IdentityBuilder
            .Create()
            .WithScopes(scopes)
            .WithAcquireTokenFromIdentity(dummyToken, DateTimeOffset.UtcNow, scopes)
            .Build();
         var token = new OneDriveToken(identity);

         await token.AcquireTokenAsync();
         var actual = token.GetToken();

         Assert.Equal(dummyToken, actual);
      }

   }
}
