using Microsoft.Identity.Client;
using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Token
   {

      [Fact]
      public async void AcquireTokenAsync_WithException_MustResultFalse()
      {
         var ex = new Exception("Just a test exception");
         var identity = IdentityBuilder.Create().WithAcquireTokenFromIdentity(ex).Build();
         var token = new OneDriveToken(identity);

         var expected = false;
         var actual = await token.AcquireTokenAsync();

         Assert.Equal(expected, actual);
      }

   }
}
