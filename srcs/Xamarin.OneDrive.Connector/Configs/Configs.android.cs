using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace Xamarin.OneDrive
{
   partial class Connector
   {

      public static void SetAuthenticationContinuationEventArgs(int requestCode, Result resultCode, Intent data)
      {
         AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
      }

   }
}