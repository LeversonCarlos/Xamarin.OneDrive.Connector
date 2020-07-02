using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public async Task<ProfileVM> GetProfile()
      {
         try
         {
            var messageContent = await this.Client.GetAsync<DTOs.Profile>("me?$select=id,displayName,userPrincipalName");
            var profileData = new ProfileVM
            {
               ID = messageContent.id,
               Description = messageContent.displayName,
               KeyValues = new Dictionary<string, string> {
                  { "EMail", messageContent.userPrincipalName }
               }
            };
            // profileData.ProfilePicture = await this.GetProfilePicture();
            return profileData;
         }
         catch (Exception) { throw; }
      }

      private async Task<byte[]> GetProfilePicture()
      {
         try
         {
            var message = await this.Client.GetAsync("me/photo/$value");
            if (!message.IsSuccessStatusCode)
            { throw new Exception(await message.Content.ReadAsStringAsync()); }
            var profilePicture = await message.Content.ReadAsByteArrayAsync();
            return profilePicture;
         }
         catch (Exception) { throw; }
      }

   }
}