using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.OneDrive.Profile;
using Xamarin.OneDrive.Files;

namespace SampleApp
{
   public partial class MainPage : ContentPage
   {

      public MainPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         this.InfoLabel.Text = "Click to authorize access to your OneDrive account";
         this.ConnectButton.Text = "Connect";
         this.ConnectButton.IsVisible = true;
         this.DisconnectButton.Text = "Connect";
         this.DisconnectButton.IsVisible = false;
         this.FilesButton.IsEnabled = false;
         // this.ImageCover.IsVisible = false;
         // this.FilesList.IsVisible = false;
      }

      async void ConnectButton_Clicked(object sender, EventArgs e)
      {
         try
         {
            this.ConnectButton.IsEnabled = false;
            if (!await App.OneDrive.ConnectAsync()) { return; }
            var profile = await App.OneDrive.GetProfileAsync();
            if (profile == null) { return; }
            this.InfoLabel.Text = $"Connected to {profile.Name} account through address {profile.Mail}";
            this.ConnectButton.IsVisible = false;
            this.DisconnectButton.IsVisible = true;
            this.FilesButton.IsEnabled = true;
         }
         catch (Exception ex) { this.InfoLabel.Text = $"Exception: {ex.ToString()}"; }
         finally { this.ConnectButton.IsEnabled = true; }
      }

      async void DisconnectButton_Clicked(object sender, EventArgs e)
      {
         try
         {
            this.DisconnectButton.IsEnabled = false;
            await App.OneDrive.DisconnectAsync();
            this.InfoLabel.Text = "Click to authorize access to your OneDrive account";
            this.ConnectButton.IsVisible = true;
            this.DisconnectButton.IsVisible = false;
            this.FilesButton.IsEnabled = false;
         }
         catch (Exception ex) { this.InfoLabel.Text = $"Exception: {ex.ToString()}"; }
         finally { this.DisconnectButton.IsEnabled = true; }
      }

      async void FilesButton_Clicked(object sender, EventArgs e)
      {
         try
         {
            this.FilesButton.IsEnabled = false;
            var fileList = await App.OneDrive.SearchFilesAsync("*.cbz");
            this.FileList.IsVisible = true;
            FileList.ItemsSource = fileList;
         }
         catch (Exception ex) { this.InfoLabel.Text = $"Exception: {ex.ToString()}"; }
         finally { this.FilesButton.IsEnabled = true; }
      }

   }
}
