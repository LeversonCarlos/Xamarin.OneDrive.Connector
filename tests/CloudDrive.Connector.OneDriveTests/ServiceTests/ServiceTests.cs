using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public partial class ServiceTests
   {

      [Fact]
      public void Constructor_WithNullArguments_MustThrowException()
      {
         var creator = new Action(() => new OneDriveService(client: null));

         var expected = "The client argument for the OneDrive service must be set";
         var value = Assert.Throws<ArgumentException>(creator);

         Assert.Equal(expected, value.Message);
      }

   }
}
