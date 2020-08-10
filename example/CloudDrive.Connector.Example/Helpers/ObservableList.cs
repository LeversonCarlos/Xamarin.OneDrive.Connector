using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Xamarin.CloudDrive.Connector.Example.Helpers
{
   public class ObservableList<T> : ObservableCollection<T>, INotifyCollectionChanged
   {

      public ObservableList() : base() { }
      public ObservableList(IEnumerable<T> collection) : base(collection) { }

      public void AddRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add)
      {
         try
         {
            if (collection == null) throw new ArgumentNullException("collection");
            this.CheckReentrancy();

            if (notificationMode == NotifyCollectionChangedAction.Reset)
            {
               foreach (var i in collection) { Items.Add(i); }
               this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
               this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
               this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
               return;
            }

            int startIndex = Count;
            var changedItems = collection is List<T> ? (List<T>)collection : new List<T>(collection);
            foreach (var i in changedItems) { Items.Add(i); }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItems, startIndex));
         }
         catch { }
      }

      public void RemoveRange(IEnumerable<T> collection)
      {
         try
         {
            if (collection == null) throw new ArgumentNullException("collection");
            foreach (var i in collection) { Items.Remove(i); }
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
         }
         catch { }
      }

      public void Replace(T item)
      {
         try
         {
            var index = this.Items.IndexOf(item);
            if (index == -1)
            {
               this.Items.Add(item);
               this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }
            else
            {
               var oldItem = this.Items[index];
               this.SetItem(index, item);
               this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, oldItem, index));
            }
         }
         catch { }
      }
      public void ReplaceRange(IEnumerable<T> collection)
      {
         if (collection == null) throw new ArgumentNullException("collection");
         this.Items.Clear();
         this.AddRange(collection, NotifyCollectionChangedAction.Reset);
      }

   }
}
