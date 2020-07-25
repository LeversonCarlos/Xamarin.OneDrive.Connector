using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public async Task<FileVM[]> GetFiles(DirectoryVM directory)
      {
         try
         {
            if (!await CheckConnectionAsync()) return null;

            if (directory == null) return null;
            if (string.IsNullOrEmpty(directory.ID)) return null;
            if (!Directory.Exists(directory.ID)) return null;

            var searchPattern = "*.*";
            var searchOption = SearchOption.TopDirectoryOnly;

            var fileListQuery = Directory
               .EnumerateFiles(directory.ID, searchPattern, searchOption)
               .Where(file => !string.IsNullOrEmpty(file))
               .Where(file => File.Exists(file))
               .OrderBy(file => file)
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

      public async Task<FileVM> GetDetails(string fileID)
      {
         try
         {
            if (!await CheckConnectionAsync()) return null;

            var fileInfo = await Task.FromResult(new FileInfo(fileID));
            var fileData = new FileVM
            {
               ID = fileInfo.FullName,
               Name = fileInfo.Name,
               CreatedDateTime = fileInfo.CreationTime,
               SizeInBytes = fileInfo.Length,
               Path = fileInfo.DirectoryName,
               ParentID = fileInfo.DirectoryName
            };

            return fileData;
         }
         catch (Exception) { throw; }
      }

   }
}