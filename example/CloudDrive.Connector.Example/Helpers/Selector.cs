namespace Xamarin.CloudDrive.Connector.Example.Helpers
{

   public class Selector
   {
   }

   public class SelectorItem
   {
      public string ID { get; set; }
      public string Name { get; set; }
      public string Path { get; set; }
      public SelectorItem Parent { get; set; }
      public enSelectorItemType Type { get; set; }
   }

   public enum enSelectorItemType : short { Drive, Folder, File }

}
