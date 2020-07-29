using Microsoft.Identity.Client;
using System;

namespace Xamarin.CloudDrive.Connector
{
   internal partial class OneDriveToken : IOneDriveToken
   {

      readonly string[] Scopes;
      readonly IClientApplicationBase Identity;

      internal OneDriveToken(IClientApplicationBase identity, string[] scopes)
      {
         if (identity == null) throw new ArgumentException("The identity argument for the token client must be set");
         this.Identity = identity;
         this.Scopes = scopes;
      }

      // tenants { common, organizations, consumers }
      const string Authority = "https://login.microsoftonline.com/{tenant}";
      static Uri GetAuthorityUri() => new Uri(Authority.Replace("{tenant}", "common"));

   }
}
