using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("CloudDrive.Connector.OneDriveTests")]

namespace Xamarin.CloudDrive.Connector
{
   internal class OneDriveSettings
   {

      internal OneDriveSettings(string clientID, string clientSecret, string redirectUri, string[] scopes) : this(clientID, redirectUri, scopes)
      {
         if (string.IsNullOrEmpty(clientSecret) || clientSecret == "{YOUR_MICROSOFT_APPLICATION_SECRET}")
            throw new ArgumentException("The clientSecret argument for the onedrive client must be set");
         ClientSecret = clientSecret;
      }

      internal OneDriveSettings(string clientID, string redirectUri, string[] scopes)
      {

         if (string.IsNullOrEmpty(clientID) || clientID == "{YOUR_MICROSOFT_APPLICATION_ID}")
            throw new ArgumentException("The application ID argument for the onedrive client must be set");
         ClientID = clientID;

         if (string.IsNullOrEmpty(redirectUri) || redirectUri == "msal{YOUR_MICROSOFT_APPLICATION_ID}://auth")
            throw new ArgumentException("The redirectUri argument for the onedrive client must be set");
         RedirectUri = redirectUri;

         if (scopes == null || scopes?.Where(scope => !string.IsNullOrEmpty(scope)).Count() == 0)
            throw new ArgumentException("The scopes argument for the onedrive client must be set");
         Scopes = scopes;
      }

      public string ClientID { get; }
      public string RedirectUri { get; }
      public string ClientSecret { get; }
      public string[] Scopes { get; }

   }
}
