using System;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public partial class OneDriveService : ICloudDriveService
   {

      readonly IClient Client;

      public OneDriveService(IClient client)
      {
         if (client == null) throw new ArgumentException("The client argument for the OneDrive service must be set");
         this.Client = client;
      }

   }
}