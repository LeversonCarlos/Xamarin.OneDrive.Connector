using System;

namespace Xamarin.OneDrive
{
   internal partial class Token : IDisposable
   {
      Configs Configs { get; set; }

      public Token(Configs configs)
      { 
         this.Configs = configs;
      }

      public void Dispose()
      { 

      }

   }
}