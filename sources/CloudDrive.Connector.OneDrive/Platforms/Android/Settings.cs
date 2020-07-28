namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveSettings
   {

      internal OneDriveSettings() { }

      internal void Init(string clientID, string redirectUri, string[] scopes)
      {
         ClientID = clientID;
         RedirectUri = redirectUri;
         Scopes = scopes;
      }

   }
}
