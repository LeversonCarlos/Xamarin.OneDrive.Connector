using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public class Token
   {

      [Fact]
      public void Constructor_NullIdentityParameter_MustThrowException()
      {
         var exception = Assert.Throws<ArgumentException>(() => new OneDriveToken(null, new string[] { "[test]" }));

         Assert.NotNull(exception);
         Assert.Equal("The identity argument for the token client must be set", exception.Message);
      }

      [Theory]
      [InlineData(null, "The scopes argument for the onedrive client must be set")]
      [InlineData(new string[] { }, "The scopes argument for the onedrive client must be set")]
      [InlineData(new string[] { "" }, "The scopes argument for the onedrive client must be set")]
      public void Constructor_InvalidScopesParameter_MustThrowException(string[] scopes, string message)
      {
         var identity = IdentityBuilder.Create().WithTokenForClient(null, DateTimeOffset.Now, null).Build();

         var exception = Assert.Throws<ArgumentException>(() => new OneDriveToken(identity, scopes));

         Assert.NotNull(exception);
         Assert.Equal(message, exception.Message);
      }

      [Fact]
      public void InitialTokenMustBeEmpty()
      {
         var identity = IdentityBuilder.Create().Build();
         var token = new OneDriveToken(identity, null);

         var expected = "";
         var actual = token.GetToken();

         Assert.Equal(expected, actual);
      }

   }
}
