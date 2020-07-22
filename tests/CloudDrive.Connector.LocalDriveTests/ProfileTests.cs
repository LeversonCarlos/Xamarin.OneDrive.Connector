using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class ProfileTests
   {

      [Fact]
      public async void ProfileHasTheSpectedContent()
      {
         var service = ServiceBuilder.Create().Build();

         var expectedID = Environment.CommandLine;
         var expectedDescription = $"{Environment.UserName} on {Environment.MachineName}";
         var value = await service.GetProfile();

         Assert.Equal(expectedID, value.ID);
         Assert.Equal(expectedDescription, value.Description);
      }

   }
}
