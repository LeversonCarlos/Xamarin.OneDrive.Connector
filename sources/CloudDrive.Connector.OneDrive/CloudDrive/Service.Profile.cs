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
            if (!await Client.CheckConnectionAsync())
               return null;

            var profileDTO = await Client
               .GetAsync<DTOs.Profile>("me?$select=id,displayName,userPrincipalName");

            var profileData = new ProfileVM
            {
               ID = profileDTO.id,
               Description = profileDTO.displayName,
               KeyValues = new Dictionary<string, string> {
                  { "EMail", profileDTO.userPrincipalName }
               }
            };

            return profileData;
         }
         catch (Exception) { throw; }
      }

      public async Task<byte[]> GetProfilePicture()
      {
         try
         {
            var message = await Client.GetAsync("me/photo/$value");
            message.EnsureSuccessStatusCode();
            var profilePicture = await message.Content.ReadAsByteArrayAsync();
            return profilePicture;
         }
         catch (Exception) { throw; }
      }

   }
}