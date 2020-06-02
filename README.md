# Xamarin.CloudDrive.Connector
A wrapper around some of the **most common cloud drivers** around to be easily used with **xamarin apps**. What started as a specific microsoft onedrive connector, now evolved to a generic library with **multiple implementations** *(including an onedrive one)*.  
![Release](https://github.com/LeversonCarlos/Xamarin.OneDrive.Connector/workflows/Release/badge.svg)

## Install instructions
You can add the library to your project using the [nuget](https://www.nuget.org/packages/Xamarin.OneDrive.Connector) package:  
   ```shell
   dotnet add package Xamarin.OneDrive.Connector
   ```  
*To use the onedrive implementation, you will need a microsoft application id that you can get following [this guide](https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-register-an-app).*

## Example project
There is an example project on the `example` directory. A simple file picker using both implementations (localDrive and oneDrive) to show how to configure and execute to whole thing.  
Just replace `{YOUR_MICROSOFT_APPLICATION_ID}` with the microsoft application id that you received following the guide mentioned above.  

## How to use the OneDrive implementation
Replace `{YOUR_MICROSOFT_APPLICATION_ID}` with the microsoft application id that you received following the guide mentioned above.

### Android project : MainActivity.cs
```csharp
using Xamarin.CloudDrive.Connector.OneDrive;
protected override void OnCreate(Bundle savedInstanceState)
{
   ...
   Xamarin.Forms.Forms.Init(this, savedInstanceState);
   this.AddOneDriveConnector("{YOUR_MICROSOFT_APPLICATION_ID}", "User.Read", "Files.ReadWrite");
   ...
}
```
```csharp
protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
{
   base.OnActivityResult(requestCode, resultCode, data);
   this.SetOneDriveAuthenticationResult(requestCode, resultCode, data);
}
```

### Android project : AndroidManifest.xml
```xml
<application ...>
   <activity android:name="microsoft.identity.client.BrowserTabActivity">
      <intent-filter>
         <action android:name="android.intent.action.VIEW" />
         <category android:name="android.intent.category.DEFAULT" />
         <category android:name="android.intent.category.BROWSABLE" />
         <data android:scheme="msal{YOUR_MICROSOFT_APPLICATION_ID}" android:host="auth" />
      </intent-filter>
   </activity>
</application>
```

### Simplest get-started example 

```csharp
using Xamarin.CloudDrive.Connector.Common;
using Xamarin.CloudDrive.Connector.OneDrive;

var service = DependencyProvider.Get<OneDriveService>();

if (await service.ConnectAsync()) { // user will be asked for credentials 

   // user profile [id, name, mail, picture]
   var profile = await service.GetProfile(); 

   // user's drivers including shared ones
   var driversList = await service.GetDrivers(); 
   var driver = driversList.First();

   // list directories on a directory 
   var directoriesList = await service.GetDirectories(driver);
   var directory = directoriesList.First();

   // list files on a directory 
   var filesList = await service.GetFiles(directory);
   var file = filesList.First();

   // download file content
   byte[] fileContent = await service.Download(file.ID);
   
   // upload file overwriting its content
   file = await service.Upload(file.ID, fileContent);

   // at some point you may call disconnect to clear user auth data
   // doing so, next time user will be asked credentials again
   // not disconnecting, will continue to use same credentials
   await service.DisconnectAsync();
}
```

### iOS project 
**TODO**  
*i have no mac or iDevice, any help here will be appreciated*


## How to use the LocalDrive implementation
This implementation is used to access local external card data

### Android project : MainActivity.cs
```csharp
using Xamarin.CloudDrive.Connector.LocalDrive;
protected override void OnCreate(Bundle savedInstanceState)
{
   ...
   Xamarin.Forms.Forms.Init(this, savedInstanceState);
   this.AddLocalDriveConnector(savedInstanceState);
   ...
}
```
```csharp
public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
{
   this.SetLocalDrivePermissionsResult(requestCode, permissions, grantResults);
   base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
}
```

### Simplest get-started example 

```csharp
using Xamarin.CloudDrive.Connector.Common;
using Xamarin.CloudDrive.Connector.LocalDrive;

var service = DependencyProvider.Get<LocalDriveService>();

if (await service.ConnectAsync()) { // user will be asked to authorize storage permissions 

   // device's external cards 
   var driversList = await service.GetDrivers(); 
   var driver = driversList.First();

   // list directories on a directory 
   var directoriesList = await service.GetDirectories(driver);
   var directory = directoriesList.First();

   // list files on a directory 
   var filesList = await service.GetFiles(directory);
   var file = filesList.First();

   // open file content
   byte[] fileContent = await service.Download(file.ID);
   
   // overwrite file content
   file = await service.Upload(file.ID, fileContent);

}
```

## Build using
* [.Net Core](https://dotnet.github.io) 
* [Microsoft Identity](https://github.com/AzureAD/microsoft-authentication-library-for-dotnet) 
* [Microsoft Graph API](https://docs.microsoft.com/en-us/graph/overview) 
* [xUnit](https://xunit.github.io/) 
* [vsCode](https://github.com/Microsoft/vscode) 

## Changelog
### v1.0.*
The specific OneDrive package evolved to a generic library with multiple implementations.  
**breaking changes**
### v0.4.*
Upgrading component version 
### v0.3.*
Upgrading MsBuild.Sdk.Extras dependency.  
Upgrading Xamarin.Forms dependency.  
Upgrading Microsoft Identity Client dependency.  
Removing UWP scenarios and projects.  
### v0.2.*
Upgrading Microsoft Identity Client.  
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


## Authors
* [Leverson Carlos](https://github.com/LeversonCarlos) 

## License
MIT License - see the [LICENSE](LICENSE) file for details.
