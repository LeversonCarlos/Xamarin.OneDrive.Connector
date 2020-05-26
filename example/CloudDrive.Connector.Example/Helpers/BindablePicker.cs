using System;
using System.Collections;
using Xamarin.Forms;

namespace Xamarin.CloudDrive.Connector.Example.Helpers
{
   public class BindablePicker : Picker
   {

      public BindablePicker()
      {
         this.SelectedIndexChanged += this.OnSelectedIndexChanged;
      }

      public static new readonly BindableProperty ItemsSourceProperty =
         BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(BindablePicker), default(IEnumerable),
         propertyChanged: OnItemsSourceChanged, defaultBindingMode: BindingMode.TwoWay);
      public new IEnumerable ItemsSource
      {
         get { return (IEnumerable)GetValue(ItemsSourceProperty); }
         set { SetValue(ItemsSourceProperty, value); }
      }
      private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
      {
         var picker = bindable as BindablePicker;
         picker.Items.Clear();
         if (newvalue != null)
         {
            var optionValues = newvalue as IEnumerable;
            foreach (var item in optionValues) { picker.Items.Add(item.ToString()); }
         }
      }

      public static new readonly BindableProperty SelectedItemProperty =
         BindableProperty.Create("SelectedItem", typeof(string), typeof(BindablePicker), default(string),
         propertyChanged: OnSelectedItemChanged, defaultBindingMode: BindingMode.TwoWay);
      public new string SelectedItem
      {
         get { return (string)GetValue(SelectedItemProperty); }
         set { SetValue(SelectedItemProperty, value); }
      }
      private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
      {
         var picker = bindable as BindablePicker;
         if (newvalue != null)
         {
            picker.SelectedIndex = picker.Items.IndexOf(newvalue.ToString());
         }
      }

      private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
      {
         if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
         { SelectedItem = null; }
         else { SelectedItem = Items[SelectedIndex]; }
      }

   }
}
