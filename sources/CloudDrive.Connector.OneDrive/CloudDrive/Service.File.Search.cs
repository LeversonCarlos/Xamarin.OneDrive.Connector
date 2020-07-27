using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public Task<FileVM[]> SearchFiles(DirectoryVM directory, string searchPattern) =>
         SearchFiles(directory, searchPattern, int.MaxValue);

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

            var filesArray = fileList
               ?.Where(x => x != null)
               ?.Where(x => !string.IsNullOrEmpty(x.Path))
               ?.OrderBy(x => x.Path)
               ?.ToArray();

            return filesArray;
         }
         catch (Exception) { throw; }
      }

      private async Task<bool> SearchFiles(DirectoryVM directory, string[] searchPatterns, Func<FileVM[], bool> addFilesUntilLimit)
      {
         try
         {

            // SEARCH FILE ON FOLDER
            var fileList = (await this.GetFiles(directory))?.ToList();
            fileList.RemoveAll(x => !searchPatterns.Any(ext => x.Name.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase)));
            if (!addFilesUntilLimit(fileList.ToArray())) { return false; }

            // LOCATE SUB DIRECTORIES
            var childFolders = await this.GetDirectories(directory);
            if (childFolders == null || childFolders.Length == 0) { return true; }

            // LOOP SUB DIRECTORIES 
            var childTasks = new List<Task<bool>>();
            foreach (var childFolder in childFolders)
            {
               childTasks.Add(this.SearchFiles(childFolder, searchPatterns, addFilesUntilLimit));
            }
            var childsResult = await Task.WhenAll(childTasks.ToArray());

            return childsResult.All(x => x == true);
         }
         catch (Exception) { throw; }
      }

   }
}