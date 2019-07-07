using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{
   internal class DependencyImplementation : IDependency
   {

      public void Initialize(Configs configs)
      {
      }

      public async Task<AuthenticationResult> GetAuthResult(IPublicClientApplication client, Configs configs)
      {
         try
         {
            return await client
               .AcquireTokenInteractive(configs.Scopes)
               .ExecuteAsync();
         }
         catch (Exception) { throw; }
      }

   }
}