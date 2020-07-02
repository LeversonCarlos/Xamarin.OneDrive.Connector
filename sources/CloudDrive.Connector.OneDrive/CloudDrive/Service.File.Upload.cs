using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public Task<FileVM> Upload(string fileID, byte[] content)
      { return this.UploadContent($"me/drive/items/{fileID}/content", content); }

      public Task<FileVM> Upload(string folderID, string fileName, byte[] content)
      { return this.UploadContent($"me/drive/items/{folderID}:/{fileName}:/content", content); }

      private async Task<FileVM> UploadContent(string httpPath, byte[] content)
      {
         try
         {

            var httpData = new System.Net.Http.ByteArrayContent(content);
            var httpMessage = await this.Client.PutAsync(httpPath, httpData);
            if (!httpMessage.IsSuccessStatusCode) throw new Exception(await httpMessage.Content.ReadAsStringAsync());

            var httpContent = await httpMessage.Content.ReadAsStreamAsync();
            var fileDTO = await System.Text.Json.JsonSerializer.DeserializeAsync<DTOs.File>(httpContent);
            var fileVM = this.GetDetails(fileDTO);

            return fileVM;
         }
         catch (Exception ex) { throw new Exception($"Error while uploading file [{httpPath}] with oneDrive service", ex); }
      }

      public async Task<bool> UploadThumbnail(FileVM fileVM, System.IO.Stream image)
      {
         try
         {
            var httpPath = $"me/drive/items/{fileVM.ID}/thumbnails/0/source/content";

            var httpData = new System.Net.Http.StreamContent(image);
            var httpMessage = await this.Client.PutAsync(httpPath, httpData);

            if (!httpMessage.IsSuccessStatusCode) throw new Exception(await httpMessage.Content.ReadAsStringAsync());
            return true;
         }
         catch (Exception ex) { throw new Exception($"Error while uploading thumbnail for file [{fileVM.ID}] with oneDrive service", ex); }
      }

   }
}