using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xamarin.OneDrive.Files
{

   [DataContract]
   internal class SearchData
   {

      [DataMember(Name = "value")]
      public List<FileData> Files { get; set; }

      [DataMember(Name = "@odata.nextLink")]
      public string nextLink { get; set; }

   }

}