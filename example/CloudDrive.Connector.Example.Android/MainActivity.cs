using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

namespace Xamarin.CloudDrive.Connector.Example.Droid
{

   [Activity(Label = "Xamarin.CloudDrive.Connector.Example", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate(savedInstanceState);

         Xamarin.Essentials.Platform.Init(this, savedInstanceState);
         Xamarin.CloudDrive.Connector.LocalDriveService.Init(this, savedInstanceState);
         Xamarin.CloudDrive.Connector.OneDriveService.Init(this, "{YOUR_MICROSOFT_APPLICATION_ID}", "msal{YOUR_MICROSOFT_APPLICATION_ID}://auth", "User.Read", "Files.Read.All");
         Xamarin.Forms.Forms.Init(this, savedInstanceState);
         LoadApplication(new App());
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
      {
         Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
         base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }

      protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
      {
         base.OnActivityResult(requestCode, resultCode, data);
         Xamarin.CloudDrive.Connector.OneDriveService.SetAuthenticationResult(requestCode, resultCode, data);
      }

   }
}