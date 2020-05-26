using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Example.Helpers;

namespace Xamarin.CloudDrive.Connector.Example
{
   public class MainVM : ObservableObject
   {

      public MainVM()
      {
         this.Title = "CloudDrive Connector Example";
      }

      string _SelectedCloundDrive;
      public string SelectedCloundDrive
      {
         get { return _SelectedCloundDrive; }
         set { this.SetProperty(ref _SelectedCloundDrive, value); this.ServiceDisconnect(); }
      }
      public List<string> CloundDriveList { get; set; } = new List<string> { "LocalDrive", "OneDrive" };

      async Task ServiceDisconnect()
      {
         /* TODO */
      }

   }
}
