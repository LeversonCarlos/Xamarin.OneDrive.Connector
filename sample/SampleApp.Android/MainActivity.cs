using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace SampleApp.Droid
{
   [Activity(Label = "SampleApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
   {

      protected override void OnCreate(Bundle savedInstanceState)
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate(savedInstanceState);
         global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
         LoadApplication(new App());
      }

      protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
      {
         base.OnActivityResult(requestCode, resultCode, data);
         Xamarin.OneDrive.Connector.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
      }

   }
}