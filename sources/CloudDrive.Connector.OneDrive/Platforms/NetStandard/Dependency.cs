using System;

namespace Xamarin.CloudDrive.Connector
{
   internal class Dependency
   {

      public static T GetService<T>(IServiceProvider serviceProvider) =>
         (T)serviceProvider.GetService(typeof(T));

   }
}
