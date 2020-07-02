using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public class Token
   {

      [Fact]
      public void ConstructorArgumentsMustBeSet()
      {
         var ex = Assert.Throws<ArgumentException>(() => new OneDrive.Token(null));
         Assert.Equal("The identity argument for the token client must be set", ex.Message);
      }

      [Fact]
      public void InitialTokenMustBeEmpty()
      {
         var identity = IdentityBuilder.Create().Builder();
         var token = new OneDrive.Token(identity);

         var expected = "";
         var actual = token.GetToken();

         Assert.Equal(expected, actual);
      }

   }
}
