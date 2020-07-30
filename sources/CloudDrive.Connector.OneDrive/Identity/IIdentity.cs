using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal interface IOneDriveIdentity
   {

      Task<AuthenticationResult> AcquireTokenFromIdentity();

   }
}
