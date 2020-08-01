using System;

namespace Xamarin.CloudDrive.Connector
{
   public partial class OneDriveService : ICloudDriveService
   {

      public readonly IOneDriveClient Client;

      internal OneDriveService(IOneDriveClient client)
      {
         if (client == null)
            throw new ArgumentException("The client argument for the OneDrive service must be set");
         this.Client = client;
      }

      (string DriveID, string ID) GetIDs(string itemID)
      {
         var directoryParts = itemID.Split(new string[] { "!" }, StringSplitOptions.RemoveEmptyEntries);
         if (directoryParts?.Length != 2) throw new Exception("The directory ID for the onedrive client is invalid");

         var driveID = (string)directoryParts.GetValue(0);
         var ID = (string)directoryParts.GetValue(1);
         if (ID != "root")
            ID = itemID;

         return (DriveID: driveID, ID: ID);
      }

      string GetPath(string path)
      {
         if (string.IsNullOrEmpty(path)) return "";

         var fullPath = Uri.UnescapeDataString(path);

         var splitIndex = fullPath.IndexOf(":");
         if (splitIndex != -1 && (fullPath.StartsWith($"/drives/") || fullPath.StartsWith($"/drive/")))
            fullPath = fullPath.Substring(splitIndex + 1);

         return fullPath;
      }

   }
}