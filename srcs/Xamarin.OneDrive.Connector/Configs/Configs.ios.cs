using Microsoft.Identity.Client;

namespace Xamarin.OneDrive
{
   partial class Connector
   {

      public static void SetAuthenticationContinuationEventArgs(Foundation.NSUrl url)
      {
         AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
      }

   }
}