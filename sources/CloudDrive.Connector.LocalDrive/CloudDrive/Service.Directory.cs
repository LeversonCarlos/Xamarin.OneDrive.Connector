using System.IO;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      internal DirectoryInfo GetDirectoryInfo(string path)
      {
         if (Directory.Exists(path))
            return new DirectoryInfo(path);
         else
            return null;
      }

   }
}