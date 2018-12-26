namespace Xamarin.OneDrive
{
   internal class DependencyImplementation : IDependency
   {

      public void Initialize(Configs configs)
      {
         var mainActivity = Xamarin.Forms.Forms.Context as Forms.Platform.Android.FormsAppCompatActivity;
         configs.UiParent = new Microsoft.Identity.Client.UIParent(mainActivity);
         configs.RedirectUri = $"msal{configs.ClientID}://auth";
      }

   }
}