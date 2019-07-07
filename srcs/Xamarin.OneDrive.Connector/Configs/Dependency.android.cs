using Android.App;
using Android.Content;
using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{

   internal class DependencyImplementation : IDependency
   {
      internal Context _activity;
      internal string _redirectUrl;

      public void Initialize(Configs configs)
      {
         if (_activity != null) {
            configs.UiParent = _activity;
         }
         else {
            var mainActivity = Xamarin.Forms.Forms.Context as Forms.Platform.Android.FormsAppCompatActivity;
            configs.UiParent = mainActivity;
         }
         if (!string.IsNullOrEmpty(_redirectUrl)) {
            configs.RedirectUri = _redirectUrl;
         }
         else {
            configs.RedirectUri = $"msal{configs.ClientID}://auth";
         }
      }

      public async Task<AuthenticationResult> GetAuthResult(IPublicClientApplication client, Configs configs)
      {
         try
         {
            return await client
               .AcquireTokenInteractive(configs.Scopes)
               .WithParentActivityOrWindow((Activity)configs.UiParent)
               .ExecuteAsync();
         }
         catch (Exception) { throw; }
      }

   }

   partial class Connector
   {

      public static void Init(Context activity) { Init(activity, "");  }

      public static void Init(Context activity, string redirectUrl) {
         var dependency= (DependencyImplementation)Dependency.Current;
         dependency._activity = activity;
         dependency._redirectUrl = redirectUrl;
      }

   }

}