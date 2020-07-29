namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveSettings
   {

      public OneDriveSettings(string clientID, string clientSecret, string redirectUri, string[] scopes)
      {
         ClientID = clientID;
         ClientSecret = clientSecret;
         RedirectUri = redirectUri;
         Scopes = scopes;
      }

   }
}
