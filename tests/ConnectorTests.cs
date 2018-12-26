using System.Threading.Tasks;
using Xunit;

namespace Xamarin.OneDrive.Tests
{
   public class ConnectorTests : IClassFixture<ConnectorFixture>
   { 

      ConnectorFixture ConnectorFixture;
      public ConnectorTests()
      {
         this.ConnectorFixture = new ConnectorFixture();
      }

      [Fact(Skip="We cant test this cause we have no interactive mode here")]
      public async Task Connect_MustBe_True()
      {
         var result = await this.ConnectorFixture.Connector.ConnectAsync();
         Assert.True(result);
      }

   }
}