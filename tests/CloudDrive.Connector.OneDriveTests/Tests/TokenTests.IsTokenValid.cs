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
      public void IsTokenValid_WithEmptyAccessToken_MustResultFalse()
      {
         var identity = IdentityBuilder.Create().With("").WithResult().Build();
         var token = new OneDriveToken(identity);

         var expected = false;
         var actual = token.IsTokenValid();

         Assert.Equal(expected, actual);
      }

      [Theory]
      [InlineData(-61)]
      [InlineData(-555.55)]
      public void IsTokenValid_WithExpiredDate_MustResultFalse(double expiresSeconds)
      {
         var identity = IdentityBuilder.Create().With("[test]").With(DateTimeOffset.UtcNow.AddSeconds(expiresSeconds)).WithResult().Build();
         var token = new OneDriveToken(identity);

         var expected = false;
         var actual = token.IsTokenValid();

         Assert.Equal(expected, actual);
      }


   }
}
