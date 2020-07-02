using System;

namespace Xamarin.CloudDrive.Connector
{
   public partial class OneDriveService : ICloudDriveService
   {

      readonly IOneDriveClient Client;

      public OneDriveService(IOneDriveClient client)
      {
         if (client == null) throw new ArgumentException("The client argument for the OneDrive service must be set");
         this.Client = client;
      }

   }
}