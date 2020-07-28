using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public class SettingsTests
   {

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      [InlineData("{YOUR_MICROSOFT_APPLICATION_ID}")]
      public void Constructor_WithInvalidClientID_MustThrowException(string clientID)
      {
         var value = new Action(() => new OneDriveSettings(clientID, "[test]", "[test]", new string[] { "[test]" }));

         var exception = Assert.Throws<ArgumentException>(value);
         Assert.NotNull(exception);
         Assert.Equal("The clientID argument for the onedrive client must be set", exception.Message);
      }

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      [InlineData("{YOUR_MICROSOFT_APPLICATION_SECRET}")]
      public void Constructor_WithInvalidClientSecret_MustThrowException(string clientSecret)
      {
         var value = new Action(() => new OneDriveSettings("[test]", clientSecret, "[test]", new string[] { "[test]" }));

         var exception = Assert.Throws<ArgumentException>(value);
         Assert.NotNull(exception);
         Assert.Equal("The clientSecret argument for the onedrive client must be set", exception.Message);
      }

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      [InlineData("msal{YOUR_MICROSOFT_APPLICATION_ID}://auth")]
      public void Constructor_WithInvalidRedirectUri_MustThrowException(string redirectUri)
      {
         var value = new Action(() => new OneDriveSettings("[test]", "[test]", redirectUri, new string[] { "[test]" }));

         var exception = Assert.Throws<ArgumentException>(value);
         Assert.NotNull(exception);
         Assert.Equal("The redirectUri argument for the onedrive client must be set", exception.Message);
      }

      [Theory]
      [InlineData(null, "The scopes argument for the onedrive client must be set")]
      [InlineData(new string[] { }, "The scopes argument for the onedrive client must be set")]
      [InlineData(new string[] { "" }, "The scopes argument for the onedrive client must be set")]
      public void Constructor_WithInvalidScopes_MustThrowException(string[] scopes, string message)
      {
         var value = new Action(() => new OneDriveSettings("[test]", "[test]", "[test]", scopes));

         var exception = Assert.Throws<ArgumentException>(value);
         Assert.NotNull(exception);
         Assert.Equal(message, exception.Message);
      }


   }
}
