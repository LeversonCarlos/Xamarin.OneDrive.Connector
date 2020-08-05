using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   partial class OneDriveService
   {

      public async Task<DirectoryVM[]> GetDrives()
      {
         try
         {
            var drives = new List<DirectoryVM>();

            // USER's PROFILE
            var profile = await GetProfile();
            if (profile == null)
               return null;

            // ROOT DRIVE
            var rootDrive = new DirectoryVM
            {
               ID = $"{profile.ID}!root",
               Name = profile.Description,
               Path = "/"
            };
            drives.Add(rootDrive);

            // SHARED DRIVES
            var sharedDrives = await GetSharedDrives();
            if (sharedDrives != null)
               drives.AddRange(sharedDrives);

            return drives.ToArray();
         }
         catch (Exception) { throw; }
      }

      internal async Task<DirectoryVM[]> GetSharedDrives()
      {
         try
         {

            var messageContent = await Client
               .GetAsync<DTOs.SharedDriveSearch>("me/drive/sharedWithMe");
            if (messageContent == null || messageContent.value == null)
               return null;

            var sharedDrives = messageContent.value
               .Where(v => v.remoteItem != null)
               .Where(v => v.remoteItem.folder != null)
               .Where(v => !string.IsNullOrEmpty(v.remoteItem.shared?.owner?.user?.id))
               .Select(v => new DirectoryVM
               {
                  ID = v.remoteItem.id,
                  Name = v.remoteItem.name,
                  Path = "/"
               })
               .ToArray();

            return sharedDrives;
         }
         catch (Exception) { return null; }
      }

   }
}