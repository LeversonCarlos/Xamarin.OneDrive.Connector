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

   }
}
