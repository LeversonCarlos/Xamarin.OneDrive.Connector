using System;

namespace Xamarin.CloudDrive.Connector
{

   partial class OneDriveClient
   {

      public OneDriveClient() :
         this(Dependency.GetService<IOneDriveToken>())
      { }

   }

}
