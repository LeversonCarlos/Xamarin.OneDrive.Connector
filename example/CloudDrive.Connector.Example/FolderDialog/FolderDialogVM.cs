using System;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Example.Helpers;
using Xamarin.Forms;

namespace Xamarin.CloudDrive.Connector.Example.FolderDialog
{
   public class FolderDialogVM : ObservableObject
   {

      private readonly TaskCompletionSource<SelectorItem> tcs;
      public FolderDialogVM()
      {
         this.Title = "Select an Image File";
         this.Data = new ObservableList<SelectorItem>();
         this.ConfirmCommand = new Command(async () => await this.Confirm());
         this.CancelCommand = new Command(async () => await this.Cancel());
         this.ItemSelectCommand = new Command(async (item) => await this.ItemSelect(item));
         this.tcs = new TaskCompletionSource<SelectorItem>();
      }

      public ObservableList<SelectorItem> Data { get; private set; }
      public SelectorItem SelectedItem { get; set; }

      SelectorItem _CurrentItem;
      public SelectorItem CurrentItem
      {
         get { return this._CurrentItem; }
         set { this.SetProperty(ref this._CurrentItem, value); }
      }

      public EventHandler<SelectorItem> OnItemSelected;
      public Command ItemSelectCommand { get; set; }
      internal async Task ItemSelect(object item)
      {
         this.OnItemSelected?.Invoke(this, this.SelectedItem);
         await Task.CompletedTask;
      }

      public Command ConfirmCommand { get; set; }
      async Task Confirm()
      {
         await this.ClosePage();
         tcs.SetResult(this.CurrentItem);
      }

      public Command CancelCommand { get; set; }
      async Task Cancel()
      {
         await this.ClosePage();
         tcs.SetResult(null);
      }

      public async Task<SelectorItem> OpenPage()
      {
         var view = new FolderDialogPage { BindingContext = this };
         await Navigation().PushModalAsync(view, true);
         return await this.tcs.Task;
      }

      async Task ClosePage()
      {
         this.IsBusy = true;
         await Navigation().PopModalAsync(true);
         this.IsBusy = false;
      }

      public INavigation Navigation()
      {
         var mainPage = Application.Current.MainPage as MainPage;
         return mainPage.Navigation;
      }

   }
}
