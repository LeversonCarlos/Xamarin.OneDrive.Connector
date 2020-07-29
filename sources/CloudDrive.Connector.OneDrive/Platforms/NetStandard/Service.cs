using System;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public OneDriveService(IServiceProvider serviceProvider) :
         this(Dependency.GetService<OneDriveClient>(serviceProvider))
      { }

   }
}
