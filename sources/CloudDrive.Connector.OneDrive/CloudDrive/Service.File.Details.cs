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
            var httpPath =
               $"drives/{IDs.DriveID}/items/{IDs.ID}" +
               "?select=id,name,createdDateTime,size,@microsoft.graph.downloadUrl,file,parentReference";

            // REQUEST DATA FROM SERVER
            var fileDTO = await Client
               .GetAsync<DTOs.File>(httpPath);

            // CONVERT AND RETURN
            var fileVM = GetDetails(fileDTO);
            return fileVM;

         }
         catch (Exception ex) { throw new Exception($"Error while loading details for file [{fileID}] with oneDrive service", ex); }
      }

      internal FileVM GetDetails(DTOs.File fileDTO)
      {
         return new FileVM
         {
            ID = fileDTO.id,
            Name = fileDTO.name,
            Path = GetPath(fileDTO.parentReference.path),
            CreatedDateTime = GetDetails_CreatedDateTime(fileDTO.createdDateTime),
            SizeInBytes = fileDTO.size,
            KeyValues = new Dictionary<string, string> {
               { "downloadUrl", fileDTO.downloadUrl }
            },
            ParentID = fileDTO.parentReference.id
         };
      }

      DateTime GetDetails_CreatedDateTime(string createdDateTimeText)
      {
         DateTime.TryParse(createdDateTimeText, out DateTime createdDateTime);
         return createdDateTime;
      }

   }
}