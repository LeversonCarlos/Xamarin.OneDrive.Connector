using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      string CurrentToken { get; set; } = "";

      public string GetCurrentToken() => this.CurrentToken;

   }
}
