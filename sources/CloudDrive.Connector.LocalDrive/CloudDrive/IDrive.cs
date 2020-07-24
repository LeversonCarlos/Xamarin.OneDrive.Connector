using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal interface IDrives
   {
      Task<string[]> GetDriveList();
   }
}
