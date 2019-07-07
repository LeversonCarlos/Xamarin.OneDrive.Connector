using Android.App;
using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Xamarin.OneDrive
{
   internal class DependencyImplementation : IDependency
   {

      public void Initialize(Configs configs)
      {
         var mainActivity = Xamarin.Forms.Forms.Context as Forms.Platform.Android.FormsAppCompatActivity;
         configs.UiParent = mainActivity;
         configs.RedirectUri = $"msal{configs.ClientID}://auth";
      }

      public async Task<AuthenticationResult> GetAuthResult(IPublicClientApplication client, Configs configs)
      {
         try
         {
            return await client
               .AcquireTokenInteractive(configs.Scopes)
               .WithUseEmbeddedWebView(true)
               .WithParentActivityOrWindow((Activity)configs.UiParent)
               .ExecuteAsync();
         }
         catch (Exception) { throw; }
      }

   }
}