using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public Task<FileVM[]> SearchFiles(DirectoryVM directory, string searchPattern) =>
         SearchFiles(directory, searchPattern, int.MaxValue);

      public async Task<FileVM[]> SearchFiles(DirectoryVM directory, string searchPattern, int limit)
      {
         try
         {
            if (!await ConnectAsync()) return null;

            if (directory == null) return null;
            if (string.IsNullOrEmpty(directory.ID)) return null;
            if (!Directory.Exists(directory.ID)) return null;
            if (limit <= 0) return null;

            var searchOption = SearchOption.AllDirectories;

            var fileListQuery = Directory
               .EnumerateFiles(directory.ID, searchPattern, searchOption)
               .Where(file => !string.IsNullOrEmpty(file))
               .Where(file => File.Exists(file))
               .OrderBy(file => file)
               .Take(limit)
               .AsQueryable();
            var fileList = await Task.FromResult(fileListQuery.ToArray());

            var fileTasks = fileList
               .Select(file => GetDetails(file))
               .ToArray();
            var fileTasksResult = await Task.WhenAll(fileTasks);
            var fileResult = fileTasksResult
               .Where(fileVM => fileVM != null)
               .ToArray();

            return fileResult;
         }
         catch (Exception) { throw; }
      }

   }
}