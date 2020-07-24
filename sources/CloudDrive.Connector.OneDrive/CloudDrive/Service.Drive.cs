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

            // USER's ROOT DRIVE
            var profile = await this.GetProfile();
            var rootDrive = new DirectoryVM
            {
               ID = $"{profile.ID}!root",
               Name = profile.Description,
               Path = "/"
            };
            drives.Add(rootDrive);

            // LOCATE SHARED DRIVES
            var sharedDrives = await GetSharedDrivers();
            if (sharedDrives != null && sharedDrives.Length != 0)
               drives.AddRange(sharedDrives);

            return drives.ToArray();
         }
         catch (Exception) { throw; }
      }

      async Task<DirectoryVM[]> GetSharedDrivers()
      {
         try
         {

            var messageContent = await this.Client.GetAsync<DTOs.SharedDriveSearch>("me/drive/sharedWithMe");
            if (messageContent == null || messageContent.value == null) return null;

            var sharedDrives = messageContent.value
               ?.Where(v => !string.IsNullOrEmpty(v.remoteItem?.shared?.owner?.user?.id))
               ?.Where(v => v.remoteItem.folder != null)
               ?.Select(v => new DirectoryVM
               {
                  ID = v.remoteItem.id,
                  Name = $"{v.remoteItem.name}",
                  Path = "/"
               })
               ?.ToArray();

            return sharedDrives;

         }
         catch (Exception) { return null; }
      }

   }
}