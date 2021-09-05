using OpenCodeDev.Blazor.Foundation;
using OpenCodeDev.Blazor.Foundation.Extensions;
using OpenCodeDev.Blazor.Foundation.Doc.Core.Plugins.CSS;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.StyleManager;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OpenCodeDev.Blazor.Foundation.Doc.Wasm
{
    public class Program
    {
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    // Required to Manage the Theme Color Style Dynamically
    builder.Services.AddAllBFPlugins();
    // Optional, Can be use on its own to copy text to clipboard and it is required if you use Code Highlighter (HighlightCS)
    builder.Services.AddBFClipboard();
    await builder.Build().RunAsync();
}
    }
}
