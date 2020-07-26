using System;
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

   internal class SampleClone : IDisposable
   {

      public SampleClone()
      {
         var sampleFile = FileSystem.SampleFile;
         FilePath = $"{sampleFile}.clone";
         FileContent = File.ReadAllBytes(sampleFile);
      }

      public readonly string FilePath;
      public readonly byte[] FileContent;

      public void WriteFile() =>
         File.WriteAllBytes(FilePath, FileContent);

      public void RemoveFile()
      {
         try
         {
            if (File.Exists(FilePath))
               File.Delete(FilePath);
         }
         catch { }
      }

      public void Dispose() =>
         RemoveFile();

   }

}
