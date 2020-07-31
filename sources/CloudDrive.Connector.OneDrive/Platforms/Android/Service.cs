namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public OneDriveService() :
         this(Dependency.GetService<IOneDriveClient>())
      { }

   }
}
