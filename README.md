# Xamarin.OneDrive.Connector
A wrapper around microsoft identity connector and microsoft graph api to access one drive content

## Install instructions
TODO

## Sample of how to use the library
### Simplest get started sample
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
