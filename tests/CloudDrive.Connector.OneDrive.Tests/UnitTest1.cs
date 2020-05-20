using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDrive.Tests
{
   public class UnitTest1
   {

      [Fact]
      public void Test1()
      {
         var service = new OneDrive.OneDriveService();

         Assert.NotNull(service);
      }

   }
}
