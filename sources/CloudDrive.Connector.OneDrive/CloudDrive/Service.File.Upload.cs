using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public Task<FileVM> Upload(string fileID, byte[] content)
      {
         var IDs = GetIDs(fileID);
         return UploadContent($"drives/{IDs.DriveID}/items/{IDs.ID}/content", content);
      }

      public Task<FileVM> Upload(string folderID, string fileName, byte[] content)
      {
         var IDs = GetIDs(folderID);
         return UploadContent($"drives/{IDs.DriveID}/items/{IDs.ID}:/{fileName}:/content", content);
      }

      async Task<FileVM> UploadContent(string httpPath, byte[] content)
      {
         try
         {

            var httpData = new System.Net.Http.ByteArrayContent(content);
            var httpMessage = await Client.PutAsync(httpPath, httpData);

            if (!httpMessage.IsSuccessStatusCode) 
               throw new Exception(await httpMessage.Content.ReadAsStringAsync());

            var httpContent = await httpMessage.Content.ReadAsStreamAsync();
            var fileDTO = await System.Text.Json.JsonSerializer.DeserializeAsync<DTOs.File>(httpContent);

            var fileVM = GetDetails(fileDTO);
            return fileVM;
         }
         catch (Exception ex) { throw new Exception($"Error while uploading file [{httpPath}] with oneDrive service", ex); }
      }

      public async Task<bool> UploadThumbnail(string fileID, System.IO.Stream image)
      {
         try
         {
            var IDs = GetIDs(fileID);
            var httpPath = $"drives/{IDs.DriveID}/items/{IDs.ID}/thumbnails/0/source/content";

            var httpData = new System.Net.Http.StreamContent(image);
            var httpMessage = await Client.PutAsync(httpPath, httpData);

            if (!httpMessage.IsSuccessStatusCode) 
               throw new Exception(await httpMessage.Content.ReadAsStringAsync());
            return true;
         }
         catch (Exception ex) { throw new Exception($"Error while uploading thumbnail for file [{fileID}] with oneDrive service", ex); }
      }

   }
}