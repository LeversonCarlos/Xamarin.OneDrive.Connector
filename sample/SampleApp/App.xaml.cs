using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SampleApp
{
   public partial class App : Application
   {

      public App()
      {
         InitializeComponent();
         OneDrive = new Xamarin.OneDrive.Connector(new Xamarin.OneDrive.Configs
         {
            ClientID = "YOUR_MICROSOFT_APPLICATION_ID",
            Scopes = new string[] { "User.Read", "Files.Read" }
         });
         MainPage = new MainPage();
      }
      internal static Xamarin.OneDrive.Connector OneDrive;

      protected override void OnStart()
      {
         // Handle when your app starts
      }

      protected override void OnSleep()
      {
         // Handle when your app sleeps
      }

      protected override void OnResume()
      {
         // Handle when your app resumes
      }

   }
}