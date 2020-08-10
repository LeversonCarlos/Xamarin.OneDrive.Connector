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
            var fileList = new List<FileVM>();
            if (limit == 0)
               return fileList.ToArray();

            // SPLIT SEARCH PATTERNS INTO ARRAY
            var searchPatterns = searchPattern
               .Replace("*.", "")
               .ToLower()
               .Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
               .ToArray();

            // AUXILIARY FUNCTION TO ADD RESULT AND CHECK FOR THE LIMIT
            var addFilesUntilLimit = new Func<FileVM[], bool>(files =>
            {
               fileList.AddRange(files);
               return fileList.Count < limit;
            });

            // EXECUTE SEARCH FOR FILES THROUGH MULTIPLE THREADS
            await SearchFiles(directory, searchPatterns, addFilesUntilLimit);

            // RESULT
            var filesArray = fileList
               .Where(file => file != null)
               .Where(file => !string.IsNullOrEmpty(file.Path))
               .OrderBy(file => file.Path)
               .Take(limit)
               .ToArray();
            return filesArray;
         }
         catch (Exception) { throw; }
      }

      async Task<bool> SearchFiles(DirectoryVM directory, string[] searchPatterns, Func<FileVM[], bool> addFilesUntilLimit)
      {
         try
         {

            // SEARCH FILE ON FOLDER
            var fileList = (await GetFiles(directory)).ToList();
            fileList.RemoveAll(x => !searchPatterns.Any(ext => x.Name.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase)));
            if (!addFilesUntilLimit(fileList.ToArray()))
               return false;

            // LOCATE SUB DIRECTORIES
            var childFolders = await GetDirectories(directory);
            if (childFolders.Length == 0)
               return true;

            // LOOP SUB DIRECTORIES 
            var childTasks = new List<Task<bool>>();
            foreach (var childFolder in childFolders)
               childTasks.Add(SearchFiles(childFolder, searchPatterns, addFilesUntilLimit));
            var childsResult = await Task.WhenAll(childTasks.ToArray());

            return childsResult.All(x => x == true);
         }
         catch (Exception) { throw; }
      }

   }
}