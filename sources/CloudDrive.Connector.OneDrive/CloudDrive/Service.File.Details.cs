using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public async Task<FileVM> GetDetails(string fileID)
      {
         try
         {

            var IDs = GetIDs(fileID);
            var httpPath = $"drives/{IDs.DriveID}/items/{IDs.ID}";
            httpPath += "?select=id,name,createdDateTime,size,@microsoft.graph.downloadUrl,file,parentReference";

            // REQUEST DATA FROM SERVER
            var fileDTO = await this.Client.GetAsync<DTOs.File>(httpPath);

            // CONVERT AND RETURN
            var fileVM = this.GetDetails(fileDTO);
            return fileVM;

         }
         catch (Exception ex) { throw new Exception($"Error while loading details for file [{fileID}] with oneDrive service", ex); }
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

      DateTime GetDetails_CreatedDateTime(string createdDateTimeText)
      {
         DateTime.TryParse(createdDateTimeText, out DateTime createdDateTime);
         return createdDateTime;
      }

      string GetDetails_FullPath(DTOs.File fileDTO)
      {
         var fullPath = GetPath(fileDTO?.parentReference?.path);
         return $"{fullPath}";
      }

   }
}