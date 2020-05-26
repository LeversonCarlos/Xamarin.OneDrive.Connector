using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class OneDriveService
   {

      public async Task<FileVM> GetDetails(string fileID)
      {
         try
         {

            var httpPath = $"me/drive/items/{fileID}";
            httpPath += "?select=id,name,createdDateTime,size,@microsoft.graph.downloadUrl,file,parentReference";

            // REQUEST DATA FROM SERVER
            var fileDTO = await this.Client.GetAsync<DTOs.File>(httpPath);

            // CONVERT AND RETURN
            var fileVM = this.GetDetails(fileDTO);
            return fileVM;

         }
         catch (Exception) { throw; }
      }

      private FileVM GetDetails(DTOs.File fileDTO)
      {
         try
         {
            var fileData = new FileVM
            {
               ID = fileDTO.id,
               Name = fileDTO.name,
               Path = this.GetDetails_FullPath(fileDTO),
               CreatedDateTime = this.GetDetails_CreatedDateTime(fileDTO.createdDateTime),
               SizeInBytes = fileDTO.size,
               KeyValues = new Dictionary<string, string> {
                  { "downloadUrl", fileDTO.downloadUrl }
               },
               ParentID = fileDTO.parentReference.id
            };
            return fileData;
         }
         catch (Exception) { throw; }
      }

      private DateTime GetDetails_CreatedDateTime(string createdDateTimeText)
      {
         DateTime.TryParse(createdDateTimeText, out DateTime createdDateTime);
         return createdDateTime;
      }

      private string GetDetails_FullPath(DTOs.File fileDTO)
      {
         if (fileDTO.parentReference == null || string.IsNullOrEmpty(fileDTO.parentReference.path)) { return ""; }
         var fullPath = fileDTO.parentReference.path;
         fullPath = System.Uri.UnescapeDataString(fullPath);
         fullPath = fullPath.Replace("/drive/root:", "");
         return $"{fullPath}/{fileDTO.name}";
      }

   }
}