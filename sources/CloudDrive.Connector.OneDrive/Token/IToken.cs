using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public interface IToken
   {

      Task<bool> Connect();
      Task<bool> CheckConnection();
      Task Disconnect();

      string GetCurrentToken();

   }
}
