using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   public class FileTests
   {

      [Fact]
      public async void Service_WithoutConnection_MustReturnNull()
      {
         var connection = ConnectionBuilder.Create().WithCheckConnectionValue(false).Build();
         var service = new LocalDriveService(connection);

         DirectoryVM directoryVM = null;
         var value = await service.GetFiles(directoryVM);

         Assert.Null(value);
      }


   }
}
