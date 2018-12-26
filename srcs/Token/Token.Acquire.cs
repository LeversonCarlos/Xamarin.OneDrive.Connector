using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{
   partial class Token
   { 

      public async Task<bool> AcquireAsync()
      {
         try
         {
            this.AuthResult = await this.Client.AcquireTokenAsync(this.Configs.Scopes, this.Configs.UiParent);
            return this.IsValid();
         }
         catch (Exception) { return false; }
      }

   }
}