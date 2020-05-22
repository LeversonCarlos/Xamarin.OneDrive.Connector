using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public interface IToken
   {

      Task<bool> AcquireTokenAsync();

      // Task<bool> Connect();
      // Task<bool> CheckConnection();
      // Task Disconnect();

      bool IsTokenValid();
      string GetCurrentToken();

   }
}
