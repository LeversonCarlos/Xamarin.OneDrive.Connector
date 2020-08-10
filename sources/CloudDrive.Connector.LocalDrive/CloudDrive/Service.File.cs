using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class LocalDriveService
   {

      public async Task<FileVM[]> GetFiles(DirectoryVM directory)
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

      public async Task<FileVM> GetDetails(string fileID)
      {
         if (!await CheckConnectionAsync()) return null;

         if (string.IsNullOrEmpty(fileID)) return null;
         if (!File.Exists(fileID)) return null;

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

   }
}