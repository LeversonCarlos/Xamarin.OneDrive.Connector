using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.LocalDrive
{
   partial class LocalDriveService
   {

      public async Task<FileVM[]> GetFiles(DirectoryVM directory)
      {
         try
         {
            string searchPattern = "*.*";
            System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly;

            var fileQuery = System.IO.Directory
               .EnumerateFiles(directory.ID, searchPattern, searchOption)
               .Where(x => !string.IsNullOrEmpty(x))
               .OrderBy(x => x)
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

      public async Task<FileVM> GetDetails(string fileID)
      {
         try
         {
            var fileInfo = await Task.FromResult(new System.IO.FileInfo(fileID));

            var fileData = new FileVM
            {
               ID = fileInfo.FullName,
               Name = fileInfo.Name,
               CreatedDateTime = fileInfo.CreationTime,
               SizeInBytes = fileInfo.Length,
               Path = fileInfo.FullName,
               ParentID = fileInfo.DirectoryName
            };

            return fileData;
         }
         catch (Exception ex)
         {
            Console.WriteLine($"fileID:{fileID}{Environment.NewLine}Exception:{ex}");
            return null;
         }
      }

   }
}