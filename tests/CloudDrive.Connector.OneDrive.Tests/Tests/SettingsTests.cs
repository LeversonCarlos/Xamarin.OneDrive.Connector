using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public class SettingsTests
   {

      [Theory]
      [InlineData((string)null, null, null,
         "The application ID argument for the onedrive client must be set")]
      [InlineData("", null, null,
         "The application ID argument for the onedrive client must be set")]
      [InlineData("{YOUR_MICROSOFT_APPLICATION_ID}", null, null,
         "The application ID argument for the onedrive client must be set")]
      [InlineData("[test]", (string)null, null,
         "The redirectUri argument for the onedrive client must be set")]
      [InlineData("[test]", "", null,
         "The redirectUri argument for the onedrive client must be set")]
      [InlineData("[test]", "msal{YOUR_MICROSOFT_APPLICATION_ID}://auth", null,
         "The redirectUri argument for the onedrive client must be set")]
      [InlineData("[test]", "[test]", null,
         "The scopes argument for the onedrive client must be set")]
      [InlineData("[test]", "[test]", new string[] { },
         "The scopes argument for the onedrive client must be set")]
      [InlineData("[test]", "[test]", new string[] { "" },
         "The scopes argument for the onedrive client must be set")]
      public void Constructor_WithInvalidParameters_MustThrowException(string clientID, string redirectUri, string[] scopes, string exceptionMessage)
      {
         var value = new Action(() => new OneDriveSettings(clientID, redirectUri, scopes));

         var exception = Assert.Throws<ArgumentException>(value);
         Assert.NotNull(exception);
         Assert.Equal(exceptionMessage, exception.Message);
      }

   }
}
