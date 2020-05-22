using Microsoft.Identity.Client;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDrive.Tests
{
   public class Token
   {

      [Fact]
      public void IdentityConstructorArgumentMustBeSet()
      {
         var ex = Assert.Throws<ArgumentException>(() => new OneDrive.Token(null));
         Assert.Equal("The identity argument for the OneDrive client must be set", ex.Message);
      }

      [Fact]
      public void InitialTokenMustBeEmpty()
      {
         var clientMock = new Mock<IClientApplicationBase>();
         var token = new OneDrive.Token(clientMock.Object);

         var expected = "";
         var actual = token.GetCurrentToken();

         Assert.Equal(expected, actual);
      }

      [Fact]
      public async void InitialConnectionStateMustBeFalse()
      {
         var clientMock = new Mock<IClientApplicationBase>();
         clientMock.Setup(m => m.GetAccountsAsync()).ReturnsAsync(() => new IAccount[] { });
         var token = new OneDrive.Token(clientMock.Object);

         var expected = false;
         var actual = await token.CheckConnection();

         Assert.Equal(expected, actual);
      }

   }
}
