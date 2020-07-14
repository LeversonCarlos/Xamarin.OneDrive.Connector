using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Xamarin.CloudDrive.Connector
{
   namespace DTOs
   {

      internal class SharedDriveSearch
      {
         public SharedDrive[] value { get; set; }

         [JsonPropertyName("@odata.nextLink")]
         public string nextLink { get; set; }
      }

      internal class SharedDrive
      {
         public SharedDriveDetails remoteItem { get; set; }
      }

      internal class SharedDriveDetails
      {
         public string id { get; set; }
         public string name { get; set; }
         public SharedDriveDetailsShared shared { get; set; }
      }

      internal class SharedDriveDetailsShared
      {
         public SharedDriveDetailsSharedOwner owner { get; set; }
      }

      internal class SharedDriveDetailsSharedOwner
      {
         public DTOs.Profile user { get; set; }
      }

   }
}