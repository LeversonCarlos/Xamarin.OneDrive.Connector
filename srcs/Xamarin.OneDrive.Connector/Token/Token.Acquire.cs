using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{
   partial class Token
   {

      internal async Task<bool> AcquireAsync()
      {
         try
         {
            this.AuthResult = await Dependency.Current.GetAuthResult(this.Client, this.Configs);
            return this.IsValid();
         }
         catch (Exception) { throw; }
      }

   }
}