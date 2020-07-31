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
         var identity = IdentityBuilder.Create().Build();
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
      public async void RefreshTokenAsync_WithValidAccount_ButFaileAUth_MustResultFalse()
      {
         var identity = IdentityBuilder.Create().WithGetAccounts("Dummy Username").Build();
         var token = new OneDriveToken(identity);

         var expected = false;
         var actual = await token.RefreshTokenAsync();

         Assert.Equal(expected, actual);
      }

   }
}
