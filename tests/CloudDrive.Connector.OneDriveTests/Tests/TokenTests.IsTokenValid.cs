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

   }
}
