using System;
using System.Threading.Tasks;
using Xunit;

namespace Xamarin.OneDrive.Tests
{
   public class ConnectorTests
   {

      [Fact]
      public void NullConfigs_Throws_Exception()
      {
         Assert.Throws<NullReferenceException>(() =>
         {
            var connector = new Connector(null);
         });
      }

      [Fact]
      public void EmptyClientID_Throws_Exception()
      {
         Assert.Throws<ArgumentNullException>(() =>
         {
            var connector = new Connector("", "");
         });
      }

      [Fact]
      public void EmptyScope_Throws_Exception()
      {
         Assert.Throws<ArgumentNullException>(() =>
         {
            var connector = new Connector(Settings.ClientID, "");
         });
      }

   }
}