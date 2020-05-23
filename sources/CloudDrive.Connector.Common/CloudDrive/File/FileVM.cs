using System;
using System.Collections.Generic;

namespace Xamarin.CloudDrive.Connector.Common
{
   public class FileVM 
   {
      public string ID { get; set; }
      public string Name { get; set; }
      public string Path { get; set; }
      public DateTime? CreatedDateTime { get; set; }
      public long? SizeInBytes { get; set; }
      public string ParentID { get; set; }
      public Dictionary<string, string> KeyValues { get; set; }
   }
}
