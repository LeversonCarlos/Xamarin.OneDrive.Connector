using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveIdentity
   {

      public Task<AuthenticationResult> AcquireTokenFromIdentity() =>
         (_Identity as IConfidentialClientApplication)
            ?.AcquireTokenForClient(_Scopes)
            ?.ExecuteAsync();

   }
}
