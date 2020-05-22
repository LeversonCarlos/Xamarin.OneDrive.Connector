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
         var identity = IdentityBuilder.Create().Builder();
         var token = new OneDrive.Token(identity);

         var expected = "";
         var actual = token.GetCurrentToken();

         Assert.Equal(expected, actual);
      }

      [Fact]
      public async void InitialConnectionStateMustBeFalse()
      {
         var identity = IdentityBuilder.Create().WithEmptyAccountList().Builder();
         var token = new OneDrive.Token(identity);

         var expected = false;
         var actual = await token.CheckConnection();

         Assert.Equal(expected, actual);
      }

   }
}
