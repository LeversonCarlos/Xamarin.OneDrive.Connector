using System;
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
      public void InitialTokenMustBeInvalid()
      {
         var identity = IdentityBuilder.Create().Builder();
         var token = new OneDrive.Token(identity);

         var expected = false;
         var actual = token.IsTokenValid();

         Assert.Equal(expected, actual);
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
