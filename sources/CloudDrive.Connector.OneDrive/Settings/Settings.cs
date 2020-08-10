using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("CloudDrive.Connector.OneDriveTests")]

namespace Xamarin.CloudDrive.Connector
{
   internal partial class OneDriveSettings
   {

      string _ClientID;
      public string ClientID
      {
         get => _ClientID;
         private set
         {
            if (string.IsNullOrEmpty(value) || value == "{YOUR_MICROSOFT_APPLICATION_ID}")
               throw new ArgumentException("The clientID argument for the onedrive client must be set");
            _ClientID = value;
         }
      }

      string _ClientSecret;
      public string ClientSecret
      {
         get => _ClientSecret;
         private set
         {
            if (string.IsNullOrEmpty(value) || value == "{YOUR_MICROSOFT_APPLICATION_SECRET}")
               throw new ArgumentException("The clientSecret argument for the onedrive client must be set");
            _ClientSecret = value;
         }
      }

      string _RedirectUri;
      public string RedirectUri
      {
         get => _RedirectUri;
         private set
         {
            if (string.IsNullOrEmpty(value) || value == "msal{YOUR_MICROSOFT_APPLICATION_ID}://auth")
               throw new ArgumentException("The redirectUri argument for the onedrive client must be set");
            _RedirectUri = value;
         }
      }

      string[] _Scopes;
      public string[] Scopes
      {
         get => _Scopes;
         private set
         {
            if (value == null || value.Where(scope => !string.IsNullOrEmpty(scope)).Count() == 0)
               throw new ArgumentException("The scopes argument for the onedrive client must be set");
            _Scopes = value;
         }
      }

   }
}
