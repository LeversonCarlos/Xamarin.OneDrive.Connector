using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      public async void GetSharedDrives_WithException_MustResultNull()
      {
         var exception = new Exception("Some Dummy Exception");
         var client = ClientBuilder.Create().With("me/drive/sharedWithMe", exception).Build();
         var service = new OneDriveService(client);

         var value = await service.GetSharedDrives();

         Assert.Null(value);
      }

   }
}
