using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDrive.Tests
{
   public class Client
   {

      [Fact]
      public void IdentityConstructorArgumentMustBeSet()
      {
         var ex = Assert.Throws<ArgumentException>(() => new OneDrive.Client(null));
         Assert.Equal("The token argument for the OneDrive client must be set", ex.Message);

         var client  = new OneDrive.Client(TokenBuilder.Create().Builder());
         Assert.NotNull(client);
      }


   }
}
