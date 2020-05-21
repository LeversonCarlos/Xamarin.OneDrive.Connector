using System;
using System.Threading.Tasks;

namespace Xamarin.CloudDrive.Connector.OneDrive
{
   partial class Token
   {

      public async Task<bool> CheckConnection()
      {
         try
         {

            // CHECK IF AUTHENTICATION IS STILL VALID
            if (this.IsAuthValid()) { return true; }

            // REFRESH AN EXPIRED TOKEN 
            if (await this.RefreshToken()) { return true; }

            // OTHERWISE
            return false;

         }
         catch (Exception) { throw; }
      }

   }
}
