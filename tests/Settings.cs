namespace Xamarin.OneDrive.Tests
{
   internal class Settings
   {
      public static string ClientID { get { return "YOUR_MICROSOFT_APPLICATION_ID"; } }
      public static string[] Scopes { get { return new string[] { "User.Read", "Files.ReadWrite" }; } }
   }
}