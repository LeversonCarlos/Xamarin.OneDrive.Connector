using System;

namespace Xamarin.CloudDrive.Connector
{

   partial class OneDriveClient
   {

      public OneDriveClient(IServiceProvider serviceProvider) :
         this(Dependency.GetService<IOneDriveToken>(serviceProvider))
      { }

   }

}
