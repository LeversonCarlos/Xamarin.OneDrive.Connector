using Microsoft.Identity.Client;

namespace Xamarin.OneDrive
{
   public partial class Configs
   {

      public string ClientID { get; set; }
      public string[] Scopes { get; set; }

      internal string RedirectUri { get; set; }
      internal UIParent UiParent { get; set; }

   }
}