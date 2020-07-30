using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Token
   {

      [Fact]
      public void IsTokenValid_WithNullAuth_MustResultFalse()
      {
         var identity = IdentityBuilder.Create().Build();
         var token = new OneDriveToken(identity);

         var expected = false;
         var actual = token.IsTokenValid();

         Assert.Equal(expected, actual);
      }

      [Fact]
      public async void IsTokenValid_WithEmptyAccessToken_MustResultFalse()
      {
         var identity = IdentityBuilder.Create()
            .WithAcquireTokenFromIdentity("", DateTimeOffset.UtcNow, null)
            .Build();
         var token = new OneDriveToken(identity);
         await token.AcquireTokenAsync();

         var expected = false;
         var actual = token.IsTokenValid();

         Assert.Equal(expected, actual);
      }

      [Theory]
      [InlineData(-61)]
      [InlineData(-555.55)]
      public async void IsTokenValid_WithExpiredDate_MustResultFalse(double expiresSeconds)
      {
         var identity = IdentityBuilder.Create()
            .WithAcquireTokenFromIdentity("[test]", DateTimeOffset.UtcNow.AddSeconds(expiresSeconds), null)
            .Build();
         var token = new OneDriveToken(identity);
         await token.AcquireTokenAsync();

         var expected = false;
         var actual = token.IsTokenValid();

         Assert.Equal(expected, actual);
      }

   }
}
