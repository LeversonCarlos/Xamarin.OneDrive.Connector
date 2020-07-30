using Microsoft.Identity.Client;

namespace Xamarin.CloudDrive.Connector
{
   internal partial class OneDriveIdentity : IOneDriveIdentity
   {

      string[] _Scopes;
      IClientApplicationBase _Identity;

   }
}
