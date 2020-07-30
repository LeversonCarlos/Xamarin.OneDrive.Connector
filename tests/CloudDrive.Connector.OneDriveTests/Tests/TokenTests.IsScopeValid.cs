using System;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Token
   {

      [Fact]
      public async void IsScopeValid_WithNullAuth_MustResultFalse()
      {
         var identity = IdentityBuilder.Create().Build();
         var token = new OneDriveToken(identity);
         await token.AcquireTokenAsync();

         var expected = false;
         var actual = token.IsScopeValid();

         Assert.Equal(expected, actual);
      }

      [Theory]
      [InlineData(null, null, false)]
      [InlineData(null, new string[] { "A" }, false)]
      [InlineData(new string[] { "A", "B" }, null, false)]
      [InlineData(new string[] { "A", "B" }, new string[] { }, false)]
      [InlineData(new string[] { "A", "B" }, new string[] { "" }, false)]
      [InlineData(new string[] { "A", "B" }, new string[] { "A" }, false)]
      [InlineData(new string[] { "A", "B" }, new string[] { "A", "b" }, false)]
      [InlineData(new string[] { "A", "B" }, new string[] { "A", "B" }, true)]
      [InlineData(new string[] { "A", "B" }, new string[] { "B", "A" }, true)]
      public async void IsScopeValid_WithSpecifiedParameters_MustResultSpecifiedValue(string[] identityScopes, string[] authScopes, bool expectedValue)
      {
         var identity = IdentityBuilder.Create()
            .WithScopes(identityScopes)
            .WithAcquireTokenFromIdentity("[test]", DateTimeOffset.UtcNow, authScopes)
            .Build();
         var token = new OneDriveToken(identity);
         await token.AcquireTokenAsync();

         var actualValue = token.IsScopeValid();

         Assert.Equal(expectedValue, actualValue);
      }


   }
}
