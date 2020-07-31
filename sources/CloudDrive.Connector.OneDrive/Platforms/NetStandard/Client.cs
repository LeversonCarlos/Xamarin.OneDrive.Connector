using System;

namespace Xamarin.CloudDrive.Connector
{

   partial class OneDriveClient
   {

      public OneDriveClient(IServiceProvider serviceProvider) :
         this(Dependency.GetService<OneDriveToken>(serviceProvider))
      { }

   }

   partial class OneDriveClientHandler
   {
      public OneDriveClientHandler(IServiceProvider serviceProvider) :
         this(Dependency.GetService<OneDriveToken>(serviceProvider))
      { }
   }

}
