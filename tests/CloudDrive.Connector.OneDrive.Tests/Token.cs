using Microsoft.Identity.Client;
using Moq;
using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDrive.Tests
{
   public class Token
   {

      [Fact]
      public void ClientApplicationConstructorArgumentMustBeSet()
      {
         var ex = Assert.Throws<ArgumentException>(() => new OneDrive.Token(null));
         Assert.Equal("The client application argument for the OneDrive client must be set", ex.Message);
      }

      [Fact]
      public void InitialTokenMustBeEmpty()
      {
         var mock = new Mock<OneDrive.IToken>();
         mock.Setup(m => m.GetCurrentToken()).Returns("");
         var clientMock = new Mock<IClientApplicationBase>();
         var token = new OneDrive.Token(clientMock.Object);

         var expected = mock.Object.GetCurrentToken();
         var actual = token.GetCurrentToken();

         Assert.Equal(expected, actual);
      }

   }
}
