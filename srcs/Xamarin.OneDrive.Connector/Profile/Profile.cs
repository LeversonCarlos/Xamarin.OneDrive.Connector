using System.Runtime.Serialization;

namespace Xamarin.OneDrive
{

   [DataContract]
   public class Profile
   {

      [DataMember(Name = "id")]
      public string id { get; set; }

      [DataMember(Name = "displayName")]
      public string Name { get; set; }

      [DataMember(Name = "mail")]
      public string Mail { get; set; }

   }
}