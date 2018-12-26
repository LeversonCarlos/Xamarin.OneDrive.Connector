using System;
using Xamarin.OneDrive;

namespace Xamarin.OneDrive.Tests
{
   public class TokenFixture : IDisposable
   {
      internal Token Token { get; private set; }

      public TokenFixture()
      {
         var configs = new Configs { ClientID = Settings.ClientID };
         this.Token = new Token(configs);
      }

      public void Dispose()
      {
         this.Token.Dispose();
         this.Token = null;
      }

   }
}