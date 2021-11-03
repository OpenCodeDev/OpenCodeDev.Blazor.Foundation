# OpenCodeDev.Blazor.Foundation.Extensions
This is a extension package for Blazor Foundation and some services requires BF's JS.

It can be used on its own if you are not using services requiring BF.

# What's inside?
## Services
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