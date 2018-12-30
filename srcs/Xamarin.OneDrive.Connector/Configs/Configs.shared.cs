using System;
using Microsoft.Identity.Client;

namespace Xamarin.OneDrive
{
   internal class Configs : IDisposable
   {

      public string ClientID { get; set; }
      public string[] Scopes { get; set; }

      internal string RedirectUri { get; set; }
      internal UIParent UiParent { get; set; }

      public void Dispose()
      {
         this.UiParent = null;
      }

   }
}