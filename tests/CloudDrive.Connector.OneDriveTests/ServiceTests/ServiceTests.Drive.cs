using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Sdk;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      public async void GetSharedDrives_WithException_MustResultNull()
      {
         var exception = new Exception("Some Dummy Exception");
         var client = ClientBuilder.Create().With("me/drive/sharedWithMe", exception).Build();
         var service = new OneDriveService(client);

         var value = await service.GetSharedDrives();

         Assert.Null(value);
      }

      [Theory]
      [MemberData(nameof(GetSharedDrives_WithNullShares_MustResultNull_Data))]
      internal async void GetSharedDrives_WithNullShares_MustResultNull(DTOs.SharedDriveSearch param)
      {
         var client = ClientBuilder.Create().With("me/drive/sharedWithMe", param).Build();
         var service = new OneDriveService(client);

         var value = await service.GetSharedDrives();

         Assert.Null(value);
      }
      public static IEnumerable<object[]> GetSharedDrives_WithNullShares_MustResultNull_Data() =>
         new[]
         {
            new object[] { null },
            new object[] { new DTOs.SharedDriveSearch() }
         };

      [Theory]
      [MemberData(nameof(GetSharedDrives_WithInvalidShares_MustResultEmptyArray_Data))]
      internal async void GetSharedDrives_WithInvalidShares_MustResultEmptyArray(DTOs.SharedDriveSearch param)
      {
         var client = ClientBuilder.Create().With("me/drive/sharedWithMe", param).Build();
         var service = new OneDriveService(client);

         var value = await service.GetSharedDrives();

         Assert.Null(value);
      }

      public static IEnumerable<object[]> GetSharedDrives_WithInvalidShares_MustResultEmptyArray_Data() =>
         new[]
         {
            new object[] { null },
            new object[] { new DTOs.SharedDriveSearch() }
         };

   }
}
