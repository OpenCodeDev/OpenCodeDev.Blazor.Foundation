# OpenCodeDev.Blazor.Foundation.Extensions
This is a extension package for Blazor Foundation and some services requires BF's JS.

It can be used on its own if you are not using services requiring BF.

# What's inside?
## Services
### Localizasion Helper
Helps to use localization to easily switch language.
```
using OpenCodeDev.Blazor.Foundation.Extensions;
WebAssemblyHost host = builder.Build();
await host.LoadCulture(); // Load Default 'en' or saved in localStorage as 'culture'
await host.RunAsync();
```

Change language like so...
```
	@inject NavigationManager Navigate;
	await LocalStorage.Set("culture", 'en'); // Set new language in localstorage.
	Navigate.NavigateTo(Navigate.Uri, forceLoad: true);
```
### LocalStorage
Quick helper tool to access and set localstorage objects.

```
builder.Services.AddBFLocalStorage(); // Add LocalStorage Service
```
_Imports.razor
```
@using OpenCodeDev.Blazor.Foundation.Extensions.LocalStorage;
@inject ILocalStorage LocalStorage;
```
CRUD functions
```
await LocalStorage.Set("culture", code); // Set a value
await LocalStorage.Get("culture"); // Get value for given key
await LocalStorage.Remove("culture"); // Remove Specific Item
await LocalStorage.Clear(); // Clear All Storage
```


### Clipboard Service (Requires Blazor Foundation's JS)
Copy and Paste text easily across your app

```
using OpenCodeDev.Blazor.Foundation.Extensions;

builder.Services.AddBFClipboard();
```


```
@using Microsoft.JSInterop;
@using OpenCodeDev.Blazor.Foundation.Extensions.Clipboard;
@inject IJSRuntime JS;
@inject IClipboard clip;

await clip.SetText(JS,"My Copied Text");
string textPaste  = await clip.ReadText(JS); // Requires Permission from user.
```
## Extension Methods
```
@using OpenCodeDev.Blazor.Foundation.Extensions;
```
### HTMLCompliant()
Makes guid html compliant for id (for vanilla JS). It simply makes sure the guid starts with a letter and not a number.

```
Guid id = Guid.NewGuid().HTMLCompliant();
```
### ToJSObjectString()
Convert a <code>Dictionary<string, object></code> to Javascript Object structure.

Note: it can only support cimple object not nested Key:Value (String, Bool, Number).

```
Dictionary<string, object> tt = new Dictionary<string, object>() { { "MyString", "Test" }, { "MyNum", 6.0 }, { "MyBool", false } };
string jsobj = tt.ToJSObjectString(); //Produce a JS object.
```