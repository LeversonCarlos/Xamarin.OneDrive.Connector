using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class UploadTests
   {

      [Fact]
      public async void Upload_WithoutConnection_MustReturnNull()
      {
         var connection = ConnectionBuilder.Create().WithCheckConnectionValue(false).Build();
         var service = new LocalDriveService(connection);

         var value = await service.Upload(null, null);

         Assert.Null(value);
      }

      [Theory]
      [InlineData((string)null)]
      [InlineData("")]
      public async void Upload_WithInvalidFileID_MustReturnNull(string fileID)
      {
         var service = new LocalDriveService();

         var value = await service.Upload(fileID, null);

         Assert.Null(value);
      }

      [Theory]
      [InlineData((byte[])null)]
      [InlineData(new byte[] { })]
      public async void Upload_WithInvalidFileContent_MustReturnNull(byte[] fileContent)
      {
         var service = new LocalDriveService();

         var fileID = Helpers.FileSystem.SampleFile;
         var value = await service.Upload(fileID, fileContent);

         Assert.Null(value);
      }

      [Fact]
      public async void Upload_WithValidExistingFile_MustReturnSpectedData()
      {
         using (var sampleClone = new Helpers.SampleClone())
         {
            var service = new LocalDriveService();

            sampleClone.WriteFile();
            var expectedValue = await service.GetDetails(sampleClone.FilePath);
            var value = await service.Upload(sampleClone.FilePath, sampleClone.FileContent);

            Assert.Equal(expectedValue?.SizeInBytes, value?.SizeInBytes);
         }
      }

      [Fact]
      public async void Upload_WithValidNonExistingFile_MustReturnSpectedData()
      {
         using (var sampleClone = new Helpers.SampleClone())
         {
            var service = new LocalDriveService();

            var directoryID = Path.GetDirectoryName(sampleClone.FilePath);
            var fileName = Path.GetFileName(sampleClone.FilePath);
            var value = await service.Upload(directoryID, fileName, sampleClone.FileContent);
            var expectedValue = await service.GetDetails($"{directoryID}{Path.DirectorySeparatorChar}{fileName}");

            Assert.Equal(expectedValue?.SizeInBytes, value?.SizeInBytes);
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
