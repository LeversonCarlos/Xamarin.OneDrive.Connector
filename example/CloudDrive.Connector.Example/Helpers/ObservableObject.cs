using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xamarin.CloudDrive.Connector.Example.Helpers
{
   public class ObservableObject : INotifyPropertyChanged
   {

      string _Title = string.Empty;
      public string Title
      {
         get { return this._Title; }
         set { this.SetProperty(ref this._Title, value); }
      }

      bool _IsBusy = false;
      public bool IsBusy
      {
         get { return this._IsBusy; }
         set { this.SetProperty(ref this._IsBusy, value); }
      }


      protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
      {
         if (EqualityComparer<T>.Default.Equals(backingStore, value))
         { return false; }

         backingStore = value;
         onChanged?.Invoke();
         OnPropertyChanged(propertyName);
         return true;
      }

      public event PropertyChangedEventHandler PropertyChanged;
      protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
      {
         var changed = PropertyChanged;
         if (changed == null)
            return;

         changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

   }
}
