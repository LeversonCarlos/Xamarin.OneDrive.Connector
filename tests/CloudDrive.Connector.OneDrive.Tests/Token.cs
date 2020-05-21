using Moq;
using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDrive.Tests
{
   public class UnitTest1
   {

      [Fact]
      public void InitialTokenMustBeEmpty()
      {
         var mock = new Mock<OneDrive.IToken>();
         mock.Setup(m => m.GetCurrentToken()).Returns("");
         var token = new OneDrive.Token();

         var expected = mock.Object.GetCurrentToken();
         var actual = token.GetCurrentToken();

         Assert.Equal(expected, actual);
      }

   }
}
