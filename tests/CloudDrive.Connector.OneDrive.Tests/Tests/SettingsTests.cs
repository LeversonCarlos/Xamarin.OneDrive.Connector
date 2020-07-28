using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public class SettingsTests
   {

      const string clientIDException = "The clientID argument for the onedrive client must be set";
      const string clientSecretException = "The clientSecret argument for the onedrive client must be set";
      const string redirectUriException = "The redirectUri argument for the onedrive client must be set";
      const string scopesException = "The scopes argument for the onedrive client must be set";
      [Theory]
      [InlineData((string)null, null, null, null, clientIDException)]
      [InlineData("", null, null, null, clientIDException)]
      [InlineData("{YOUR_MICROSOFT_APPLICATION_ID}", null, null, null, clientIDException)]
      [InlineData("[test]", (string)null, "[test]", new string[] { "[test]" }, clientSecretException)]
      [InlineData("[test]", "", "[test]", new string[] { "[test]" }, clientSecretException)]
      [InlineData("[test]", "{YOUR_MICROSOFT_APPLICATION_SECRET}", "[test]", new string[] { "[test]" }, clientSecretException)]
      [InlineData("[test]", "[test]", (string)null, null, redirectUriException)]
      [InlineData("[test]", "[test]", "", null, redirectUriException)]
      [InlineData("[test]", "[test]", "msal{YOUR_MICROSOFT_APPLICATION_ID}://auth", null, redirectUriException)]
      [InlineData("[test]", "[test]", "[test]", null, scopesException)]
      [InlineData("[test]", "[test]", "[test]", new string[] { }, scopesException)]
      [InlineData("[test]", "[test]", "[test]", new string[] { "" }, scopesException)]
      public void Constructor_WithInvalidParameters_MustThrowException(string clientID, string clientSecret, string redirectUri, string[] scopes, string exceptionMessage)
      {
         var value = new Action(() => new OneDriveSettings(clientID, clientSecret, redirectUri, scopes));

         var exception = Assert.Throws<ArgumentException>(value);
         Assert.NotNull(exception);
         Assert.Equal(exceptionMessage, exception.Message);
      }

   }
}
