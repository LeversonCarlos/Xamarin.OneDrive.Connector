using System;

namespace Xamarin.OneDrive
{
   public partial class Token : IDisposable
   {
      public Configs Configs { get; private set; }

      public Token(Configs configs)
      {
         this.Configs = configs;
      }

      public void Dispose()
      {
         this.Configs.Dispose();
         this.Configs = null;
      }

   }
}