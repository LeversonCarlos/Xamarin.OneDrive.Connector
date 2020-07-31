using System;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      public OneDriveToken(IServiceProvider serviceProvider) :
         this(Dependency.GetService<IOneDriveIdentity>(serviceProvider))
      { }

   }
}
