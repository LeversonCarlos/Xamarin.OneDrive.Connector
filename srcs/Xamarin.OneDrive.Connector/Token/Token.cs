using System;
using Microsoft.Identity.Client;

namespace Xamarin.OneDrive
{
   internal partial class Token : IDisposable
   {
      internal Configs Configs { get; private set; }
      IPublicClientApplication Client { get; set; }

      internal Token(Configs configs)
      {
         this.Configs = configs;
         var builder = PublicClientApplicationBuilder.Create(configs.ClientID);
         if (!string.IsNullOrEmpty(configs.RedirectUri)) { builder = builder.WithRedirectUri(configs.RedirectUri); }
         this.Client = builder.Build();
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