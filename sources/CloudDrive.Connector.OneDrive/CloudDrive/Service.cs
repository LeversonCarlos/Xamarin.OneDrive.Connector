using System;

namespace Xamarin.CloudDrive.Connector
{
   public partial class OneDriveService : ICloudDriveService
   {

      internal OneDriveService(IOneDriveClient client)
      {
         Client = client;
      }

      IOneDriveClient _Client;
      public IOneDriveClient Client
      {
         get => _Client;
         private set
         {
            if (value == null)
               throw new ArgumentException("The client argument for the OneDrive service must be set");
            _Client = value;
         }
      }

      internal (string DriveID, string ID) GetIDs(string itemID)
      {
         if (string.IsNullOrEmpty(itemID))
            throw new ArgumentException("The directory ID for the onedrive client is invalid");

         var directoryParts = itemID.Split(new string[] { "!" }, StringSplitOptions.RemoveEmptyEntries);
         if (directoryParts?.Length != 2)
            throw new ArgumentException("The directory ID for the onedrive client is invalid");

         var driveID = (string)directoryParts.GetValue(0);
         var ID = (string)directoryParts.GetValue(1);
         if (ID != "root")
            ID = itemID;

         return (DriveID: driveID, ID: ID);
      }

      internal string GetPath(string path)
      {
         if (string.IsNullOrEmpty(path)) 
            return "";

         var fullPath = Uri.UnescapeDataString(path);

         var splitIndex = fullPath.IndexOf(":");
         if (splitIndex != -1 && (fullPath.StartsWith("/drives/") || fullPath.StartsWith("/drive/")))
            fullPath = fullPath.Substring(splitIndex + 1);

         return fullPath;
      }

   }
}