using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace Xamarin.OneDrive
{
   partial class Connector
   {

      /* 
      internal Context _activity;
      internal Bundle _bundle;
      public static void Init(Context activity, Bundle bundle) {
         this._activity=activity;
         this._bundle=bundle;
      }
      */

      public static void SetAuthenticationContinuationEventArgs(int requestCode, Result resultCode, Intent data)
      {
         AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
      }

   }
}