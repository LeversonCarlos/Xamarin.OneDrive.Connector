using Microsoft.Identity.Client;
using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Token
   {

      [Fact]
      public async void CheckConnectionAsync_WithValidToken_MustResultTrue()
      {
         var identity = IdentityBuilder.Create()
            .WithScopes(new string[] { "A" })
            .WithAcquireTokenFromIdentity("[test]", DateTimeOffset.UtcNow, new string[] { "A" })
            .Build();
         var token = new OneDriveToken(identity);
         await token.AcquireTokenAsync();

         var expected = true;
         var actual = await token.CheckConnectionAsync();

         Assert.Equal(expected, actual);
      }

      [Fact]
      public async void CheckConnectionAsync_WithInvalidTokenThatGetsRefreshed_MustResultTrue()
      {
         var account = IdentityBuilder.GetAccount("Dummy Username");
         var identity = IdentityBuilder
            .Create()
            .WithScopes(new string[] { "A" })
            .WithGetAccounts(new IAccount[] { account })
            .WithAcquireTokenSilent(account, "[test]", DateTimeOffset.UtcNow, new string[] { "A" })
            .Build();
         var token = new OneDriveToken(identity);

         var expected = true;
         var actual = await token.CheckConnectionAsync();

         Assert.Equal(expected, actual);
      }

      [Fact]
      public async void CheckConnectionAsync_WithInvalidTokenThatDidntEvolve_MustResultFalse()
      {
         var identity = IdentityBuilder.Create()
            .Build();
         var token = new OneDriveToken(identity);

         var expected = false;
         var actual = await token.CheckConnectionAsync();

         Assert.Equal(expected, actual);
      }

   }
}
