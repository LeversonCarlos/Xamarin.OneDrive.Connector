using Microsoft.Identity.Client;
using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Token
   {

      [Fact]
      public async void RefreshTokenAsync_WithNullAccounts_MustResultFalse()
      {
         var identity = IdentityBuilder.Create().WithGetAccounts(accounts: null).Build();
         var token = new OneDriveToken(identity);

         var expected = false;
         var actual = await token.RefreshTokenAsync();

         Assert.Equal(expected, actual);
      }

      [Fact]
      public async void RefreshTokenAsync_WithException_MustResultFalse()
      {
         var ex = new Exception("Just a test exception");
         var identity = IdentityBuilder.Create().WithGetAccounts(ex).Build();
         var token = new OneDriveToken(identity);

         var expected = false;
         var actual = await token.RefreshTokenAsync();

         Assert.Equal(expected, actual);
      }

      [Fact]
      public async void RefreshTokenAsync_WithInvalidAccounts_MustResultFalse()
      {
         var identity = IdentityBuilder.Create().WithGetAccounts(new IAccount[] { }).Build();
         var token = new OneDriveToken(identity);

         var expected = false;
         var actual = await token.RefreshTokenAsync();

         Assert.Equal(expected, actual);
      }

      [Fact]
      public async void RefreshTokenAsync_WithValidAccount_ButFailedAuth_MustResultFalse()
      {
         var identity = IdentityBuilder.Create().WithGetAccounts("Dummy Username").Build();
         var token = new OneDriveToken(identity);

         var expected = false;
         var actual = await token.RefreshTokenAsync();

         Assert.Equal(expected, actual);
      }

      [Fact]
      public async void RefreshTokenAsync_WithValidAccount_AndSuccessAuth_MustResultTrue()
      {
         var account = IdentityBuilder.GetAccount("Dummy Username");
         var identity = IdentityBuilder
            .Create()
            .WithScopes(new string[] { "Dummy Scope" })
            .WithGetAccounts(new IAccount[] { account })
            .WithAcquireTokenSilent(account, "Dummy AccessCode", DateTimeOffset.UtcNow, new string[] { "Dummy Scope" })
            .Build();
         var token = new OneDriveToken(identity);

         var expected = true;
         var actual = await token.RefreshTokenAsync();

         Assert.Equal(expected, actual);
      }

   }
}
