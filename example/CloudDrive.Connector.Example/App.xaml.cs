using Xamarin.Forms;

namespace Xamarin.CloudDrive.Connector.Example
{
   public partial class App : Application
   {
      public App()
      {
         InitializeComponent();
         MainPage = new MainPage { BindingContext = new MainVM() };
      }

      protected override void OnStart()
      {
      }

      protected override void OnSleep()
      {
      }

      protected override void OnResume()
      {
      }
   }
}
