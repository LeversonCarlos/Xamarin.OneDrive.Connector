using System;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.CloudDrive.Connector.Helpers
{
   internal class StorageHelper
   {

      internal static List<string> GetStorages()
      {
         try
         {

            var externalStorages = GetStoragesFromProcMounts();
            if (externalStorages == null) { externalStorages = new List<string>(); }

            externalStorages.Add(global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);

            externalStorages = externalStorages
               .GroupBy(x => x)
               .Select(x => x.Key)
               .OrderBy(x => x)
               .ToList();

            return externalStorages;
         }
         catch (Exception) { throw; }
      }

      private static List<string> GetStoragesFromProcMounts()
      {
         try
         {

            // ATTEMPTING TO READ '/proc/mounts' TO SEE IF THERE'S AN EXTERNAL SD CARD REFERENCE
            string procMountsContent = System.IO.File.ReadAllText("/proc/mounts");
            if (string.IsNullOrEmpty(procMountsContent)) { return null; }

            var procMountsStorages = procMountsContent
               .Split('\n', '\r')
               .Where(x => x.IndexOf("storage", StringComparison.OrdinalIgnoreCase) >= 0)
               .ToList();

            // you can have things like fat, vfat, exfat, texfat, etc.
            var procMountsEntries = procMountsStorages
               .Where(x => x.IndexOf("ext", StringComparison.OrdinalIgnoreCase) >= 0 ||
                           x.IndexOf("sd", StringComparison.OrdinalIgnoreCase) >= 0 ||
                           x.IndexOf("fat", StringComparison.OrdinalIgnoreCase) >= 0)
               .ToList();

            // e.g. /dev/block/vold/179:9 /storage/extSdCard vfat rw,dirsync,nosuid, blah
            var procMountsResult = procMountsEntries
               .Select(x => x.Split().Where(s => s.IndexOf("/storage/", System.StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault())
               .Where(x => !string.IsNullOrEmpty(x))
               .GroupBy(x => x)
               .Select(x => x.Key)
               .ToList();

            return procMountsResult;
         }
         catch (Exception ex) { Console.WriteLine($"Exception:{ex}"); return null; }
      }

   }
}
