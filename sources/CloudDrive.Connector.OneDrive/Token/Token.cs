using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   public partial class Token : IToken
   {

      public Task<bool> Connect() => throw new NotImplementedException();
      public Task<bool> IsConnected() => throw new NotImplementedException();
      public Task Disconnect() => throw new NotImplementedException();

      public string GetCurrentToken() => throw new NotImplementedException();

   }
}
