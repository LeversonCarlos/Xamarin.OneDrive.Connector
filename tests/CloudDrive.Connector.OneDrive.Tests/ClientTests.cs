using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDrive.Tests
{
   public class Client
   {

      [Fact]
      public void IdentityConstructorArgumentMustBeSet()
      {
         var creator = new Action(() => new OneDrive.Client(null));

         var expected = "The token argument for the OneDrive client must be set";
         var value = Assert.Throws<ArgumentException>(creator);

         Assert.Equal(expected, value.Message);
      }

      [Fact]
      public void BaseAddressMustBePointingToCorrectUri()
      {
         var client = new OneDrive.Client(TokenBuilder.Create().Builder());

         var expected = "https://graph.microsoft.com/v1.0/";
         var value = client.BaseAddress.AbsoluteUri;

         Assert.Equal(expected, value);
      }


   }
}
