using System;
using Microsoft.Identity.Client;

namespace Xamarin.OneDrive
{
   public partial class Token : IDisposable
   {
      public Configs Configs { get; private set; }
      PublicClientApplication Client { get; set; }

      public Token(Configs configs)
      {
         this.Configs = configs;
         this.Client = new PublicClientApplication(configs.ClientID);
         if (!string.IsNullOrEmpty(configs.RedirectUri))
         { this.Client.RedirectUri = configs.RedirectUri; }
      }

      public void Dispose()
      {
         this.Client = null;
         this.Configs.Dispose();
         this.Configs = null;
      }

   }
}