using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public interface IToken
   {

      Task<bool> ConnectAsync();
      Task<bool> CheckConnectionAsync();
      Task DisconnectAsync();

      string GetToken();

   }
}
