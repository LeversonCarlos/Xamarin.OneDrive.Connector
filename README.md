# Xamarin.OneDrive.Connector
A wrapper around microsoft identity connector and microsoft graph api to access one drive content

## Sample of how to use the library
### Simplest get-started sample
```csharp
   using Xamarin.OneDrive;

   var connector = new Connector("YOUR_MICROSOFT_APPLICATION_ID", "User.Read");
   if (await connector.ConnectAsync())
   {
      var httpMessage = await connector.GetAsync("me");
      /* json message with the requested data */
   }
   connector.Dispose();
```
### Using the [profile](https://www.nuget.org/packages/Xamarin.OneDrive.Connector.Profile) plugin to request user profile data
```csharp
   using Xamarin.OneDrive;
   using Xamarin.OneDrive.Profile;

   var connector = new Connector("YOUR_MICROSOFT_APPLICATION_ID", "User.Read");
   if (await connector.ConnectAsync())
   {
      var profile = await connector.GetProfileAsync();
      Console.WriteLine($"Connected to {profile.Name} account through address {profile.Mail}");
   }
   connector.Dispose();
```
### Using the [files](https://www.nuget.org/packages/Xamarin.OneDrive.Connector.Files) plugin to search download and manipulate files stored in OneDrive
```csharp
   using Xamarin.OneDrive;
   using Xamarin.OneDrive.Files;

   var connector = new Connector("YOUR_MICROSOFT_APPLICATION_ID", "Files.Read");
   if (await connector.ConnectAsync())
   {
      var fileList = await connector.SearchFilesAsync("*.zip");
      Console.WriteLine($"Retrieved {fileList.Count} files on the search request");
      var file = fileList[0];
      Console.WriteLine($"The file {file.FileName} has {file.Bytes} bytes and is located on {file.FilePath}.");
   }
   connector.Dispose();
```

## Install instructions
* You can add the library to your project using the [nuget](https://www.nuget.org/packages/Xamarin.OneDrive.Connector) package: 
   ```shell
   dotnet add package Xamarin.OneDrive.Connector
   ```  

* And the optionals plugins:
   ```shell
   dotnet add package Xamarin.OneDrive.Connector.Profile  
   dotnet add package Xamarin.OneDrive.Connector.Files  
   ```
* You will nedd a microsoft application id that you can get following [this guide](https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-register-an-app).

## Build using
* [.Net Core](https://dotnet.github.io) 
* [Microsoft Identity](https://github.com/AzureAD/microsoft-authentication-library-for-dotnet) 
* [Microsoft Graph API](https://docs.microsoft.com/en-us/graph/overview) 
* [xUnit](https://xunit.github.io/) 
* [vsCode](https://github.com/Microsoft/vscode) 

## Changelog
### v0.1.*
Trying to learn and apply unit tests (quantum physics for me).  
Conditional framework's builds according to platform specifics, need because android requires some extras steps on acquiring token.  
Extending the HttpClient library to accommodate token acquisition.  
Implementing the plugin concept using profile as the first try.  
Preparing projects to be build, packed and deploy by the server.  
Provide a sample application.
Implementing the UploadAsync method to send file content to drive. 
SearchFiles overloads to allow searching on specific folder.  
Methods for listing folder's childs.  
### v0.2.*
Upgrading Microsoft Identity Client.  


## Authors
* [Leverson Carlos](https://github.com/LeversonCarlos) 

## License
MIT License - see the [LICENSE](LICENSE) file for details.
