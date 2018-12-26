using System;

namespace Xamarin.OneDrive.Connector.Tests
{
   public class TokenFixture : IDisposable
   {
      internal Token Token { get; private set; }

      public TokenFixture()
      {
         var configs = new Configs { ClientID = Settings.ClientID };
         this.Token = new Token(configs);
         Console.WriteLine("TokenFixture Initialize");
      }

      public void Dispose()
      {
         this.Token.Dispose();
         this.Token = null;
         Console.WriteLine("TokenFixture Dispose");
      }

   }
}