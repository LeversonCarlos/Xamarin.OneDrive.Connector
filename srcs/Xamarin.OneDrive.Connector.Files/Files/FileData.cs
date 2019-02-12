using System;
using System.Runtime.Serialization;

namespace Xamarin.OneDrive.Files
{

   [DataContract]
   public class FileData
   {

      [DataMember(Name = "id")]
      public string id { get; set; }

      [DataMember(Name = "name")]
      public string FileName { get; set; }

      [DataMember(Name = "path")]
      public string FilePath { get; set; }

      [DataMember(Name = "createdDateTime")]
      internal string CreatedDateTimeText { get; set; }
      public DateTime? CreatedDateTime { get; set; }

      [DataMember(Name = "size")]
      public long? Size { get; set; }

      [DataMember(Name = "@microsoft.graph.downloadUrl")]
      internal string downloadUrl { get; set; }

      [DataMember(Name = "parentReference")]
      internal FileData parentReference { get; set; }
      public string parentID { get; set; }

      [DataMember(Name = "folder")]
      internal FolderData folderData { get; set; }

   }

   [DataContract]
   internal class FolderData
   {

      [DataMember(Name = "childCount")]
      internal int childCount { get; set; }

   }

}