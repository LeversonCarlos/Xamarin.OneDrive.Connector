using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.OneDrive.Profile;
using Xamarin.OneDrive.Files;
using System.Linq;

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
         this.DisconnectButton.Text = "Disconnect";
         this.DisconnectButton.IsVisible = false;
         this.FilesButton.IsVisible = false;
         // this.ImageCover.IsVisible = false;
         // this.FilesList.IsVisible = false;
      }

      async void ConnectButton_Clicked(object sender, EventArgs e)
      {
         try
         {
            this.Disable();
            if (!await App.OneDrive.ConnectAsync()) { return; }
            var profile = await App.OneDrive.GetProfileAsync();
            if (profile == null) { return; }
            this.InfoLabel.Text = $"Connected to {profile.Name} account through {profile.Mail}.";
            this.ConnectButton.IsVisible = false;
            this.DisconnectButton.IsVisible = true;
            this.FilesButton.IsVisible = true;
         }
         catch (Exception ex) { this.InfoLabel.Text = $"Exception: {ex.ToString()}"; }
         finally { this.Enable(); }
      }

      async void DisconnectButton_Clicked(object sender, EventArgs e)
      {
         try
         {
            this.Disable();
            await App.OneDrive.DisconnectAsync();
            this.InfoLabel.Text = "Click to authorize access to your OneDrive account";
            this.ConnectButton.IsVisible = true;
            this.DisconnectButton.IsVisible = false;
            this.FilesButton.IsVisible = false;
         }
         catch (Exception ex) { this.InfoLabel.Text = $"Exception: {ex.ToString()}"; }
         finally { this.Enable(); }
      }

      async void FilesButton_Clicked(object sender, EventArgs e)
      {
         try
         {
            this.Disable();
            var fileList = await App.OneDrive.SearchFilesAsync("*.cbz");
            this.FileList.IsVisible = true;
            FileList.ItemsSource = fileList;
         }
         catch (Exception ex) { this.InfoLabel.Text = $"Exception: {ex.ToString()}"; }
         finally { this.Enable(); }
      }

      async void ImageCell_Tapped(object sender, EventArgs e)
      {
         try
         {
            this.Disable();

            var imageCell = sender as TextCell;
            var file = imageCell.BindingContext as Xamarin.OneDrive.Files.FileData;
            var downloadUrl = await App.OneDrive.GetDownloadUrlAsync(file);
           
            using (var zipStream = new System.IO.Compression.HttpZipStream(downloadUrl))
            {
               if (file.Size.HasValue && file.Size.Value > 0)
               { zipStream.SetContentLength(file.Size.Value); }

               var entryList = await zipStream.GetEntriesAsync();

               var entry = entryList
                  .Where(x =>
                     x.FileName.ToLower().EndsWith(".jpg") ||
                     x.FileName.ToLower().EndsWith(".jpeg") ||
                     x.FileName.ToLower().EndsWith(".png"))
                  .OrderBy(x => x.FileName)
                  .FirstOrDefault();

               await zipStream.ExtractAsync(entry, (entryStream) => {
                  this.ImageCover.Source = ImageSource.FromStream(() => { return entryStream; });
               });

            }

         }
         catch (Exception ex) { this.InfoLabel.Text = $"Exception: {ex.ToString()}"; }
         finally { this.Enable(); }
      }

      void Enable()
      {
         this.ConnectButton.IsEnabled = true;
         this.DisconnectButton.IsEnabled = true;
         this.FilesButton.IsEnabled = true;
         this.ActivityIndicator.IsEnabled = false;
         this.ActivityIndicator.IsRunning = false;
      }

      void Disable()
      {
         this.ConnectButton.IsEnabled = false;
         this.DisconnectButton.IsEnabled = false;
         this.FilesButton.IsEnabled = false;
         this.ActivityIndicator.IsEnabled = true;
         this.ActivityIndicator.IsRunning = true;
      }

   }
}
