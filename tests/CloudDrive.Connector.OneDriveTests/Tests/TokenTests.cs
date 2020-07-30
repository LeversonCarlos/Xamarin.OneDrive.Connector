using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public partial class Token
   {

      [Fact]
      public void Constructor_WithNullParameter_MustThrowException()
      {
         var exception = Assert.Throws<ArgumentException>(() => new OneDriveToken(identity: null));

         Assert.NotNull(exception);
         Assert.Equal("The identity argument for the token client must be set", exception.Message);
      }

      [Fact]
      public void Constructor_WithValidParameter_ShouldBeInitializedWithEmptyToken()
      {
         var identity = IdentityBuilder.Create().Build();
         var token = new OneDriveToken(identity);

         var expected = "";
         var actual = token.GetToken();

         Assert.Equal(expected, actual);
      }

   }
}
