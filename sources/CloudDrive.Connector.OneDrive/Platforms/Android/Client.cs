using System;

namespace Xamarin.CloudDrive.Connector
{

   partial class OneDriveClient
   {

      public OneDriveClient() :
         this(Dependency.GetService<OneDriveToken>())
      { }

   }

   partial class OneDriveClientHandler
   {
      public OneDriveClientHandler() :
         this(Dependency.GetService<OneDriveToken>())
      { }
   }

}
