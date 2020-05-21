using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public interface IToken
   {

      Task<bool> Connect();
      Task<bool> IsConnected();
      Task Disconnect();

      string GetCurrentToken();

   }
}
