using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class ServiceTests
   {

      [Fact]
      internal async void GetDrives_WithException_MustThrowException()
      {
         var exception = new Exception("Some Dummy Exception");
         var client = ClientBuilder.Create().With("me?$select=id,displayName,userPrincipalName", exception).Build();
         var service = new OneDriveService(client);

         var value = await Assert.ThrowsAsync<Exception>(async () => await service.GetDrives());

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

      [Fact]
      internal async void GetDrives_WithoutConnection_MustResultNull()
      {
         var client = ClientBuilder.Create().WithoutConnection().Build();
         var service = new OneDriveService(client);

         var value = await service.GetDrives();

         Assert.Null(value);
      }

      [Fact]
      internal async void GetDrives_WithValidProfile_MustResultSpectedDrive()
      {
         var param = new DTOs.Profile { id = "id", displayName = "displayName" };
         var client = ClientBuilder.Create().With("", param).Build();
         var service = new OneDriveService(client);

         var value = await service.GetDrives();

         Assert.NotNull(value);
         Assert.NotEmpty(value);
         Assert.Equal($"id!root", value[0].ID);
         Assert.Equal("displayName", value[0].Name);
         Assert.Equal("/", value[0].Path);
      }

      [Fact]
      internal async void GetSharedDrives_WithException_MustResultNull()
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

         Assert.NotNull(value);
         Assert.Empty(value);
      }
      public static IEnumerable<object[]> GetSharedDrives_WithInvalidShares_MustResultEmptyArray_Data() =>
         GetSharedDrives_WithInvalidShares_MustResultEmptyArray_Data_Shares()
         .Union(new[] { new object[] { new DTOs.SharedDriveSearch { value = new DTOs.SharedDrive[] { } } } })
         .ToArray();
      internal static IEnumerable<object[]> GetSharedDrives_WithInvalidShares_MustResultEmptyArray_Data_Shares() =>
         GetSharedDrives_WithInvalidShares_MustResultEmptyArray_Data_Drives()
            .Select(drive => new DTOs.SharedDriveSearch { value = new DTOs.SharedDrive[] { drive } })
            .Select(search => new object[] { search })
            .ToArray();
      internal static DTOs.SharedDrive[] GetSharedDrives_WithInvalidShares_MustResultEmptyArray_Data_Drives() =>
         new[] {
            new DTOs.SharedDrive{},
            new DTOs.SharedDrive{ remoteItem=new DTOs.SharedDriveDetails{ } },
            new DTOs.SharedDrive{ remoteItem=new DTOs.SharedDriveDetails{ shared=new DTOs.SharedDriveDetailsShared{ } } },
            new DTOs.SharedDrive{ remoteItem=new DTOs.SharedDriveDetails{ folder=new DTOs.DirectoryDetails{ } } },
            new DTOs.SharedDrive{ remoteItem=new DTOs.SharedDriveDetails{ folder=new DTOs.DirectoryDetails{ },
               shared=new DTOs.SharedDriveDetailsShared{ } } },
            new DTOs.SharedDrive{ remoteItem=new DTOs.SharedDriveDetails{ folder=new DTOs.DirectoryDetails{ },
               shared=new DTOs.SharedDriveDetailsShared{ owner=new DTOs.SharedDriveDetailsSharedOwner{ } } } },
            new DTOs.SharedDrive{ remoteItem=new DTOs.SharedDriveDetails{ folder=new DTOs.DirectoryDetails{ },
               shared=new DTOs.SharedDriveDetailsShared{ owner=new DTOs.SharedDriveDetailsSharedOwner{ user=new DTOs.Profile{ } } } } },
            new DTOs.SharedDrive{ remoteItem=new DTOs.SharedDriveDetails{ folder=new DTOs.DirectoryDetails{ },
               shared=new DTOs.SharedDriveDetailsShared{ owner=new DTOs.SharedDriveDetailsSharedOwner{ user=new DTOs.Profile{ id="" } } } } },
         };

      [Fact]
      internal async void GetSharedDrives_WithValidShares_MustResultAsSpected()
      {

         var drive = new DTOs.SharedDrive
         {
            remoteItem = new DTOs.SharedDriveDetails
            {
               id = "driveID",
               name = "driveName",
               folder = new DTOs.DirectoryDetails { },
               shared = new DTOs.SharedDriveDetailsShared { owner = new DTOs.SharedDriveDetailsSharedOwner { user = new DTOs.Profile { id = "userID" } } }
            }
         };
         var param = new DTOs.SharedDriveSearch { value = new DTOs.SharedDrive[] { drive } };
         var client = ClientBuilder.Create().With("me/drive/sharedWithMe", param).Build();
         var service = new OneDriveService(client);

         var value = await service.GetSharedDrives();

         Assert.NotNull(value);
         Assert.NotEmpty(value);
         Assert.Equal("driveID", value[0].ID);
         Assert.Equal("driveName", value[0].Name);
         Assert.Equal("/", value[0].Path);
      }

   }
}
