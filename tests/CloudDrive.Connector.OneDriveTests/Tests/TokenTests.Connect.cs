using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Token
   {

      [Fact]
      public async void ConnectAsync_WithValidToken_MustResultTrue()
      {
         var identity = IdentityBuilder.Create()
            .WithScopes(new string[] { "A" })
            .WithAcquireTokenFromIdentity("[test]", DateTimeOffset.UtcNow, new string[] { "A" })
            .Build();
         var token = new OneDriveToken(identity);
         await token.AcquireTokenAsync();

         var expected = true;
         var actual = await token.ConnectAsync();

         Assert.Equal(expected, actual);
      }

   }
}
