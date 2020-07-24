using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector
{
   internal interface IStorage
   {
      Task<string[]> GetStorageList();
   }
}
