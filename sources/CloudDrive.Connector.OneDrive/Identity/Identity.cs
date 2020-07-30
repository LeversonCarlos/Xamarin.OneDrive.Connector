using Microsoft.Identity.Client;
using System;

namespace Xamarin.CloudDrive.Connector
{
   internal partial class OneDriveIdentity : IOneDriveIdentity
   {

      string[] _Scopes;
      IClientApplicationBase _Identity;

      // tenants { common, organizations, consumers }
      const string Authority = "https://login.microsoftonline.com/{tenant}";
      static Uri _GetAuthorityUri() => new Uri(Authority.Replace("{tenant}", "common"));

   }
}
