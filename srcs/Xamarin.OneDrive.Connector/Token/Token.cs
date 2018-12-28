using System;
using Microsoft.Identity.Client;

namespace Xamarin.OneDrive
{
   internal partial class Token : IDisposable
   {
      internal Configs Configs { get; private set; }
      PublicClientApplication Client { get; set; }

      internal Token(Configs configs)
      {
         this.Configs = configs;
         this.Client = new PublicClientApplication(configs.ClientID);
         if (!string.IsNullOrEmpty(configs.RedirectUri))
         { this.Client.RedirectUri = configs.RedirectUri; }
      }

      public void Dispose()
      {
         this.Client = null;
         this.AuthResult = null;
         this.Configs.Dispose();
         this.Configs = null;
      }

   }
}