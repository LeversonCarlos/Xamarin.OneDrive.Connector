using System;

namespace Xamarin.CloudDrive.Connector
{
   internal partial class OneDriveToken : IOneDriveToken
   {

      internal OneDriveToken(IOneDriveIdentity identity)
      {
         Identity = identity;
      }

      IOneDriveIdentity _Identity;
      public IOneDriveIdentity Identity
      {
         get => _Identity;
         private set
         {
            if (value == null)
               throw new ArgumentException("The identity argument for the token client must be set");
            _Identity = value;
         }
      }

   }
}
