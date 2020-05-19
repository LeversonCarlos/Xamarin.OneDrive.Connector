using System;
using System.Collections.Generic;

namespace Xamarin.CloudDrive.Connector.Common
{
   public class DependencyProvider
   {

      static Dictionary<Type, object> CreateCollection = new Dictionary<Type, object>();
      static Dictionary<Type, object> ServiceCollection = new Dictionary<Type, object>();

      public static void Add<T>(Func<T> createInstance)
      {
         if (!CreateCollection.ContainsKey(typeof(T))) CreateCollection.Add(typeof(T), createInstance);
      }

      public static T Get<T>()
      {
         var type = typeof(T);
         if (!ServiceCollection.ContainsKey(type))
         {
            if (!CreateCollection.ContainsKey(type)) { throw new Exception($"Type {type.FullName} not defined on service provider"); }
            var create = (Func<T>)CreateCollection[type];
            ServiceCollection.Add(type, create());
         }
         if (!ServiceCollection.ContainsKey(type)) { throw new Exception($"Type {type.FullName} could not be created on service provider"); }
         return (T)ServiceCollection[type];
      }

   }
}