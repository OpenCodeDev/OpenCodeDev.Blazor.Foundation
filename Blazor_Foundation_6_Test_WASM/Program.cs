using OpenCodeDev.Blazor.Foundation.Components.Plugins.Clipboard;
using Blazor_Foundation_6_Test_Shared.Plugins.CSS;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.StyleManager;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace Blazor_Foundation_6_Test_WASM
{
    public class Program
    {
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    // Required to Manage the Theme Color Style Dynamically
    builder.Services.AddSingleton<IStyleManagement, StyleManagement>();
    // Optional, Can be use on its own to copy text to clipboard and it is required if you use Code Highlighter (HighlightCS)
    builder.Services.AddSingleton<IClipboard, Clipboard>();
    await builder.Build().RunAsync();
}
    }
}
