using Moq;

namespace Xamarin.CloudDrive.Connector.LocalDriveTests
{
   internal class ServiceBuilder
   {

      readonly Mock<LocalDriveService> Mock;
      public ServiceBuilder() => this.Mock = new Mock<LocalDriveService>();

      public static ServiceBuilder Create() => new ServiceBuilder();

      public LocalDriveService Build() => this.Mock.Object;

      /*
      public ServiceBuilder WithDefaultProfile()
      {
         var profile = new ProfileVM
         {
            ID = $"{Environment.CommandLine}",
            Description = $"{Environment.UserName} on {Environment.MachineName}"
         };
         this.Mock.Setup(m => m.GetProfile()).ReturnsAsync(profile);
         return this;
      }
      */


   }
}