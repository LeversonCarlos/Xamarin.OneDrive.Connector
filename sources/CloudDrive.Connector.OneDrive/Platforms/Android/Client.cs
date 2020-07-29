using System;

namespace Xamarin.CloudDrive.Connector
{

   partial class OneDriveClient
   {

      public OneDriveClient() :
         base(new OneDriveClientHandler())
      {
         Token = Dependency.GetService<OneDriveToken>();
         if (Token == null)
            throw new ArgumentException("The token argument for the http client must be set");
         BaseAddress = new Uri(BaseURL);
      }

   }

   partial class OneDriveClientHandler
   {
      public OneDriveClientHandler() :
         this(Dependency.GetService<OneDriveToken>())
      { }
   }

}
