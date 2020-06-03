using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService
   {

      public async Task<FileVM[]> SearchFiles(DirectoryVM directory, string searchPattern, int limit)
      {
         try
         {
            if (!await this.ConnectAsync()) { return null; }
            if (limit == 0) { limit = int.MaxValue; }

            System.IO.SearchOption searchOption = System.IO.SearchOption.AllDirectories;
            var fileQuery = System.IO.Directory
               .EnumerateFiles(directory.ID, searchPattern, searchOption)
               .Where(x => !string.IsNullOrEmpty(x))
               .OrderBy(x => x)
               .Take(limit)
               .AsQueryable();
            var fileQueryResult = await Task.FromResult(fileQuery.ToList());

            var fileList = new List<FileVM>();
            foreach (var filePath in fileQueryResult)
            {
               var fileItem = await this.GetDetails(filePath);
               if (fileItem != null) { fileList.Add(fileItem); }
            }

            return fileList.ToArray();
         }
         catch (Exception) { throw; }
      }

   }
}