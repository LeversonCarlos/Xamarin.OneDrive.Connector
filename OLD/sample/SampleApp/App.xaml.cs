﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SampleApp
{
   public partial class App : Application
   {

      public App()
      {
         InitializeComponent();
         OneDrive = new Xamarin.OneDrive.Connector("YOUR_MICROSOFT_APPLICATION_ID", "User.Read", "Files.ReadWrite");
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