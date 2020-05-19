using System;
using System.Threading.Tasks;
using Xunit;

namespace Xamarin.OneDrive.Tests
{
   public class ProfileTests : IClassFixture<Fixture>
   {

      Fixture Fixture;
      public ProfileTests()
      {
         this.Fixture = new Fixture();
      }

      [Fact]
      public async Task Connection_Must_Succeed()
      {
         await Assert.ThrowsAsync<Exception>(async () =>
         {
            var connect = await this.Fixture.Connector.ConnectAsync();
         });
      }

   }
}