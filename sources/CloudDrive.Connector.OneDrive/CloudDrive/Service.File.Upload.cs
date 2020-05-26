using System;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Common;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class OneDriveService
   {

      public Task<FileVM> Upload(string fileID, byte[] fileContent) => throw new System.NotImplementedException();
      public Task<FileVM> Upload(string directoryID, string fileName, byte[] fileContent) => throw new System.NotImplementedException();

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
         catch (Exception) { throw; }
      }

   }
}