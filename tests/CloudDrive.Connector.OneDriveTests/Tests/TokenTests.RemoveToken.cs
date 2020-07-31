using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Token
   {

      [Fact]
      public async void RemoveTokenAsync_WithException_MustThrowException()
      {
         var ex = new Exception("Just a test exception");
         var identity = IdentityBuilder.Create().WithGetAccounts(ex).Build();
         var token = new OneDriveToken(identity);

         var actual = await Assert.ThrowsAsync<Exception>(async () => await token.RemoveTokenAsync());

         Assert.NotNull(actual);
         Assert.Equal(ex.Message, actual.Message);
      }

   }
}
