using System.Runtime.Serialization;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   namespace DTOs
   {

      internal class FileSearch
      {
         public File[] value { get; set; }

         [DataMember(Name = "@odata.nextLink")]
         public string nextLink { get; set; }
      }

      internal class File
      {
         public string id { get; set; }
         public string name { get; set; }
         public string createdDateTime { get; set; }
         public long? size { get; set; }
         public FileDetails file { get; set; }
         public DirectoryParent parentReference { get; set; }

         [DataMember(Name = "@microsoft.graph.downloadUrl")]
         public string downloadUrl { get; set; }
      }

      internal class FileDetails
      {
         public string mimeType { get; set; }
      }

   }
}