using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class DownloadTests
   {

      [Fact]
      public async void Download_WithoutConnection_MustReturnNull()
      {
         var connection = ConnectionBuilder.Create().WithCheckConnectionValue(false).Build();
         var service = new LocalDriveService(connection);

         var fileID = (string)null;
         var value = await service.Download(fileID);

         Assert.Null(value);
      }

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      [InlineData("#:/$%^")]
      public async void Download_WithInvalidParameter_MustReturnNull(string fileID)
      {
         var service = new LocalDriveService();

         var value = await service.Download(fileID);

         Assert.Null(value);
      }

      [Fact]
      public async void Download_WithValidFile_MustReturnSpectedData()
      {
         using (var sampleClone = new Helpers.SampleClone())
         {
            var service = new LocalDriveService();

            sampleClone.WriteFile();
            var expectedValue = sampleClone.FileContent.Length;
            var value = await service.Download(sampleClone.FilePath);

            Assert.Equal(expectedValue, value?.Length);
         }
      }

      /*
      [Fact]
      public void Download_WithLockedFile_MustThrowException()
      {
         var service = new LocalDriveService();

         var sampleFile = Helpers.FileSystem.SampleFile;
         var sampleFileCopy = $"{sampleFile}.sample";
         File.Copy(sampleFile, sampleFileCopy, true);
         var value = new Action(async () => await service.Download(sampleFileCopy));

         using (var stream = File.Open(sampleFileCopy, FileMode.Open, FileAccess.Read, FileShare.None))
         {
            Assert.Throws<Exception>(value);
         }
         File.Delete(sampleFileCopy);
      }
      */

   }
}
