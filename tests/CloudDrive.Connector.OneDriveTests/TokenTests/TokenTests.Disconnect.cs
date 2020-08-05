using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Token
   {

      [Fact]
      public async void DisconnectAsync_WithException_MustThrowException()
      {
         var ex = new Exception("Just a test exception");
         var identity = IdentityBuilder.Create().WithGetAccounts(ex).Build();
         var token = new OneDriveToken(identity);

         var actual = await Assert.ThrowsAsync<Exception>(async () => await token.DisconnectAsync());

         Assert.NotNull(actual);
         Assert.Equal(ex.Message, actual.Message);
      }

      [Fact]
      public async void DisconnectAsync_WithNullAccounts_MustResultWithInvalidToken()
      {
         var identity = IdentityBuilder.Create().WithGetAccounts(accounts: null).Build();
         var token = new OneDriveToken(identity);

         await token.DisconnectAsync();

         var expected = false;
         var actual = token.IsTokenValid();

         Assert.Equal(expected, actual);
      }

      [Fact]
      public async void DisconnectAsync_WithAccounts_MustResultWithInvalidToken()
      {
         var identity = IdentityBuilder.Create().WithGetAccounts("Dummy Username").Build();
         var token = new OneDriveToken(identity);

         await token.DisconnectAsync();

         var expected = false;
         var actual = token.IsTokenValid();

         Assert.Equal(expected, actual);
      }

   }
}
