using System.Runtime.Serialization;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   namespace DTOs
   {

      internal class DirectorySearch
      {
         public Directory[] value { get; set; }

         [DataMember(Name = "@odata.nextLink")]
         public string nextLink { get; set; }
      }

      internal class Directory
      {
         public string id { get; set; }
         public string name { get; set; }
         public DirectoryDetails folder { get; set; }
         public DirectoryParent parentReference { get; set; }
      }

      internal class DirectoryDetails
      {
         public int childCount { get; set; }
      }

      internal class DirectoryParent
      {
         public string id { get; set; }
         public string path { get; set; }
         public string driveId { get; set; }
      }

   }
}