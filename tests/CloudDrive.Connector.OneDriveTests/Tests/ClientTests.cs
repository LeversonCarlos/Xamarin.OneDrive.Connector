using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   public partial class Client
   {

      [Fact]
      public void Constructor_WithNullArguments_MustThrowException()
      {
         var creator = new Action(() => new OneDriveClient(token: null));

         var expected = "The token argument for the http client must be set";
         var value = Assert.Throws<ArgumentException>(creator);

         Assert.Equal(expected, value.Message);
      }

      [Fact]
      public void Constructor_BaseAddress_MustBeAsSpected()
      {
         var client = new OneDriveClient(TokenBuilder.Create().Builder());

         var expected = "https://graph.microsoft.com/v1.0/";
         var value = client._HttpClient.BaseAddress.AbsoluteUri;

         Assert.Equal(expected, value);
      }

   }
}
