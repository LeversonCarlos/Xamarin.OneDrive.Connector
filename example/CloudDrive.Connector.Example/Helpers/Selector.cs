using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CloudDrive.Connector.Example.FolderDialog;

namespace Xamarin.CloudDrive.Connector.Example.Helpers
{

   public class Selector
   {
      static ICloudDriveService DriveService { get; set; }

      public static async Task<SelectorItem> GetFolder(ICloudDriveService driveService)
      {
         try
         {
            DriveService = driveService;

            // INITIATE THE VIEW MODEL 
            var vm = new FolderDialogVM();

            // LOCATE ROOT CHILDS
            var rootChilds = await getChildren(null);
            vm.Data.AddRange(rootChilds);
            vm.CurrentItem = null;

            // HOOK UP EVENT FOR ITEM SELECTION
            vm.OnItemSelected += async (sender, item) =>
            {
               try
               {
                  vm.IsBusy = true;
                  var folderChilds = await getChildren(item);
                  vm.Data.ReplaceRange(folderChilds);
                  vm.CurrentItem = item;
               }
               catch (Exception ex) { Console.WriteLine(ex.ToString()); }
               finally { vm.IsBusy = false; }
            };

            // SHOW DIALOG
            var folder = await vm.OpenPage();
            return folder;

         }
         catch (Exception) { throw; }
      }

      static async Task<SelectorItem[]> getChildren(SelectorItem item = null)
      {
         if (item == null || string.IsNullOrEmpty(item.ID))
         {
            var driveList = await DriveService.GetDrivers();
            return driveList
               .Select(x => new SelectorItem
               {
                  ID = x.ID,
                  Name = x.Name,
                  Path = x.Path,
                  Type = enSelectorItemType.Drive
               })
               .ToArray();
         }
         else if (item.Type == enSelectorItemType.Drive || item.Type == enSelectorItemType.Folder)
         {
            var folder = new DirectoryVM
            {
               ID = item.ID,
               Name = item.Name,
               Path = item.Path,
               ParentID = item.Parent?.ID
            };

            var folderList = (await DriveService.GetDirectories(folder)).ToList();
            var fileList = (await DriveService.GetFiles(folder)).ToList();

            var itemList = folderList
               .Select(x => new SelectorItem
               {
                  ID = x.ID,
                  Name = x.Name,
                  Path = x.Path,
                  Parent = item,
                  Type = enSelectorItemType.Folder
               })
               .Union(
                  fileList
                     .Select(x => new SelectorItem
                     {
                        ID = x.ID,
                        Name = x.Name,
                        Path = x.Path,
                        Parent = item,
                        Type = enSelectorItemType.File
                     })
                     .ToList()
               ).ToList();
            itemList.Insert(0, new SelectorItem { Name = "..", ID = item.Parent?.ID, Path = item.Parent?.Path, Parent = item?.Parent?.Parent });

            return itemList.ToArray();
         }
         else { return new SelectorItem[] { }; }
      }

   }

   public class SelectorItem
   {
      public string ID { get; set; }
      public string Name { get; set; }
      public string Path { get; set; }
      public SelectorItem Parent { get; set; }
      public enSelectorItemType Type { get; set; }
   }

   public enum enSelectorItemType : short { Drive, Folder, File }

}
