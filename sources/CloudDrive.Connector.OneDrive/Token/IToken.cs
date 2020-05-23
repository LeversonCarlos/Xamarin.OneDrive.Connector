using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public interface IToken
   {

      Task<bool> AcquireTokenAsync();
      Task<bool> RefreshTokenAsync();
      Task RemoveTokenAsync();

      bool IsTokenValid();
      string GetToken();

   }
}
