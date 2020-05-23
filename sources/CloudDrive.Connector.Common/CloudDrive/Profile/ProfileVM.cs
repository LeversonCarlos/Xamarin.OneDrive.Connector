using System.Collections.Generic;

namespace Xamarin.CloudDrive.Connector.Common
{
   public class ProfileVM
   {
      public string ID { get; set; }
      public string Description { get; set; }
      public byte[] ProfilePicture { get; set; }
      public Dictionary<string, string> KeyValues { get; set; }
   }
}