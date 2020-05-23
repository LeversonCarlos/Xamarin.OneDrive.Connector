using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public interface IClient
   {

      Task<bool> ConnectAsync();
      Task<bool> CheckConnectionAsync();

   }
}
