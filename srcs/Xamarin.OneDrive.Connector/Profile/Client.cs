using System;
using System.Threading.Tasks;

namespace Xamarin.OneDrive
{
   partial class Connector
   {

      public async Task<Profile> GetProfileAsync()
      {
         try
         { 
            var httpMessage = await this.GetAsync("me?$select=id,displayName,mail");
            if (!httpMessage.IsSuccessStatusCode)
            { throw new Exception(await httpMessage.Content.ReadAsStringAsync()); }

            var httpContent = await httpMessage.Content.ReadAsStreamAsync();
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(Profile));
            var httpResult = (Profile)serializer.ReadObject(httpContent);

            return httpResult;
         }
         catch (Exception) { throw; }
      }

   }
}