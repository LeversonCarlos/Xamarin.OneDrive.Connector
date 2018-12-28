using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive.Profile
{
   public static partial class Extender
   {

      public static async Task<ProfileData> GetProfileAsync(this Xamarin.OneDrive.Connector connector)
      {
         try
         { 
            var httpMessage = await connector.GetAsync("me?$select=id,displayName,mail");
            if (!httpMessage.IsSuccessStatusCode)
            { throw new Exception(await httpMessage.Content.ReadAsStringAsync()); }

            var httpContent = await httpMessage.Content.ReadAsStreamAsync();
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ProfileData));
            var httpResult = (ProfileData)serializer.ReadObject(httpContent);

            return httpResult;
         }
         catch (Exception) { throw; }
      }

   }
}