using System.IO;
using System.Linq;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests.Helpers
{
   internal class FileSystem
   {

      public static string CurrentDirectory => Directory
         .GetCurrentDirectory();

      public static string SampleFile => Directory
         .EnumerateFiles(CurrentDirectory, "*.dll", SearchOption.AllDirectories)
         .Where(file => !string.IsNullOrEmpty(file))
         .Where(file => File.Exists(file))
         .OrderBy(file => file)
         .Take(1)
         .FirstOrDefault();

   }
}
