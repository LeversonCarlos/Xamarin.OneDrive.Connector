using System.Collections.Generic;

namespace Xamarin.CloudDrive.Connector
{
   public class DirectoryVM 
   {
      public string ID { get; set; }
      public string Name { get; set; }
      public string Path { get; set; }
      public string ParentID { get; set; }
      public Dictionary<string, string> KeyValues { get; set; }
   }
}
