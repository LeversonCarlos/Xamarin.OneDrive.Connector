# Xamarin.OneDrive.Connector
A wrapper around microsoft identity connector and microsoft graph api to access one drive content

## Sample of how to use the library
### Simplest get-started sample
```csharp
   using Xamarin.OneDrive;

   var configs = new Configs
   {
      ClientID = "YOUR_MICROSOFT_APPLICATION_ID",
      Scopes = new string[] { "User.Read" }
   };

   var connector = new Connector(configs);
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

   var configs = new Configs
   {
      ClientID = "YOUR_MICROSOFT_APPLICATION_ID",
      Scopes = new string[] { "User.Read" }
   };

   var connector = new Connector(configs);
   if (await connector.ConnectAsync())
   {
      var profile = await App.OneDrive.GetProfileAsync();
      Console.WriteLine($"Connected to {profile.Name} account through address {profile.Mail}");
   }
   connector.Dispose();
```

## Install instructions
You can add the library to your project using the [nuget](https://www.nuget.org/packages/Xamarin.OneDrive.Connector) package: 
```shell
dotnet add package Xamarin.OneDrive.Connector
```  

And the optionals plugins:
```shell
dotnet add package Xamarin.OneDrive.Connector.Profile  
dotnet add package Xamarin.OneDrive.Connector.Search  
```

## Build using
* [.Net Core](https://dotnet.github.io) 
* [Microsoft Identity](https://github.com/AzureAD/microsoft-authentication-library-for-dotnet) 
* [Microsoft Graph API](https://docs.microsoft.com/en-us/graph/overview) 
* [xUnit](https://xunit.github.io/) 
* [vsCode](https://github.com/Microsoft/vscode) 

## Changelog
### v0.0.1
Trying to learn and apply unit tests (quantum physics for me).  
Conditional framework's builds according to platform specifics, need because android requires some extras steps on acquiring token.  
Extending the HttpClient library to accommodate token acquisition.  
Implementing the plugin concept using profile as the first try.  
Preparing projects to be build, packed and deploy by the server.  


## Authors
* [Leverson Carlos](https://github.com/LeversonCarlos). 

## License
MIT License - see the [LICENSE](LICENSE) file for details.
