using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public class Service
   {

      [Fact]
      public void ConstructorArgumentsMustBeSet()
      {
         var creator = new Action(() => new OneDriveService(null));

         var expected = "The client argument for the OneDrive service must be set";
         var value = Assert.Throws<ArgumentException>(creator);

         Assert.Equal(expected, value.Message);
      }

   }
}
