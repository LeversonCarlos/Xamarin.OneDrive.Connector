using System;
using System.Collections.Generic;
using System.Threading;

namespace Xamarin.CloudDrive.Connector
{
   public class ImplementationProvider
   {

      static Dictionary<Type, object> Implementations = new Dictionary<Type, object>();

      public static void Add<T>(Func<T> createInstance)
      {
         try
         {
            var type = typeof(T);
            if (Implementations.ContainsKey(type)) { return; }
            Lazy<T> implementation = new Lazy<T>(() => createInstance(), LazyThreadSafetyMode.PublicationOnly);
            Implementations.Add(type, implementation);
         }
         catch (Exception) { throw; }
      }

      public static T Get<T>()
      {
         try
         {
            var type = typeof(T);
            var implementation = (Lazy<T>)Implementations[type];
            return implementation.Value;
         }
         catch (Exception) { throw; }
      }

   }
}