namespace Xamarin.CloudDrive.Connector
{
   internal class Dependency
   {

      public static T GetService<T>() where T : class =>
         Xamarin.Forms.DependencyService.Get<T>();

   }
}
