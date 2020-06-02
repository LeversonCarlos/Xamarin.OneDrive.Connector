using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Example.Helpers;
using Xamarin.Forms;

namespace Xamarin.CloudDrive.Connector.Example
{
   public class MainVM : ObservableObject
   {
      Common.ICloudDriveService DriveService;

      public MainVM()
      {
         this.Title = "CloudDrive Connector Example";
         this.IsConnected = false;
         this.ConnectionText = "Connect Account";
         this.ConnectionColor = Color.Green;
         this.ConnectionCommand = new Command(async () => await this.Connection());
         this.SelectFileCommand = new Command(async () => await this.SelectFile());
      }

      string _SelectedCloundDrive;
      public string SelectedCloundDrive
      {
         get { return _SelectedCloundDrive; }
         set { this.SetProperty(ref _SelectedCloundDrive, value); this.Connection_Disconnect(); }
      }
      public List<string> CloundDriveList { get; set; } = new List<string> { "LocalDrive", "OneDrive" };

      string _ConnectionText;
      public string ConnectionText
      {
         get { return this._ConnectionText; }
         set { this.SetProperty(ref this._ConnectionText, value); }
      }

      Color _ConnectionColor;
      public Color ConnectionColor
      {
         get { return this._ConnectionColor; }
         set { this.SetProperty(ref this._ConnectionColor, value); }
      }

      bool _IsConnected;
      public bool IsConnected
      {
         get { return this._IsConnected; }
         set { this.SetProperty(ref this._IsConnected, value); }
      }

      SelectorItem _CurrentItem;
      public SelectorItem CurrentItem
      {
         get { return this._CurrentItem; }
         set { this.SetProperty(ref this._CurrentItem, value); }
      }

      ImageSource _CurrentItemImage;
      public ImageSource CurrentItemImage
      {
         get { return this._CurrentItemImage; }
         set { this.SetProperty(ref this._CurrentItemImage, value); }
      }

      public Command ConnectionCommand { get; set; }
      async Task Connection()
      {
         try
         {
            this.IsBusy = true;
            if (!this.IsConnected) { await this.Connection_Connect(); }
            else { await this.Connection_Disconnect(); }
         }
         catch (Exception ex) { await this.DisplayAlert(ex.ToString()); }
         finally { this.IsBusy = false; }
      }

      async Task Connection_Connect()
      {

         if (string.IsNullOrEmpty(this.SelectedCloundDrive)) { await this.DisplayAlert("Select the drive implementation first"); return; }
         switch (this.SelectedCloundDrive)
         {
            case "LocalDrive":
               this.DriveService = Common.DependencyProvider.Get<LocalDrive.LocalDriveService>(); break;
            case "OneDrive":
               this.DriveService = Common.DependencyProvider.Get<OneDrive.OneDriveService>(); break;
            default:
               await this.DisplayAlert("Select the drive implementation first"); return;
         }

         if (!await this.DriveService.ConnectAsync()) { return; }
         await this.Connection_Connected();
      }

      async Task Connection_Connected()
      {
         var profile = await this.DriveService.GetProfile();
         if (profile == null) { return; }
         // this.ProfilePicture.Source = ImageSource.FromStream(() => new MemoryStream(profile.ProfilePicture));
         this.ConnectionText = $"Disconnect from {profile.Description}";
         this.ConnectionColor = Color.Red;
         this.IsConnected = true;
      }

      async Task Connection_Disconnect()
      {
         await this.DriveService.DisconnectAsync();
         this.ConnectionText = "Connect Account";
         this.ConnectionColor = Color.Green;
         this.CurrentItem = null;
         this.CurrentItemImage = null;
         this.IsConnected = false;
      }

      public Command SelectFileCommand { get; set; }
      async Task SelectFile()
      {
         try
         {
            this.IsBusy = true;
            this.CurrentItem = await Selector.GetFolder(this.DriveService);
            if (this.CurrentItem == null) return;

            var isImage = this.CurrentItem.Path.ToLower().EndsWith(".jpg") || this.CurrentItem.Path.ToLower().EndsWith(".jpeg") || this.CurrentItem.Path.ToLower().EndsWith(".png");
            if (!isImage) { await this.DisplayAlert("Please, select an image file"); return; }

            var imageStream = await this.DriveService.Download(this.CurrentItem.ID);
            this.CurrentItemImage = ImageSource.FromStream(() => imageStream);

         }
         catch (Exception ex) { await this.DisplayAlert(ex.ToString()); }
         finally { this.IsBusy = false; }
      }

      Task DisplayAlert(string message) => (App.Current.MainPage).DisplayAlert(this.Title, message, "Ok");

   }
}
