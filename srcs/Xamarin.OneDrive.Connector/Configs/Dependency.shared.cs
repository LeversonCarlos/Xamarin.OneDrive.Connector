using System;
using System.Threading;
using System.Threading.Tasks;


namespace Xamarin.OneDrive
{

   internal interface IDependency
   {
      void Initialize(Configs configs);
      Task<Microsoft.Identity.Client.AuthenticationResult> GetAuthResult(Microsoft.Identity.Client.IPublicClientApplication client, Configs configs);
   }

   internal class Dependency
   {

      static Lazy<IDependency> implementation = new Lazy<IDependency>(() => CreateDependency(), LazyThreadSafetyMode.PublicationOnly);

      static IDependency CreateDependency()
      {
         // #pragma warning disable IDE0022 // Use expression body for methods
         return new DependencyImplementation();
         // #pragma warning restore IDE0022 // Use expression body for methods
      }

      public static IDependency Current
      {
         get
         {
            IDependency ret = implementation.Value;
            if (ret == null)
            {
               throw new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
            }
            return ret;
         }
      }

   }

}