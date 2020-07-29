using System;

namespace Xamarin.CloudDrive.Connector
{

   partial class OneDriveClient
   {

      public OneDriveClient(IServiceProvider serviceProvider) :
         base(new OneDriveClientHandler(serviceProvider))
      {
         Token = Dependency.GetService<OneDriveToken>(serviceProvider);
         if (Token == null)
            throw new ArgumentException("The token argument for the http client must be set");
         BaseAddress = new Uri(BaseURL);
      }

   }

   partial class OneDriveClientHandler
   {
      public OneDriveClientHandler(IServiceProvider serviceProvider) :
         this(Dependency.GetService<OneDriveToken>(serviceProvider))
      { }
   }

}
