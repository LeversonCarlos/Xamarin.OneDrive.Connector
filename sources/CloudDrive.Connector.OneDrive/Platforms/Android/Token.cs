namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveToken
   {

      public OneDriveToken() :
         this(Dependency.GetService<IOneDriveIdentity>())
      { }

   }
}
