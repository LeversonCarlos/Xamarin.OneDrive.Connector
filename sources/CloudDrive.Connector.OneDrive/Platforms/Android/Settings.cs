using Android.App;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveSettings
   {

      internal OneDriveSettings() { }

      internal void Init(Activity activity, string clientID, string redirectUri, string[] scopes)
      {
         Activity = activity;
         ClientID = clientID;
         RedirectUri = redirectUri;
         Scopes = scopes;
      }

      public Activity Activity { get; private set; }

   }
}
