using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class OneDriveService
   {

      public async Task<FileVM[]> SearchFiles(DirectoryVM directory, string searchPattern, int limit)
      {
         try
         {

            var searchPatterns = searchPattern
               .Replace("*.", "")
               .ToLower()
               .Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
               .ToArray();

            var fileList = new List<FileVM>();
            var addFilesUntilLimit = new Func<FileVM[], bool>(files =>
            {
               fileList.AddRange(files);
               return limit == 0 || fileList.Count < limit;
            });

            await this.SearchFiles(directory, searchPatterns, addFilesUntilLimit);

            return fileList
               .OrderBy(x => x.Path)
               .Take(limit)
               .ToArray();

         }
         catch (Exception) { throw; }
      }

      private async Task<bool> SearchFiles(DirectoryVM directory, string[] searchPatterns, Func<FileVM[], bool> addFilesUntilLimit)
      {
         try
         {

            // SERACH FILE ON FOLDER
            var fileList = (await this.GetFiles(directory))?.ToList();
            fileList.RemoveAll(x => searchPatterns.Any(ext => x.Name.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase)));
            if (!addFilesUntilLimit(fileList.ToArray())) { return false; }

            // LOCATE SUB DIRECTORIES
            var childFolders = await this.GetDirectories(directory);
            if (childFolders == null || childFolders.Length == 0) { return true; }

            // LOOP SUB DIRECTORIES 
            foreach (var childFolder in childFolders)
            {
               if (!await SearchFiles(childFolder, searchPatterns, addFilesUntilLimit)) { return false; }
            }

            return true;
         }
         catch (Exception) { throw; }
      }

   }
}